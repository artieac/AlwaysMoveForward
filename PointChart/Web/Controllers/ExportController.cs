using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Models;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Code.Responses;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class ExportController : BaseController
    {
        private const string TaskColumnHeader = "Task";
        private const string DateFormat = "MM-dd-yyyy";

        private ChartTaskModel GenerateCompletedTasks(DateTime? targetDate, long chartId)
        {
            ChartTaskModel retVal = new ChartTaskModel();

            DateTime _targetDate = DateTime.Now.Date;

            if (targetDate.HasValue)
            {
                _targetDate = targetDate.Value.Date;
            }

            retVal.Calendar = new CalendarModel(1);
            retVal.Calendar.WeekStartDate = AlwaysMoveForward.Common.Utilities.Utils.DetermineStartOfWeek(_targetDate);

            retVal.Chart = this.Services.Charts.GetById(chartId);
            retVal.PointEarner = retVal.Chart.PointEarner;
            retVal.ChartTasks = retVal.Chart.Tasks;
            retVal.CompletedTasks = new Dictionary<long, IDictionary<DateTime, CompletedTask>>();

            IList<CompletedTask> tasksCompletedDuringWeek = this.Services.CompletedTaskService.GetByChart(chartId, retVal.Calendar.WeekStartDate, retVal.Calendar.WeekStartDate.AddDays(7), this.CurrentPrincipal.CurrentUser);

            foreach (CompletedTask completedTask in tasksCompletedDuringWeek)
            {
                if (!retVal.CompletedTasks.ContainsKey(completedTask.TaskId))
                {
                    retVal.CompletedTasks.Add(completedTask.TaskId, new Dictionary<DateTime, CompletedTask>());
                }

                retVal.CompletedTasks[completedTask.TaskId].Add(completedTask.DateCompleted, completedTask);
            }

            return retVal;
        }

        [Route("Export/Completed/{id}/{year}/{month}/{day}"), HttpGet()]
        [MVCAuthorization]
        public ActionResult Completed(long id, int year, int month, int day, string fileType)
        {
            DateTime targetDate = DateTime.Parse(month + "-" + day + "-" + year);
            ChartTaskModel model = this.GenerateCompletedTasks(targetDate, id);

            IList<string> reportHeaders = this.GenerateReportHeaders(model.Calendar.WeekStartDate);
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(TaskColumnHeader, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");

                if (model.CompletedTasks.ContainsKey(model.ChartTasks[i].Id))
                {
                    for (int j = 0; j < 7; j++)
                    {
                        string dateString = model.Calendar.WeekStartDate.AddDays(j).Date.ToString("MM-DD-YYYY");
                        string columnValue = "0";

                        if (model.CompletedTasks[model.ChartTasks[i].Id].ContainsKey(model.Calendar.WeekStartDate.AddDays(j).Date))
                        {
                            columnValue = model.CompletedTasks[model.ChartTasks[i].Id][model.Calendar.WeekStartDate.AddDays(j).Date].NumberOfTimesCompleted.ToString();
                        }

                        columnData.Add(reportHeaders[j + 1], columnValue);
                    }

                    rowData.Add(columnData);
                }
                else
                {
                    columnData.Add(reportHeaders[1], string.Empty);
                    columnData.Add(reportHeaders[2], string.Empty);
                    columnData.Add(reportHeaders[3], string.Empty);
                    columnData.Add(reportHeaders[4], string.Empty);
                    columnData.Add(reportHeaders[5], string.Empty);
                    columnData.Add(reportHeaders[6], string.Empty);
                    columnData.Add(reportHeaders[7], string.Empty);
                    rowData.Add(columnData);
                }
            }

            if (string.Compare(fileType, FileExtension.FileType.Excel.ToString(), true) == 0)
            {
                return this.Excel(this.GenerateHeaderPrefix(model), reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + ".xls");
            }
            else
            {
                return this.CSV(reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + "csv");
            }
        }

        [Route("Export/Empty/{id}"), HttpGet()]
        [MVCAuthorization]
        public ActionResult Empty(int id, String fileType)
        {
            ChartTaskModel model = this.GenerateCompletedTasks(null, id);

            IList<String> reportHeaders = this.GenerateReportHeaders(model.Calendar.WeekStartDate);
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(TaskColumnHeader, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");
                columnData.Add(reportHeaders[1], string.Empty);
                columnData.Add(reportHeaders[2], string.Empty);
                columnData.Add(reportHeaders[3], string.Empty);
                columnData.Add(reportHeaders[4], string.Empty);
                columnData.Add(reportHeaders[5], string.Empty);
                columnData.Add(reportHeaders[6], string.Empty);
                columnData.Add(reportHeaders[7], string.Empty);
                rowData.Add(columnData);
            }

            if (string.Compare(fileType, FileExtension.FileType.Excel.ToString(), true) == 0)
            {
                return this.Excel(this.GenerateHeaderPrefix(model), reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + ".xls");
            }
            else
            {
                return this.CSV(reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + "csv");
            }
        }

        private IList<IList<String>> GenerateHeaderPrefix(ChartTaskModel model)
        {
            IList<IList<String>> retVal = new List<IList<String>>();
            IList<String> nameRow = new List<String>();
            nameRow.Add("Name:");
            nameRow.Add(model.PointEarner.FirstName + " " + model.PointEarner.LastName);
            retVal.Add(nameRow);

            IList<String> pointsEarnedRow = new List<String>();
            pointsEarnedRow.Add("Points Earned");
            pointsEarnedRow.Add("0");
            retVal.Add(pointsEarnedRow);

            IList<String> pointsSpentRow = new List<String>();
            pointsSpentRow.Add("Points Spent");
            pointsSpentRow.Add("0");
            retVal.Add(pointsSpentRow);

            IList<String> totalPointsRow = new List<String>();
            totalPointsRow.Add("Total Points");
            totalPointsRow.Add(Convert.ToString(0));
            retVal.Add(totalPointsRow);

            return retVal;
        }

        private IList<string> GenerateReportHeaders(DateTime weekStartDate)
        {
            IList<string> retVal = new List<String>();
            retVal.Add(TaskColumnHeader);
            retVal.Add(DayOfWeek.Sunday.ToString() + " " + weekStartDate.Date.ToString(ExportController.DateFormat));
            retVal.Add(DayOfWeek.Monday.ToString() + " " + weekStartDate.AddDays(1).Date.ToString(ExportController.DateFormat));
            retVal.Add(DayOfWeek.Tuesday.ToString() + " " + weekStartDate.AddDays(2).Date.ToString(ExportController.DateFormat));
            retVal.Add(DayOfWeek.Wednesday.ToString() + " " + weekStartDate.AddDays(3).Date.ToString(ExportController.DateFormat));
            retVal.Add(DayOfWeek.Thursday.ToString() + " " + weekStartDate.AddDays(4).Date.ToString(ExportController.DateFormat));
            retVal.Add(DayOfWeek.Friday.ToString() + " " + weekStartDate.AddDays(5).Date.ToString(ExportController.DateFormat));
            retVal.Add(DayOfWeek.Saturday.ToString() + " " + weekStartDate.AddDays(6).Date.ToString(ExportController.DateFormat));

            return retVal;
        }
    }
}
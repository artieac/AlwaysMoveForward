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
    public class ChartController : BaseController
    {
        private const String TaskColumnHeader = "Task";

        private ChartTaskModel GenerateCompletedTasks(DateTime? targetDate, long chartId, PointChartUser pointEarner)
        {
            ChartTaskModel retVal = new ChartTaskModel();

            DateTime _targetDate = DateTime.Now.Date;

            if (targetDate.HasValue)
            {
                _targetDate = targetDate.Value.Date;
            }

            retVal.Calendar = new CalendarModel();
            retVal.Calendar.TargetMonth = _targetDate;
            retVal.Calendar.ViewDate = _targetDate;
            retVal.Calendar.RouteInformation = "/Chart/ViewChart/" + chartId;
            retVal.Calendar.WeekStartDate = AlwaysMoveForward.Common.Utilities.Utils.DetermineStartOfWeek(_targetDate);
            retVal.PointEarner = pointEarner;

            retVal.Chart = this.Services.Charts.GetById(chartId);
            retVal.ChartTasks = retVal.Chart.Tasks;
            retVal.CompletedTasks = new Dictionary<long, IDictionary<DateTime, CompletedTask>>();

            IEnumerable<CompletedTask> tasksCompletedDuringWeek = from tasks in retVal.Chart.CompletedTasks where tasks.DateCompleted > retVal.Calendar.WeekStartDate && tasks.DateCompleted < retVal.Calendar.WeekStartDate.AddDays(7) select tasks;

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

        [MVCAuthorization]
        public ActionResult CompletedTasks(long pointEarnerId, DateTime? targetDate, long id)
        {
            PointChartUser pointEarner = this.Services.UserService.GetById(pointEarnerId);
            return View(this.GenerateCompletedTasks(targetDate, id, pointEarner));
        }

        [MVCAuthorization]
        public ActionResult Export(long pointEarnerId, long id, String fileType, DateTime? targetDate)
        {
            PointChartUser pointEarner = this.Services.UserService.GetById(pointEarnerId);
            ChartTaskModel model = this.GenerateCompletedTasks(targetDate, id, pointEarner);

            IList<String> reportHeaders = this.GenerateReportHeaders();
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(TaskColumnHeader, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");

                if (model.CompletedTasks.ContainsKey(model.ChartTasks[i].Id))
                {
                    for (int j = 0; j < 7; j++)
                    {
                        String columnValue = "0";

                        if (model.CompletedTasks[model.ChartTasks[i].Id].ContainsKey(model.Calendar.WeekStartDate.AddDays(j).Date))
                        {
                            columnValue = model.CompletedTasks[model.ChartTasks[i].Id][model.Calendar.WeekStartDate.AddDays(j).Date].NumberOfTimesCompleted.ToString();
                        }

                        switch (j)
                        {
                            case 0:
                                columnData.Add(DayOfWeek.Sunday.ToString(), columnValue);
                                break;
                            case 1:
                                columnData.Add(DayOfWeek.Monday.ToString(), columnValue);
                                break;
                            case 2:
                                columnData.Add(DayOfWeek.Tuesday.ToString(), columnValue);
                                break;
                            case 3:
                                columnData.Add(DayOfWeek.Wednesday.ToString(), columnValue);
                                break;
                            case 4:
                                columnData.Add(DayOfWeek.Thursday.ToString(), columnValue);
                                break;
                            case 5:
                                columnData.Add(DayOfWeek.Friday.ToString(), columnValue);
                                break;
                            case 6:
                                columnData.Add(DayOfWeek.Saturday.ToString(), columnValue);
                                break;
                        }
                    }

                    rowData.Add(columnData);
                }
                else
                {
                    columnData.Add(DayOfWeek.Sunday.ToString(), string.Empty);
                    columnData.Add(DayOfWeek.Monday.ToString(), string.Empty);
                    columnData.Add(DayOfWeek.Tuesday.ToString(), string.Empty);
                    columnData.Add(DayOfWeek.Wednesday.ToString(), string.Empty);
                    columnData.Add(DayOfWeek.Thursday.ToString(), string.Empty);
                    columnData.Add(DayOfWeek.Friday.ToString(), string.Empty);
                    columnData.Add(DayOfWeek.Saturday.ToString(), string.Empty);
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

        [MVCAuthorization]
        public ActionResult ExportEmpty(int pointEarnerId, int id, String fileType, DateTime? targetDate)
        {
            PointChartUser pointEarner = this.Services.UserService.GetById(pointEarnerId);
            ChartTaskModel model = this.GenerateCompletedTasks(targetDate, id, pointEarner);

            IList<String> reportHeaders = this.GenerateReportHeaders();
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(TaskColumnHeader, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");
                columnData.Add(DayOfWeek.Sunday.ToString(), string.Empty);
                columnData.Add(DayOfWeek.Monday.ToString(), string.Empty);
                columnData.Add(DayOfWeek.Tuesday.ToString(), string.Empty);
                columnData.Add(DayOfWeek.Wednesday.ToString(), string.Empty);
                columnData.Add(DayOfWeek.Thursday.ToString(), string.Empty);
                columnData.Add(DayOfWeek.Friday.ToString(), string.Empty);
                columnData.Add(DayOfWeek.Saturday.ToString(), string.Empty);
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

        private IList<String> GenerateReportHeaders()
        {
            IList<String> retVal = new List<String>();
            retVal.Add(TaskColumnHeader);
            retVal.Add(DayOfWeek.Sunday.ToString());
            retVal.Add(DayOfWeek.Monday.ToString());
            retVal.Add(DayOfWeek.Tuesday.ToString());
            retVal.Add(DayOfWeek.Wednesday.ToString());
            retVal.Add(DayOfWeek.Thursday.ToString());
            retVal.Add(DayOfWeek.Friday.ToString());
            retVal.Add(DayOfWeek.Saturday.ToString());

            return retVal;
        }
    }
}
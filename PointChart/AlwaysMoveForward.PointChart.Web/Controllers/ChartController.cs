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
        private const string TaskColumn = "Task";

        // GET: /Chart/
        [RequestAuthorizationAttribute]
        public ActionResult Index()
        {
            ChartModel model = new ChartModel();
            model.Charts = this.Services.Charts.GetByUser(this.CurrentPrincipal.CurrentUser);
            return this.View(model);
        }

        [RequestAuthorizationAttribute]
        public ActionResult Add(int pointEarnerId)
        {
            PointEarner pointEarner = this.Services.PointEarner.GetById(pointEarnerId);

            if (pointEarner != null)
            {
                Chart newChart = this.Services.Charts.Add(pointEarnerId, this.CurrentPrincipal.CurrentUser);

                if (newChart == null)
                {
                    // tbd handle creation error.
                }
            }

            return this.Index();
        }

        [RequestAuthorizationAttribute]
        public ActionResult Edit(int editChartId, int editPointEarnerId, string editChartName)
        {
            PointEarner pointEarner = this.Services.PointEarner.GetById(editPointEarnerId);

            if (pointEarner != null)
            {
                this.Services.Charts.Edit(editChartId, editChartName, pointEarner.Id, this.CurrentPrincipal.CurrentUser);
            }

            return this.Index();
        }
        
        [RequestAuthorizationAttribute]
        public ActionResult ShowTasks(int chartId)
        {
            ChartTaskModel model = new ChartTaskModel();
            model.Chart = this.Services.Charts.GetById(chartId);
            model.ChartTasks = model.Chart.Tasks;
            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);
            return this.View("_ShowTasks", model);
        }

        [RequestAuthorization]
        public ActionResult AddTask(int chartId, int taskSelection)
        {
            this.Services.Charts.AddTask(chartId, taskSelection);
            return this.ShowTasks(chartId);
        }

        [RequestAuthorization]
        public ActionResult DeleteTask(int chartId, int taskId)
        {
            this.Services.Charts.DeleteTask(chartId, taskId);
            return this.ShowTasks(chartId);
        }

        private ChartTaskModel GenerateCompletedTasks(DateTime? targetDate, int pointEarnerId, int chartId)
        {
            ChartTaskModel retVal = new ChartTaskModel();

            DateTime completedDate = DateTime.Now.Date;

            if (targetDate.HasValue)
            {
                completedDate = targetDate.Value.Date;
            }

            retVal.Calendar = new CalendarModel();
            retVal.Calendar.TargetMonth = completedDate;
            retVal.Calendar.ViewDate = completedDate;
            retVal.Calendar.WeekStartDate = AlwaysMoveForward.Common.Utilities.Utils.DetermineStartOfWeek(completedDate);

            retVal.PointEarner = this.Services.PointEarner.GetById(pointEarnerId);
            retVal.Chart = (from targetChart in retVal.PointEarner.Charts where targetChart.Id == chartId select targetChart).Single();
            retVal.ChartTasks = retVal.Chart.Tasks;
            retVal.CompletedTasks = new Dictionary<int, IDictionary<DateTime, CompletedTask>>();

            IList<CompletedTask> tasksCompletedDuringWeek = this.Services.Tasks.GetCompletedByDateRangeAndChart(retVal.Calendar.WeekStartDate, retVal.Calendar.WeekStartDate.AddDays(7), retVal.Chart, this.CurrentPrincipal.CurrentUser);

            for (int i = 0; i < tasksCompletedDuringWeek.Count; i++)
            {
                if (!retVal.CompletedTasks.ContainsKey(tasksCompletedDuringWeek[i].Task.Id))
                {
                    retVal.CompletedTasks.Add(tasksCompletedDuringWeek[i].Task.Id, new Dictionary<DateTime, CompletedTask>());
                }

                retVal.CompletedTasks[tasksCompletedDuringWeek[i].Task.Id].Add(tasksCompletedDuringWeek[i].DateCompleted, tasksCompletedDuringWeek[i]);
            }

            return retVal;
        }

        [RequestAuthorization]
        public ActionResult ViewChart(DateTime? targetDate, int pointEarnerId, int id)
        {
            return this.View("ViewChart", this.GenerateCompletedTasks(targetDate, pointEarnerId, id));
        }

        [RequestAuthorization]
        public ActionResult CompleteTask(int pointEarnerId, int chartId, int taskId, DateTime weekStartDate, int sundayInput, int mondayInput, int tuesdayInput, int wednesdayInput, int thursdayInput, int fridayInput, int saturdayInput)
        {
            Chart chart = this.Services.Charts.GetById(chartId);
            Task task = this.Services.Tasks.GetById(taskId);
            this.Services.Charts.AddCompletedTask(chart, task, weekStartDate.Date, sundayInput, this.CurrentPrincipal.CurrentUser);
            this.Services.Charts.AddCompletedTask(chart, task, weekStartDate.AddDays(1).Date, mondayInput, this.CurrentPrincipal.CurrentUser);
            this.Services.Charts.AddCompletedTask(chart, task, weekStartDate.AddDays(2).Date, tuesdayInput, this.CurrentPrincipal.CurrentUser);
            this.Services.Charts.AddCompletedTask(chart, task, weekStartDate.AddDays(3).Date, wednesdayInput, this.CurrentPrincipal.CurrentUser);
            this.Services.Charts.AddCompletedTask(chart, task, weekStartDate.AddDays(4).Date, thursdayInput, this.CurrentPrincipal.CurrentUser);
            this.Services.Charts.AddCompletedTask(chart, task, weekStartDate.AddDays(5).Date, fridayInput, this.CurrentPrincipal.CurrentUser);
            this.Services.Charts.AddCompletedTask(chart, task, weekStartDate.AddDays(6).Date, saturdayInput, this.CurrentPrincipal.CurrentUser);
            return this.ViewChart(weekStartDate, pointEarnerId, chartId);
        }

        [RequestAuthorization]
        public ActionResult Export(int pointEarnerId, int chartId, string fileType, DateTime? targetDate)
        {
            ChartTaskModel model = this.GenerateCompletedTasks(targetDate, pointEarnerId, chartId);

            IList<string> reportHeaders = this.GenerateReportHeaders();
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(TaskColumn, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");

                if (model.CompletedTasks.ContainsKey(model.ChartTasks[i].Id))
                {
                    for (int j = 0; j < 7; j++)
                    {
                        string columnValue = "0";

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

        [RequestAuthorization]
        public ActionResult ExportEmpty(int pointEarnerId, int chartId, string fileType, DateTime? targetDate)
        {
            ChartTaskModel model = this.GenerateCompletedTasks(targetDate, pointEarnerId, chartId);

            IList<string> reportHeaders = this.GenerateReportHeaders();
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(TaskColumn, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");
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

        private IList<IList<string>> GenerateHeaderPrefix(ChartTaskModel model)
        {
            IList<IList<string>> retVal = new List<IList<string>>();
            IList<string> nameRow = new List<string>();
            nameRow.Add("Name:");
            nameRow.Add(model.PointEarner.FirstName + " " + model.PointEarner.LastName);
            retVal.Add(nameRow);

            IList<string> pointsEarnedRow = new List<string>();
            pointsEarnedRow.Add("Points Earned");
            pointsEarnedRow.Add(model.PointEarner.PointsEarned.ToString());
            retVal.Add(pointsEarnedRow);

            IList<string> pointsSpentRow = new List<string>();
            pointsSpentRow.Add("Points Spent");
            pointsSpentRow.Add(model.PointEarner.PointsSpent.ToString());
            retVal.Add(pointsSpentRow);

            IList<string> totalPointsRow = new List<string>();
            totalPointsRow.Add("Total Points");
            totalPointsRow.Add(Convert.ToString(model.PointEarner.PointsAvailable.ToString()));
            retVal.Add(totalPointsRow);

            return retVal;
        }

        private IList<string> GenerateReportHeaders()
        {
            IList<string> retVal = new List<string>();
            retVal.Add(TaskColumn);
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

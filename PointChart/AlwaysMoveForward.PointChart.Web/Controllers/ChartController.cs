using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.DataLayer.Entities;
using AlwaysMoveForward.PointChart.Web.Models;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Code.Responses;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class ChartController : BaseController
    {
        private class ReportColumns
        {
            public static String Task = "Task";
            public static String SundayTasks = "Sunday";
            public static String MondayTasks = "Monday";
            public static String TuesdayTasks = "Tuesday";
            public static String WednesdayTasks = "Wednesday";
            public static String ThursdayTasks = "Thursday";
            public static String FridayTasks = "Friday";
            public static String SaturdayTasks = "Saturday";
        }

        //
        // GET: /Chart/
        [RequestAuthorizationAttribute]
        public ActionResult Index()
        {
            ChartModel model = new ChartModel();
            model.Charts = this.Services.Charts.GetByUser(this.CurrentPrincipal.CurrentUser);
            return View(model);
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

            return Index();
        }

        [RequestAuthorizationAttribute]
        public ActionResult Edit(int editChartId, int editPointEarnerId, String editChartName)
        {
            PointEarner pointEarner = this.Services.PointEarner.GetById(editPointEarnerId);

            if (pointEarner != null)
            {
                this.Services.Charts.Edit(editChartId, editChartName, pointEarner.Id, this.CurrentPrincipal.CurrentUser);
            }

            return Index();
        }
        
        [RequestAuthorizationAttribute]
        public ActionResult ShowTasks(int chartId)
        {
            ChartTaskModel model = new ChartTaskModel();
            model.Chart = this.Services.Charts.GetById(chartId);
            model.ChartTasks = model.Chart.Tasks;
            model.Tasks = this.Services.Tasks.GetByUser(this.CurrentPrincipal.CurrentUser);
            return View("_ShowTasks", model);
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

        private ChartTaskModel GenerateCompletedTasks(DateTime? targetDate, int chartId)
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

            retVal.Chart = this.Services.Charts.GetById(chartId);
            retVal.ChartTasks = retVal.Chart.Tasks;
            retVal.CompletedTasks = new Dictionary<int, IDictionary<DateTime, CompletedTask>>();
            retVal.PointEarner = retVal.Chart.PointEarner;

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
        public ActionResult ViewChart(DateTime? targetDate, int id)
        {
            return View("ViewChart", this.GenerateCompletedTasks(targetDate, id));
        }

        [RequestAuthorization]
        public ActionResult CompleteTask(int chartId, int taskId, DateTime weekStartDate, int sundayInput, int mondayInput, int tuesdayInput, int wednesdayInput, int thursdayInput, int fridayInput, int saturdayInput)
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
            return this.ViewChart(weekStartDate, chartId);
        }

        [RequestAuthorization]
        public ActionResult Export(int id, String fileType, DateTime? targetDate)
        {
            ChartTaskModel model = this.GenerateCompletedTasks(targetDate, id);

            IList<String> reportHeaders = this.GenerateReportHeaders();
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(ReportColumns.Task, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");

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
                                columnData.Add(ReportColumns.SundayTasks, columnValue);
                                break;
                            case 1:
                                columnData.Add(ReportColumns.MondayTasks, columnValue);
                                break;
                            case 2:
                                columnData.Add(ReportColumns.TuesdayTasks, columnValue);
                                break;
                            case 3:
                                columnData.Add(ReportColumns.WednesdayTasks, columnValue);
                                break;
                            case 4:
                                columnData.Add(ReportColumns.ThursdayTasks, columnValue);
                                break;
                            case 5:
                                columnData.Add(ReportColumns.FridayTasks, columnValue);
                                break;
                            case 6:
                                columnData.Add(ReportColumns.SaturdayTasks, columnValue);
                                break;
                        }
                    }

                    rowData.Add(columnData);
                }
                else
                {
                    columnData.Add(ReportColumns.SundayTasks, "");
                    columnData.Add(ReportColumns.MondayTasks, "");
                    columnData.Add(ReportColumns.TuesdayTasks, "");
                    columnData.Add(ReportColumns.WednesdayTasks, "");
                    columnData.Add(ReportColumns.ThursdayTasks, "");
                    columnData.Add(ReportColumns.FridayTasks, "");
                    columnData.Add(ReportColumns.SaturdayTasks, "");
                    rowData.Add(columnData);
                }
            }

            if (String.Compare(fileType, FileExtension.FileType_Excel, true) == 0)
            {
                return this.Excel(this.GenerateHeaderPrefix(model), reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + ".xls");
            }
            else
            {
                return this.CSV(reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + "csv");
            }
        }

        [RequestAuthorization]
        public ActionResult ExportEmpty(int id, String fileType, DateTime? targetDate)
        {
            ChartTaskModel model = this.GenerateCompletedTasks(targetDate, id);

            IList<String> reportHeaders = this.GenerateReportHeaders();
            IList<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            for (int i = 0; i < model.ChartTasks.Count; i++)
            {
                Dictionary<string, string> columnData = new Dictionary<string, string>();
                columnData.Add(ReportColumns.Task, model.ChartTasks[i].Name + " (" + model.ChartTasks[i].Points + ")");
                columnData.Add(ReportColumns.SundayTasks, "");
                columnData.Add(ReportColumns.MondayTasks, "");
                columnData.Add(ReportColumns.TuesdayTasks, "");
                columnData.Add(ReportColumns.WednesdayTasks, "");
                columnData.Add(ReportColumns.ThursdayTasks, "");
                columnData.Add(ReportColumns.FridayTasks, "");
                columnData.Add(ReportColumns.SaturdayTasks, "");
                rowData.Add(columnData);
            }

            if (String.Compare(fileType, FileExtension.FileType_Excel, true)==0)
            {
                return this.Excel(this.GenerateHeaderPrefix(model), reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + ".xls");
            }
            else
            {
                return this.CSV(reportHeaders, rowData, model.PointEarner.FirstName + "_" + model.PointEarner.LastName + "csv");
            }
        }

        private IList<IList<String>>  GenerateHeaderPrefix(ChartTaskModel model)
        {
            IList<IList<String>> retVal = new List<IList<String>>();
            IList<String> nameRow = new List<String>();
            nameRow.Add("Name:");
            nameRow.Add(model.PointEarner.FirstName + " " + model.PointEarner.LastName);
            retVal.Add(nameRow);

            IList<String> pointsEarnedRow = new List<String>();
            pointsEarnedRow.Add("Points Earned");
            pointsEarnedRow.Add(model.PointEarner.PointsEarned.ToString());
            retVal.Add(pointsEarnedRow);

            IList<String> pointsSpentRow = new List<String>();
            pointsSpentRow.Add("Points Spent");
            pointsSpentRow.Add(model.PointEarner.PointsSpent.ToString());
            retVal.Add(pointsSpentRow);

            IList<String> totalPointsRow = new List<String>();
            totalPointsRow.Add("Total Points");
            totalPointsRow.Add(Convert.ToString(model.PointEarner.PointsAvailable.ToString()));
            retVal.Add(totalPointsRow);

            return retVal;
        }

        private IList<String> GenerateReportHeaders()
        {
            IList<String> retVal = new List<String>();
            retVal.Add(ReportColumns.Task);
            retVal.Add(ReportColumns.SundayTasks);
            retVal.Add(ReportColumns.MondayTasks);
            retVal.Add(ReportColumns.TuesdayTasks);
            retVal.Add(ReportColumns.WednesdayTasks);
            retVal.Add(ReportColumns.ThursdayTasks);
            retVal.Add(ReportColumns.FridayTasks);
            retVal.Add(ReportColumns.SaturdayTasks);

            return retVal;
        }
    }
}

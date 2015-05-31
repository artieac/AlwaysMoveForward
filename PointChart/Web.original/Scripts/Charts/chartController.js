define('chartController',
        ['jquery', 'Common/tableManager'],
    function ($, tableManager) {
        var getTasks = function (chartId) {
            $("#showChartTasks > #chartId").val(chartId);
            var submitOptions = { target: '#chartTasksContainer' };
            $('#showChartTasks').ajaxSubmit(submitOptions);
        }

        var handleSelection = function (chartId) {
            tableManager.makeRowEditable("chartInput", chartId, "Element");
            this.GetTasks(chartId);
        }

        var addChart = function () {
            $("#addChartForm > #chartName").val($("#newChartName").val());
            $("#addChartForm").submit();
        }

        var saveEdit = function (chartId) {
            $("#editChartForm > #editChartId").val(chartId);
            $("#editChartForm > #editPointEarnerId").val($("#chartInput" + chartId + "ElementPointEarnerId").val());
            $("#editChartForm > #editChartName").val($("#chartInput" + chartId + "ElementChartName").val());
            $("#editChartForm").ajaxSubmit(null);
        }

        var handleTaskSelectionChange = function () {
            var selectedTaskId = $("#taskSelection").val();
            $("#selectedTaskName").val($("#taskName" + selectedTaskId).val());
            $("#selectedTaskPoints").val($("#taskPoints" + selectedTaskId).val());
            $("#selectedTaskMaxPerDay").val($("#taskMaxPerDay" + selectedTaskId).val());
        }

        var addTaskToChart = function () {
            var submitOptions = { target: '#chartTasksContainer' };
            $('#addTaskToChart').ajaxSubmit(submitOptions);
        }

        var deleteTask = function (chartId, taskId) {
            $("#deleteTaskForm > #chartId").val(chartId);
            $("#deleteTaskForm > #taskId").val(taskId);
            var submitOptions = { target: '#chartTasksContainer' };
            $('#deleteTaskForm').ajaxSubmit(submitOptions);
        }

        var taskInputEditable = function (taskId) {
            alert('taskId');
            tableManager.toggleRowItemEditMode(taskId);
        }

        var completeTask = function (taskId) {
            $("#editButton" + taskId).show();
            $("#saveButton" + taskId).hide();
            $("#completeTaskForm > #taskId").val(taskId);
            $("#completeTaskForm > #sundayInput").val($("#input" + taskId + "Sunday").val());
            $("#completeTaskForm > #mondayInput").val($("#input" + taskId + "Monday").val());
            $("#completeTaskForm > #tuesdayInput").val($("#input" + taskId + "Tuesday").val());
            $("#completeTaskForm > #wednesdayInput").val($("#input" + taskId + "Wednesday").val());
            $("#completeTaskForm > #thursdayInput").val($("#input" + taskId + "Thursday").val());
            $("#completeTaskForm > #fridayInput").val($("#input" + taskId + "Friday").val());
            $("#completeTaskForm > #saturdayInput").val($("#input" + taskId + "Saturday").val());
            $("#completeTaskForm").submit();
        }

        var spendPoints = function () {
            $("#spendPointsForm > #pointsToSpend").val($("#pointsToSpendInput").val());
            $("#spendPointsForm").submit();
        }

        return {
            getTasks: getTasks,
            handleSelection: handleSelection,
            addChart: addChart,
            saveEdit: saveEdit,
            handleTaskSelectionChange: handleTaskSelectionChange,
            addTaskToChart: addTaskToChart,
            deleteTask: deleteTask,
            taskInputEditable: taskInputEditable,
            completeTask: completeTask,
            spendPoints: spendPoints
        }
    }
);

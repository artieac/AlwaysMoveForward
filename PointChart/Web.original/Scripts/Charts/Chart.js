var Chart = new function () {

    this.MakeEditable = function (prefixFilter, prefixIndex, suffixFilter) {
        var jQueryFilter = "";

        if (prefixFilter != null) {
            jQueryFilter += "[name^=" + prefixFilter + "]";
        }

        jQuery(jQueryFilter + ".readonly").attr("disabled", "true");
        jQuery(jQueryFilter + ".hidden").hide();

        if (prefixIndex != null) {
            jQueryFilter = "[name^=" + prefixFilter + prefixIndex;

            if (suffixFilter != null) {
                jQueryFilter += suffixFilter;
            }

            jQueryFilter += "]";
        }

        jQuery(jQueryFilter + ".readonly").removeAttr("disabled");
        jQuery(jQueryFilter + ".hidden").show();
    };

    this.GetTasks = function (chartId) {
        jQuery("#showChartTasks > #chartId").val(chartId);
        var submitOptions = { target: '#chartTasksContainer' };
        jQuery('#showChartTasks').ajaxSubmit(submitOptions);
    };

    this.HandleSelection = function (chartId) {
        this.MakeEditable("chartInput", chartId, "Element");
        this.GetTasks(chartId);
    };

    this.AddChart = function () {
        jQuery("#addChartForm > #chartName").val(jQuery("#newChartName").val());
        jQuery("#addChartForm").submit();
    };

    this.SaveEdit = function (chartId) {
        jQuery("#editChartForm > #editChartId").val(chartId);
        jQuery("#editChartForm > #editPointEarnerId").val(jQuery("#chartInput" + chartId + "ElementPointEarnerId").val());
        jQuery("#editChartForm > #editChartName").val(jQuery("#chartInput" + chartId + "ElementChartName").val());
        jQuery("#editChartForm").ajaxSubmit(null);
    };
    this.HandleTaskSelectionChange = function () {
        var selectedTaskId = jQuery("#taskSelection").val();
        jQuery("#selectedTaskName").val(jQuery("#taskName" + selectedTaskId).val());
        jQuery("#selectedTaskPoints").val(jQuery("#taskPoints" + selectedTaskId).val());
        jQuery("#selectedTaskMaxPerDay").val(jQuery("#taskMaxPerDay" + selectedTaskId).val());
    };

    this.AddTaskToChart = function () {
        var submitOptions = { target: '#chartTasksContainer' };
        jQuery('#addTaskToChart').ajaxSubmit(submitOptions);
    };

    this.DeleteTask = function (chartId, taskId) {
        jQuery("#deleteTaskForm > #chartId").val(chartId);
        jQuery("#deleteTaskForm > #taskId").val(taskId);
        var submitOptions = { target: '#chartTasksContainer' };
        jQuery('#deleteTaskForm').ajaxSubmit(submitOptions);
    };

    this.TaskInputEditable = function (taskId) {
        this.MakeEditable("input", taskId);
        jQuery("#editButton" + taskId).hide();
        jQuery("#saveButton" + taskId).show();
    }

    this.CompleteTask = function (taskId) {
        jQuery("#editButton" + taskId).show();
        jQuery("#saveButton" + taskId).hide();
        jQuery("#completeTaskForm > #taskId").val(taskId);
        jQuery("#completeTaskForm > #sundayInput").val(jQuery("#input" + taskId + "Sunday").val());
        jQuery("#completeTaskForm > #mondayInput").val(jQuery("#input" + taskId + "Monday").val());
        jQuery("#completeTaskForm > #tuesdayInput").val(jQuery("#input" + taskId + "Tuesday").val());
        jQuery("#completeTaskForm > #wednesdayInput").val(jQuery("#input" + taskId + "Wednesday").val());
        jQuery("#completeTaskForm > #thursdayInput").val(jQuery("#input" + taskId + "Thursday").val());
        jQuery("#completeTaskForm > #fridayInput").val(jQuery("#input" + taskId + "Friday").val());
        jQuery("#completeTaskForm > #saturdayInput").val(jQuery("#input" + taskId + "Saturday").val());
        jQuery("#completeTaskForm").submit();
    }

    this.SpendPoints = function () {
        jQuery("#spendPointsForm > #pointsToSpend").val(jQuery("#pointsToSpendInput").val());
        jQuery("#spendPointsForm").submit();
    }
};
var Task = new function () {

    this.MakeEditable = function (prefixFilter, prefixIndex) {
        var jQueryFilter = "";

        if (prefixFilter != null) {
            jQueryFilter += "[name^=" + prefixFilter + "]";
        }

        jQuery(jQueryFilter + ".readonly").attr("disabled", "true");
        jQuery(jQueryFilter + ".hidden").hide();

        if (prefixIndex != null) {
            jQueryFilter = "[name^=" + prefixFilter + prefixIndex + "Element]";
        }

        jQuery(jQueryFilter + ".readonly").removeAttr("disabled");
        jQuery(jQueryFilter + ".hidden").show();
    };

    this.MakeReadonly = function (prefixFilter) {
        var jQueryFilter = "";

        if (prefixFilter != null) {
            jQueryFilter += "[name^=" + prefixFilter + "]";
        }

        jQuery(jQueryFilter + ".readonly").attr("disabled", "true");
        jQuery(jQueryFilter + ".hidden").hide();

    };

    this.HandleSelection = function (taskId) {
        this.MakeEditable("task", taskId);
    };

    this.Add = function (taskId) {
        jQuery("#addTaskForm > #addTaskName").val(jQuery("#newTaskName").val());
        jQuery("#addTaskForm > #addTaskPoints").val(jQuery("#newTaskPoints").val());
        jQuery("#addTaskForm > #addTaskMaxPerDay").val(jQuery("#newTaskMaxPerDay").val());
        jQuery("#addTaskForm").submit();
    };

    this.SaveEdit = function (taskId) {
        jQuery("#editTaskForm > #editTaskId").val(taskId);
        jQuery("#editTaskForm > #editTaskName").val(jQuery("#taskName" + taskId).val());
        jQuery("#editTaskForm > #editTaskPoints").val(jQuery("#taskPoints" + taskId).val());
        jQuery("#editTaskForm > #editTaskMaxPerDay").val(jQuery("#taskMaxPerDay" + taskId).val());
        jQuery('#editTaskForm').ajaxSubmit();
        this.MakeReadonly("task");
    };
};
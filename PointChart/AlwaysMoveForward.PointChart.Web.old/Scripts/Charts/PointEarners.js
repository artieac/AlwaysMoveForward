var PointEarner = new function () {

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

    this.HandleSelection = function (pointEarnerId) {
        this.MakeEditable("pointEarnerInput", pointEarnerId, "Element");
    };

    this.Add = function () {
        jQuery("#addPointEarnerForm > #firstName").val(jQuery("#newFirstName").val());
        jQuery("#addPointEarnerForm > #lastName").val(jQuery("#newLastName").val());
        jQuery("#addPointEarnerForm > #email").val(jQuery("#newEmail").val());
        jQuery("#addPointEarnerForm").submit();
    };

    this.SaveEdit = function (pointEarnerId) {
        jQuery("#editPointEarnerForm > #editPointEarnerId").val(pointEarnerId);
        jQuery("#editPointEarnerForm > #editFirstName").val(jQuery("#pointEarnerInput" + pointEarnerId + "ElementFirstName").val());
        jQuery("#editPointEarnerForm > #editLastName").val(jQuery("#pointEarnerInput" + pointEarnerId + "ElementLastName").val());
        jQuery("#editPointEarnerForm > #editEmail").val(jQuery("#pointEarnerInput" + pointEarnerId + "ElementEmail").val());
        jQuery("#editPointEarnerForm").ajaxSubmit(null);
    };
};
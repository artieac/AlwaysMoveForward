define('tableManager',
    ['jquery'],
    function ($) {
        var alertTest = function () {
            alert("foo");
        }

        var makeRowEditable = function (prefixFilter, prefixIndex, suffixFilter) {
            var jQueryFilter = "";

            if (prefixFilter != null) {
                jQueryFilter += "[name^=" + prefixFilter + "]";
            }

            $(jQueryFilter + ".readonly").attr("disabled", "true");
            $(jQueryFilter + ".hidden").hide();

            if (prefixIndex != null) {
                jQueryFilter = "[name^=" + prefixFilter + prefixIndex;

                if (suffixFilter != null) {
                    jQueryFilter += suffixFilter;
                }

                jQueryFilter += "]";
            }

            $(jQueryFilter + ".readonly").removeAttr("disabled");
            $(jQueryFilter + ".hidden").show();
        }

        var toggleRowItemEditMode = function (itemId) {
            makeRowEditable("input", itemId);
            $("#editButton" + itemId).hide();
            $("#saveButton" + itemId).show();
        }

        return {
            alertTest: alertTest,
            makeRowEditable: makeRowEditable,
            toggleRowItemEditMode: toggleRowItemEditMode
        }
    }
);

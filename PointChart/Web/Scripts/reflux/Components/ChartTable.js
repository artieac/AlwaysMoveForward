var React = require('react');

// Actions
var chartCollectionActions = require("../actions/chartCollectionActions");

var ChartRow = React.createClass({
    render: function () {
        var chartNodes = this.props.rowData.map(function (currentRow) {
            return (
                <tr>
                    <td>{currentRow.Name}</td>
                </tr>
            );
        });
        return (
            <tbody>
                {chartNodes}
            </tbody>
        );
    }    
});

var ChartTable = React.createClass({
    handleRefresh: function(){
        chartCollectionActions.updateChartCreatorCollection();
    },

    render: function() {
        return (
            <div>
                <div>
                    <table class="table table-striped">
                        <thead> 
                            <th>Name</th>
                        </thead>                    
                        <ChartRow rowData={this.props.chartData}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = ChartTable;
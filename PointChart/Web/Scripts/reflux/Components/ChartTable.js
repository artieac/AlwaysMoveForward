var React = require('react');
var Table = require('react-bootstrap').Table;

// Actions
var chartCollectionActions = require("../actions/chartCollectionActions");

var ChartRow = React.createClass({
    render: function () {
        var chartNodes = this.props.rowData.map(function (currentRow) {
            return (
                <tr>
                    <td>{currentRow.Name}</td>
                    <td>{currentRow.Tasks.length}</td>
                    <td>{currentRow.PointEarnerId}</td>
                    <td>{currentRow.PointsEarned}</td>
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
                    <Table striped bordered condensed hover>
                        <thead> 
                            <th>Name</th>
                            <th>Task Count</th>
                            <th>Point Earner</th>
                            <th>Points Earned</th>
                        </thead>                    
                        <ChartRow rowData={this.props.chartData}/>
                    </Table>
                </div>
            </div>
        );
    }
});

module.exports = ChartTable;
/// <reference path="../ChartSummaryTable/ChartSummaryRow.js" />
var React = require('react');

var ChartSummaryRow = React.createClass({
    handleEditClick: function() {
        window.location.href="/Chart/" + this.props.rowData.Id;
    },

    handleAddPointsClick: function() {
        window.location.href="/Home/CollectPoints/" + this.props.rowData.Id;
    },

    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Tasks.length}</td>
                <td>{this.props.rowData.PointEarner.FirstName + ' ' + this.props.rowData.PointEarner.LastName}</td>
                <td>0</td>
                <td>
                    <img src="/Content/images/paper_pencil.png" class="deleteList" alt="addTasks" onClick={this.handleEditClick} />
                    <img src="/Content/images/action_add.png" class="deleteList" alt="addPoints" onClick={this.handleAddPointsClick} />
                </td>
            </tr>
        );
}
});

var ChartSummaryTableBody = React.createClass({
    render: function () {
        return (
            <tbody>
                {this.props.tableBodyData.map(function (currentRow) {
                    return <ChartSummaryRow key={currentRow.Id} rowData={currentRow}/>
                    })}
            </tbody>
        );
    }
});

var ChartSummaryTable = React.createClass({
    handlNewChartClick: function(){
        location.href="/chart/-1";
    },

    render: function() {
        return (
            <div>
                <div>
                    <div>
                        
                    </div>
                    <table className="table table-striped table-bordered">
                        <thead> 
                            <th>Name</th>
                            <th>Task Count</th>
                            <th>Point Earner</th>
                            <th>Points Earned</th>
                            <th></th>
                        </thead>                    
                        <ChartSummaryTableBody tableBodyData={this.props.tableData}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = ChartSummaryTable;
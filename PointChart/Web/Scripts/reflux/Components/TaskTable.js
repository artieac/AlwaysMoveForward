var React = require('react');
var Table = require('react-bootstrap').Table;

// Actions
var taskActions = require("../actions/taskActions");

var TaskRow = React.createClass({
    render: function () {
        var taskNodes = this.props.rowData.map(function (currentRow) {
            return (
                <tr>
                    <td> </td>
                    <td>{currentRow.Name}</td>
                    <td>{currentRow.Points}</td>
                    <td>{currentRow.MaxAllowedDaily}</td>
                    <td></td>
                </tr>
            );
        });
        return (
            <tbody>
                {taskNodes}
            </tbody>
        );
    }    
});

var TaskTable = React.createClass({
    render: function() {
        return (
            <div>
                <div>
                    <Table striped bordered condensed hover>
                        <thead> 
                            <th width="1%">&nbsp;</th>
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                            <th>Action</th>
                        </thead>                    
                        <TaskRow rowData={this.props.tableData}/>
                    </Table>
                </div>
            </div>
        );
    }
});

module.exports = TaskTable;
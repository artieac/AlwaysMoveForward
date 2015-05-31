'use strict'
var React = require('react');
var TaskInputRow = require('./TaskInputRow');
var TaskRow = require('./TaskRow');

var TaskTableBody = React.createClass({
    render: function () {
        return (
            <tbody>
                {this.props.tableBodyData.map(function (currentRow) {
                    return <TaskRow key={currentRow.Id} rowData={currentRow}/>
                    })}
                <TaskInputRow/>
            </tbody>
        );
    }
});

var TaskTable = React.createClass({
    render: function() {
        return (
            <div>
                <div>
                    <table className="table table-hover table-bordered">
                        <thead> 
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                            <th>Action</th>
                        </thead>                    
                        <TaskTableBody tableBodyData={this.props.tableData}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = TaskTable;
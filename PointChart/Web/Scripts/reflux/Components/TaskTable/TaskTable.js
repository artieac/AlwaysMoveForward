var React = require('react');
var Table = require('react-bootstrap').Table;
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
                    <Table striped bordered condensed hover>
                        <thead> 
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                            <th>Action</th>
                        </thead>                    
                        <TaskTableBody tableBodyData={this.props.tableData}/>
                    </Table>
                </div>
            </div>
        );
    }
});

module.exports = TaskTable;
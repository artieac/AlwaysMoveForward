var React = require('react');
var Table = require('react-bootstrap').Table;
var TaskRow = require('./TaskRow');

var TaskSelectionTableBody = React.createClass({
    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined'){
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <tr><td>foo</td></tr>
                        })}               
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }

        
    }
});

var TaskSelectionTable = React.createClass({
    render: function() {
        return (
            <div>
                <div>
                    <Table striped bordered condensed hover>
                        <thead> 
                            <th>In Chart</th>
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                        </thead>                    
                        <TaskSelectionTableBody currentChart={this.props.currentChart} tableBodyData={this.props.tableData}/>
                    </Table>
                </div>
            </div>
        );
    }
});

module.exports = TaskSelectionTable;
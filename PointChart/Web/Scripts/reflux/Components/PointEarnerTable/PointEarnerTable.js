var React = require('react');
var Button = require('react-bootstrap').Button;
var Table = require('react-bootstrap').Table;
var Panel = require('react-bootstrap').Panel;
var TaskRow = require('./TaskRow');
var chartActions = require('../../actions/chartActions');

var PointEarnerTableBody = React.createClass({      
    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined'){                        
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <PointEarnerRow key={currentRow.Id} rowData={currentRow} />
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }

        
    }
});

var PointEarnerTable = React.createClass({   
    render: function() {
        return (
            <div>
                <Table striped bordered condensed hover>
                    <thead> 
                        <th width="5%">First Name</th>
                        <th width="20%">Last Name</th>
                        <th width="20%">Points Earned</th>
                        <th width="20%">Points Spent</th>
                        <td></td>
                    </thead>                    
                    <PointEarnerTableBody tableBodyData={this.props.tableData}/>
                </Table>
            </div>
        );
    }
});

module.exports = TaskSelectionTable;
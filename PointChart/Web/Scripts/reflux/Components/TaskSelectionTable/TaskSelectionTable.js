var React = require('react');
var Table = require('react-bootstrap').Table;
var TaskRow = require('./TaskRow');

var TaskSelectionTableBody = React.createClass({
    isInChart: function(currentRow){
        var retVal = false;
        
        if(typeof this.props.chartData !== 'undefined' &&
            typeof this.props.chartData.Tasks !== 'undefined'){
            for(var i = 0; i < this.props.chartData.Tasks.length; i++){
                if(this.props.chartData.Tasks[i].Id == currentRow.Id){
                    retVal = true;
                    break;
                }
            }
        }

        return retVal;
    },

    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined'){
            for(var i = 0; i < this.props.tableBodyData.length; i++){
                this.props.tableBodyData[i].isInChart = this.isInChart(this.props.tableBodyData[i]);
            }

            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <TaskRow key={currentRow.Id} rowData={currentRow} />
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
                            <th width="5%">In Chart</th>
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                        </thead>                    
                        <TaskSelectionTableBody chartData={this.props.chartData} tableBodyData={this.props.tableData}/>
                    </Table>
                </div>
            </div>
        );
    }
});

module.exports = TaskSelectionTable;
var React = require('react');
var Button = require('react-bootstrap').Button;
var Table = require('react-bootstrap').Table;
var Panel = require('react-bootstrap').Panel;
var TaskRow = require('./TaskRow');
var chartActions = require('../../actions/chartActions');

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
                        return <TaskRow chartData={this.props.chartData} key={currentRow.Id} rowData={currentRow} />
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }

        
    }
});

var TaskSelectionTable = React.createClass({
    getSelectedTasks: function() {
        var retVal = [];
        
        if(typeof this.props.tableData !== 'undefined'){
            for(var i = 0; i < this.props.tableData.length; i++){
                if(this.props.tableData[i].isInChart === true){
                    retVal[retVal.length] = this.props.tableData[i];
                }
            }
        }

        return retVal;
    },

    handleSaveClick: function() {
        var chartName = React.findDOMNode(this.refs.chartName).value;
        var pointEarnerId = React.findDOMNode(this.refs.pointEarnerId).value;

        var chart = chartActions.updateChart(
                        this.props.chartData.Id,             
                        chartName, 
                        pointEarnerId,
                        this.getSelectedTasks()
                    );
    },

    handleNameChange: function(event){
        this.props.chartData.Name = event.target.value;  
        this.forceUpdate();
    },
   
    render: function() {
        return (
            <div>
                <Panel>
                    <span>
                        <div style={this.nameDivStyle}>                            
                            <input type="text" ref="chartName" value={this.props.chartData.Name} onChange={this.handleNameChange}/>
                        </div>
                        <div style={this.pointEarnerDivStyle}>
                            <input type="text" ref="pointEarnerId" defaultValue={this.props.chartData.PointEarnerId} />
                        </div>
                        <Button bsStyle='primary' onClick={this.handleSaveClick}>Save</Button>
                    </span>
                </Panel>
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
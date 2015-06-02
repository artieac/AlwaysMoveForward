'use strict'
var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var GenericDropDown = require('../GenericDropDown');
var chartActions = require('../../actions/chartActions');

var TaskRow = React.createClass({    
    handleIsInChartChecked: function(event){  
        this.props.rowData.isInChart = event.target.checked;   
        this.forceUpdate();
    },

    render: function () {
        return (
            <tr>
                <td>
                    <input ref="isInChartCheckbox" type="checkbox" checked={this.props.isInChartMethod(this.props.rowData.Id)} onChange={this.handleIsInChartChecked}/>
                </td>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Points}</td>
                <td>{this.props.rowData.MaxAllowedDaily}</td>
            </tr>
        );
    }    
});

var TaskSelectionTableBody = React.createClass({   
    isInChart: function(taskId){
        var retVal = false;
        
        if(typeof this.props.chartData !== 'undefined' &&
            typeof this.props.chartData.Tasks !== 'undefined' && 
            this.props.chartData.Tasks !== null){
            for(var i = 0; i < this.props.chartData.Tasks.length; i++){
                if(this.props.chartData.Tasks[i].Id == taskId){
                    retVal = true;
                    break;
                }
            }
        }

        return retVal;
    },

    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined'){                        
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <TaskRow chartData={this.props.chartData} key={currentRow.Id} rowData={currentRow} isInChartMethod={this.isInChart}/>
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
    getPointEarnerList: function() {
        var retVal = [];

        for(var i = 0; i < this.props.pointEarners.length; i++){
            retVal[i] = this.props.pointEarners[i].PointEarner;
            retVal[i].Name = retVal[i].FirstName + ' ' + retVal[i].LastName;
        }

        console.log("PointEarnerList:" + JSON.stringify(retVal));
        return retVal;
    },

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

        var chart = chartActions.updateChart(
                        this.props.chartData.Id,             
                        chartName, 
                        this.props.chartData.PointEarner.Id,
                        this.getSelectedTasks()
                    );
    },

    handleNameChange: function(event){
        this.props.chartData.Name = event.target.value;  
        this.forceUpdate();
    },
   
    handleSelectedChange: function(selectedPointEarner){
        this.props.chartData.PointEarner = selectedPointEarner;
        this.forceUpdate();
    },

    render: function() {
        if(typeof this.props.chartData !== 'undefined' && this.props.chartData!==null){
            if(typeof this.props.chartData.PointEarner !== 'undefined' && this.props.chartData.PointEarner !== null){
                this.props.chartData.PointEarner.Name = this.props.chartData.PointEarner.FirstName + ' ' + this.props.chartData.PointEarner.LastName;
            }
        }

        return (
            <div>
                <div className="row">
                    <div className="col-md-3">                            
                        <input type="text" ref="chartName" value={this.props.chartData.Name} onChange={this.handleNameChange}/>
                    </div>
                    <div className="col-md-3">
                        <GenericDropDown ref="selectedPointEarner" listData={this.getPointEarnerList()} selected={this.props.chartData.PointEarner} onSelectedChange={this.handleSelectedChange} />
                    </div>
                    <div className="col-md-3">
                        <button type="button" className="btn btn-primary" onClick={this.handleSaveClick}>Save</button>
                    </div>
                </div>
                <div>
                    <table className="table table-striped table-bordered">
                        <thead> 
                            <th width="5%">In Chart</th>
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                        </thead>                    
                        <TaskSelectionTableBody chartData={this.props.chartData} tableBodyData={this.props.tableData}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = TaskSelectionTable;
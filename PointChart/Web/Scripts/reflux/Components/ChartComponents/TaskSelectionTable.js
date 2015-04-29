﻿var jQuery = require('jquery');
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
                    <input ref="isInChartCheckbox" type="checkbox" checked={this.props.rowData.isInChart} onChange={this.handleIsInChartChecked}/>
                </td>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Points}</td>
                <td>{this.props.rowData.MaxAllowedDaily}</td>
            </tr>
        );
    }    
});

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
        for(var i = 0; i < this.props.pointEarners.length; i++){
            this.props.pointEarners[i].Name = this.props.pointEarners[i].FirstName + ' ' + this.props.pointEarners[i].LastName;
        }

        if(typeof this.props.chartData !== 'undefined' && typeof this.props.chartData.PointEarner !== 'undefined'){
            this.props.chartData.PointEarner.Name = this.props.chartData.PointEarner.FirstName + ' ' + this.props.chartData.PointEarner.LastName;
        }

        return (
            <div>
                <div className="row">
                    <div className="col-md-3">                            
                        <input type="text" ref="chartName" value={this.props.chartData.Name} onChange={this.handleNameChange}/>
                    </div>
                    <div className="col-md-3">
                        <GenericDropDown ref="selectedPointEarner" listData={this.props.pointEarners} selected={this.props.chartData.PointEarner} onSelectedChange={this.handleSelectedChange} />
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
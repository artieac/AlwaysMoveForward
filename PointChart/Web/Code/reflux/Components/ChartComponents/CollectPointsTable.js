'use strict'
var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var moment = require('moment');
var completedTaskActions = require('../../actions/completedTaskActions');
var completedTaskStore = require('../../stores/completedTaskStore');

var CollectPointsRow = React.createClass({
    addCompletedTask: function(chartId, taskId, inputValueControl, completionDate){
        if(inputValueControl != null){
            completedTaskActions.addCompletedTask(chartId,
                                                    taskId, 
                                                  inputValueControl.value,
                                                  completionDate.year(),
                                                  completionDate.month() + 1,
                                                  completionDate.date());
        }
    },

    handleSaveClick : function(){
        var completedTasks = {
            chartId : this.props.chartId,
            taskId : this.props.rowData.Id,
            timesTaskCompleted: []
        };

        completedTasks.timesTaskCompleted[completedTasks.timesTaskCompleted.length] = this.getSundayTimesCompleted();
        completedTasks.timesTaskCompleted[completedTasks.timesTaskCompleted.length] = this.getMondayTimesCompleted();
        completedTasks.timesTaskCompleted[completedTasks.timesTaskCompleted.length] = this.getTuesdayTimesCompleted();
        completedTasks.timesTaskCompleted[completedTasks.timesTaskCompleted.length] = this.getWednesdayTimesCompleted();
        completedTasks.timesTaskCompleted[completedTasks.timesTaskCompleted.length] = this.getThursdayTimesCompleted();
        completedTasks.timesTaskCompleted[completedTasks.timesTaskCompleted.length] = this.getFridayTimesCompleted();
        completedTasks.timesTaskCompleted[completedTasks.timesTaskCompleted.length] = this.getSaturdayTimesCompleted();

        completedTaskActions.addCompletedTasks(completedTasks);
    },

    getCompletedTask: function(inputControl){
        var retVal = 0;

        if(inputControl != null){
            retVal = inputControl.value;
        }

        return retVal;
    },

    getSundayTimesCompleted: function(){
        var retVal = {
            TimesCompleted : this.getCompletedTask(React.findDOMNode(this.refs.sundayInput)),
            Year : this.props.columnDates[0].year(),
            Month: this.props.columnDates[0].month() + 1,
            Day: this.props.columnDates[0].date()
        };

        return retVal;
    },

    getMondayTimesCompleted: function(){
        var retVal = {
            TimesCompleted : this.getCompletedTask(React.findDOMNode(this.refs.mondayInput)),
            Year : this.props.columnDates[1].year(),
            Month: this.props.columnDates[1].month() + 1,
            Day: this.props.columnDates[1].date()
        };

        return retVal;
    },

    getTuesdayTimesCompleted: function(){
        var retVal = {
            TimesCompleted : this.getCompletedTask(React.findDOMNode(this.refs.tuesdayInput)),
            Year : this.props.columnDates[2].year(),
            Month: this.props.columnDates[2].month() + 1,
            Day: this.props.columnDates[2].date()
        };

        return retVal;
    },

    getWednesdayTimesCompleted: function(){
        var retVal = {
            TimesCompleted : this.getCompletedTask(React.findDOMNode(this.refs.wednesdayInput)),
            Year : this.props.columnDates[3].year(),
            Month: this.props.columnDates[3].month() + 1,
            Day: this.props.columnDates[3].date()
        };

        return retVal;
    },

    getThursdayTimesCompleted: function(){
        var retVal = {
            TimesCompleted : this.getCompletedTask(React.findDOMNode(this.refs.thursdayInput)),
            Year : this.props.columnDates[4].year(),
            Month: this.props.columnDates[4].month() + 1,
            Day: this.props.columnDates[4].date()
        };

        return retVal;
    },

    getFridayTimesCompleted: function(){
        var retVal = {
            TimesCompleted : this.getCompletedTask(React.findDOMNode(this.refs.fridayInput)),
            Year : this.props.columnDates[5].year(),
            Month: this.props.columnDates[5].month() + 1,
            Day: this.props.columnDates[5].date()
        };

        return retVal;
    },

    getSaturdayTimesCompleted: function(){
        var retVal = {
            TimesCompleted : this.getCompletedTask(React.findDOMNode(this.refs.saturdayInput)),
            Year : this.props.columnDates[6].year(),
            Month: this.props.columnDates[6].month() + 1,
            Day: this.props.columnDates[6].date()
        };

        return retVal;
    },

    getCompletedTasksForDate: function(columnNumber){
        var retVal = "0";

        var targetDate = this.props.columnDates[columnNumber].format("YYYY-MM-DDT00:00:00");

        if(typeof this.props.completedTaskData !== 'undefined'){
            var foundValue = this.props.completedTaskData[targetDate];

            if(typeof foundValue !== 'undefined'){
                retVal = foundValue.NumberOfTimesCompleted;
            }
        }

        return retVal;
    },

    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.Name}({this.props.rowData.Points})</td>
                <td><input ref="sundayInput" type="text" defaultValue={this.getCompletedTasksForDate(0)} size="5" /></td>
                <td><input ref="mondayInput" type="text" defaultValue={this.getCompletedTasksForDate(1)} size="5" /></td>
                <td><input ref="tuesdayInput" type="text" defaultValue={this.getCompletedTasksForDate(2)} size="5" /></td>
                <td><input ref="wednesdayInput" type="text" defaultValue={this.getCompletedTasksForDate(3)} size="5" /></td>
                <td><input ref="thursdayInput" type="text" defaultValue={this.getCompletedTasksForDate(4)} size="5" /></td>
                <td><input ref="fridayInput" type="text" defaultValue={this.getCompletedTasksForDate(5)} size="5" /></td>
                <td><input ref="saturdayInput" type="text" defaultValue={this.getCompletedTasksForDate(6)} size="5" /></td>
                <td><button type="button" className="btn btn-primary" onClick={this.handleSaveClick}>Save</button></td>
            </tr>
        );
    }    
});

var CollectPointsTableBody = React.createClass({  
    getCompletedTasks: function(currentRow){
        var retVal = null;

        if(typeof this.props.tableBodyData !== 'undefined'){
            retVal = this.props.tableBodyData[currentRow.Id];
        }

        return retVal;
    },

    render: function(){
        if(typeof this.props.chartData.Tasks !== 'undefined'){                        
            return (
                <tbody>
                    {this.props.chartData.Tasks.map(function (currentRow) {
                        return <CollectPointsRow chartId={this.props.chartData.Id} key={currentRow.Id} rowData={currentRow} columnDates={this.props.columnDates} completedTaskData={this.getCompletedTasks(currentRow)}/>
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }
    }    
});

var CollectPointsTable = React.createClass({
    mixins: [
        Reflux.connect(completedTaskStore, "allTasks")
    ],

    getInitialState: function() {
        return { 
            allTasks: {}
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(completedTaskStore, this.handleGetAllTasks);
        completedTaskActions.getByChartId(this.props.chartId, this.props.selectedDate.month() + 1, this.props.selectedDate.date(), this.props.selectedDate.year());        
    },
    
    handleGetAllTasks: function (updateMessage) {
        this.setState({allTasks: updateMessage});
    },

    render: function() {
        var momentDate = moment();
        
        if(typeof this.state.allTasks !== 'undefined' && typeof this.state.allTasks.Calendar !== 'undefined'){
            momentDate = moment(this.state.allTasks.Calendar.WeekStartDate);
        }

        var columnDates = [ momentDate.clone(),
                            momentDate.clone().add(1, "days"),
                            momentDate.clone().add(2, "days"),
                            momentDate.clone().add(3, "days"),
                            momentDate.clone().add(4, "days"),
                            momentDate.clone().add(5, "days"),
                            momentDate.clone().add(6, "days")];

        return (
            <div>
                <br/>
                <table className="table table-striped table-bordered">
                    <thead> 
                        <th width="20%">Task</th>
                        <th width="10%">{ columnDates[0].format("MM/DD/YYYY")}</th>
                        <th width="10%">{ columnDates[1].format("MM/DD/YYYY")}</th>
                        <th width="10%">{ columnDates[2].format("MM/DD/YYYY")}</th>
                        <th width="10%">{ columnDates[3].format("MM/DD/YYYY")}</th>
                        <th width="10%">{ columnDates[4].format("MM/DD/YYYY")}</th>
                        <th width="10%">{ columnDates[5].format("MM/DD/YYYY")}</th>
                        <th width="10%">{ columnDates[6].format("MM/DD/YYYY")}</th>
                        <th width="5%"></th>
                    </thead>                    
                    <CollectPointsTableBody columnDates={columnDates} chartData={this.props.chartData} tableBodyData={this.state.allTasks.CompletedTasks}/>
                </table>
            </div>
        );
    }
});

module.exports = CollectPointsTable;
'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var completedTaskActions = require("../actions/completedTaskActions");

var completedTaskStore = Reflux.createStore({
    listenables: [completedTaskActions],

    completedTasks: {},

    init: function () {
        this.completedTasks = {};
    },

    onGetByChartId: function (chartId, month, day, year) {
        this.completedTasks = {};
    
        jQuery.ajax({
            url: '/api/Chart/' + chartId + "/CompleteTask/" + year + "/" + month + "/" + day,
            async: false,
            dataType: 'json',
            success: function (restData) {
                console.log(restData);
                this.completedTasks = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger(this.completedTasks);
        return this.completedTasks;
    },

    onAddCompletedTask: function (chartId, targetTaskId, inputValue, completedYear, completedMonth, completedDay){
       var completedTaskData = {
           TaskId: targetTaskId,
           TimesCompleted: inputValue,
            Year: completedYear,
            Month: completedMonth,
            Day: completedDay
        };
        
        console.log(JSON.stringify(completedTaskData));

        jQuery.ajax({
            method: "POST",
            url: "/api/Chart/" + chartId + "/CompleteTask",
            data: JSON.stringify(completedTaskData),
            contentType: "application/json; charset=utf-8",
            success: function (restData) {
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
    },

    onAddCompletedTasks: function (completedTasks) {  
        console.log(JSON.stringify(completedTasks));

        jQuery.ajax({
            method: "POST",
            url: "/api/Chart/" + completedTasks.chartId + "/CompleteTasks",
            data: JSON.stringify(completedTasks),
            contentType: "application/json; charset=utf-8",
            success: function (restData) {
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
    }
});

module.exports = completedTaskStore;

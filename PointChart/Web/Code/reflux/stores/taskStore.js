'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var taskActions = require("../actions/taskActions");

var taskStore = Reflux.createStore({
    listenables: [taskActions],

    currentTask: {},
    allTasks: [],

    init: function () {
        this.allTasks = [];
    },

    onGetAllTasks: function () {
        this.allTasks = [];

        jQuery.ajax({
            url: '/api/Tasks',
            async: false,
            dataType: 'json',
            success: function (restData) {
                console.log(restData);
                this.allTasks = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger(this.allTasks);
        return this.allTasks;
    },

    onCreateTask: function (taskName, taskPoints, maxTimesPerDay) {

        var taskData = {
                    Name: taskName,
                    Points: taskPoints,
                    MaxPerDay: maxTimesPerDay
                
            };

        jQuery.ajax({
            method: "POST",
            url: "/api/Task",
            data: JSON.stringify(taskData),
            contentType: "application/json; charset=utf-8",
            success: function (restData) {
                console.log(restData);
                this.onGetAllTasks();
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
    },

    onUpdateTask: function(taskId, taskName, taskPoints, maxTimesPerDay){

        var taskData = {
            Name: taskName,
            Points: taskPoints,
            MaxPerDay: maxTimesPerDay
        };

        jQuery.ajax({
            method: "PUT",
            url: "/api/Task/" + taskId,
            data: JSON.stringify(taskData),
            contentType: "application/json; charset=utf-8",
            success: function (restData) {
                console.log(restData);
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
    }
});

module.exports = taskStore;

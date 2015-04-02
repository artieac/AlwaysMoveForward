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
    allTasks: {},

    init: function () {
        this.chartEarnerCollection = this.onGetAllTasks();
    },

    onGetAllTasks: function () {
        this.allTasks = {};

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

        this.trigger((this.allTasks || {}));
        return this.allTasks;
    },
});

module.exports = taskStore;

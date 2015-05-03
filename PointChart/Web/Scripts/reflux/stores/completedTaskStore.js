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
    }
});

module.exports = completedTaskStore;

'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var chartActions = require("../actions/chartActions");

var chartStore = Reflux.createStore({
    listenables: [chartActions],

    currentChart: {},

    init: function () {
        this.currentChart = {};
    },

    onGetChart: function (chartId) {
        this.currentChart = {};

        jQuery.ajax({
            url: '/api/Chart/' + chartId,
            async: false,
            dataType: 'json',
            success: function (restData) {
                console.log(restData);
                this.currentChart = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger(this.currentChart);
        return this.currentChart;
    },

    onUpdateChart: function (chartId, chartName, pointEarnerId, tasks) {
        var chartData = {
            Name: chartName,
            PointEarnerId: pointEarnerId,
            Tasks: tasks
        };

        jQuery.ajax({
            method: "PUT",
            url: "/api/Chart/" + chartId,
            data: JSON.stringify(chartData),
            contentType: "application/json; charset=utf-8",
            success: function (restData) {
                console.log(restData);
                this.onGetAllTasks();
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
    }
});

module.exports = chartStore;

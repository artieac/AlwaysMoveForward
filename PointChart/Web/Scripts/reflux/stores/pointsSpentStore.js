'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var pointsSpentActions = require("../actions/pointsSpentActions");

var pointsSpentStore = Reflux.createStore({
    listenables: [pointsSpentActions],

    pointsDetail: {},

    init: function () {
        this.pointsDetail = {};
    },

    onGetPointsDetail: function (chartId) {
        this.pointsDetail = {};

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

        this.trigger(this.pointsDetail);
        return this.pointsDetail;
    },

    onSpendPoints: function (chartId, chartName, pointEarnerId, tasks) {
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

module.exports = pointsSpentStore;

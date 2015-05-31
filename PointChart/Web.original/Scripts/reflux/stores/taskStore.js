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

    onUpdateChartCollection: function (userIdentifier) {
        var returnValue = {};

        jQuery.ajax({
            url: '/api/User/' + userIdentifier + 'Tasks',
            async: false,
            dataType: 'json',
            success: function (restData) {
                console.log(restData);
                returnValue = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
        return returnValue;
    },

    onUpdateChartCreatorCollection: function () {
        this.trigger((this.chartCreatedCollection = this.onUpdateChartCollection('creator') || {}));
        return this.chartCreatedCollection;
    },

    onUpdateChartEarnerCollection: function () {
        this.trigger((this.chartEarnerCollection = this.onUpdateChartCollection('pointEarner') || {}));
        return this.chartEarnerCollection;
    }
});

module.exports = chartCollectionStore;

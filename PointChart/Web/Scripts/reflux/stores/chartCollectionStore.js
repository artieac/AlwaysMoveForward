'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var chartCollectionActions = require("../actions/chartCollectionActions");

var defaultChartList = {
    Name: 'TestChart',
    Tasks: {
        Name: 'TestTask',
        Points: '1'
    }
};

var chartCollectionStore = Reflux.createStore({
    listenables: [chartCollectionActions],

    chartCollection: {},

    init: function() {
        this.chartCollection = defaultChartList;
    },

    getData: function () {
        return this.chartCollection;
    },

    onChartCollectionUpdated: function (chartCollection) {
        alert(chartCollection);
    },
    
});

module.exports = chartCollectionStore;

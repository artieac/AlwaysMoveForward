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
        this.chartCollection = this.getDefaultData();
    },

    updateChartCollection: function () {
        jQuery.ajax({
            url: '/api/Charts?chartRole=creator',
            async: false,
            dataType: 'json',
            success: function(chartData) {
                this.chartCollection = chartData;
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
        return this.chartCollection;
    },    
});

module.exports = chartCollectionStore;

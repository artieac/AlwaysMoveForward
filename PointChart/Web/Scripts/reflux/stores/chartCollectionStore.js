'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var chartCollectionActions = require("../actions/chartCollectionActions");

var chartCollectionStore = Reflux.createStore({
    listenables: [chartCollectionActions],

    chartCollection: {},

    init: function() {
        this.chartCollection = this.onUpdateChartCollection();
    },

    onUpdateChartCollection: function () {
        jQuery.ajax({
            url: '/api/Charts?chartRole=creator',
            async: false,
            dataType: 'json',
            success: function (chartData) {
                alert(chartData);
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

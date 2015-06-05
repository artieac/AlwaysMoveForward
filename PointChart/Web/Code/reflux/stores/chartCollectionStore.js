'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var chartCollectionActions = require("../actions/chartCollectionActions");

var chartCollectionStore = Reflux.createStore({
    listenables: [chartCollectionActions],

    chartCreatedCollection: {},
    chartEarnerCollection: {},

    init: function() {
        this.chartCreatedCollection = this.onUpdateChartCreatorCollection();
        this.chartEarnerCollection = this.onUpdateChartEarnerCollection();
    },

    onUpdateChartCollection: function (chartRole) {
        var chartRoleParam = 'chartRole=' + chartRole;
        var returnValue = {};

        jQuery.ajax({
            url: '/api/Charts?' + chartRoleParam,
            async: false,
            dataType: 'json',
            success: function (chartData) {
                console.log(chartData);
                returnValue = chartData;
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
        return returnValue;
    },

    onUpdateChartCreatorCollection: function () {
        this.chartCreatedCollection = this.onUpdateChartCollection('creator');
        this.trigger({ chartCreatedCollection: this.chartCreatedCollection, chartEarnerCollection: this.chartEarnerCollection });
        return this.chartCreatedCollection;
    },

    onUpdateChartEarnerCollection: function () {
        this.chartEarnerCollection = this.onUpdateChartCollection('pointEarner');
        this.trigger({ chartCreatedCollection: this.chartCreatedCollection, chartEarnerCollection: this.chartEarnerCollection });
        return this.chartEarnerCollection;
    }
});

module.exports = chartCollectionStore;

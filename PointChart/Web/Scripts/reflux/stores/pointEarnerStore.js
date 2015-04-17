'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var pointEarnerActions = require("../actions/pointEarnerActions");

var pointEarnerStore = Reflux.createStore({
    listenables: [pointEarnerActions],

    currentPointEarner: {},
    allPointEarners: [],

    init: function() {
        this.currentPointEarner = {};
        this.allPointEarners = [];
    },

    onGetAll: function () {
        this.allPointEarners = [];

        jQuery.ajax({
            url: '/api/PointEarners',
            async: false,
            dataType: 'json',
            success: function (restData) {
                console.log(restData);
                this.allPointEarners = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger((this.allPointEarners || []));
        return this.allPointEarners;
    },

    onFindPointEarnerByEmail: function (emailAddress) {
        this.currentPointEarner = {};

        jQuery.ajax({
            method: "GET",
            url: "/api/PointEarner?emailAddress=" + emailAddress,
            async: false,
            dataType: 'json',
            success: function (restData) {
                console.log(restData);
                this.currentPointEarner = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger((this.currentPointEarner || {}));
        return this.currentPointEarner;
    },

    onAddPointEarner: function (pointEarnerId) {
        var pointEarnerData = {
            PointEarnerId: pointEarnerId
        };

        jQuery.ajax({
            method: "POST",
            url: "/api/PointEarner",
            data: JSON.stringify(pointEarnerData),
            contentType: "application/json; charset=utf-8",
            success: function (restData) {
                console.log(restData);
                this.onGetAll();
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });
    }
});

module.exports = pointEarnerStore;

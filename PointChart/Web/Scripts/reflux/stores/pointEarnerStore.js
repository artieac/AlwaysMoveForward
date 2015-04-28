'use strict';

var Reflux = require('reflux');
var React = require('react');
var jQuery = require('jquery');
var _ = require('lodash');

// Actions
var pointEarnerActions = require("../actions/pointEarnerActions");

var pointEarnerStore = Reflux.createStore({
    listenables: [pointEarnerActions],

    allPointEarners: [],
    foundPointEarners: [],

    init: function() {
        this.allPointEarners = [];
        this.foundPointEarners = [];
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

        this.trigger({allPointEarners: this.allPointEarners, foundPointEarners: this.foundPointEarners});
        return this.allPointEarners;
    },

    onFindPointEarnersByEmail: function (emailAddress) {
        this.foundPointEarners = [];

        jQuery.ajax({
            method: "GET",
            url: "/api/PointEarner?emailAddress=" + emailAddress,
            async: false,
            dataType: 'json',
            success: function (restData) {
                console.log(restData);
                this.foundPointEarners = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger({ allPointEarners: this.allPointEarners, foundPointEarners: this.foundPointEarners });
        return this.foundPointEarners;
    },

    onAddPointEarner: function (pointEarner) {
        var pointEarnerData = {
            PointEarnerId: pointEarner.Id,
            OAuthServiceUserId: pointEarner.OAuthServiceUserId
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
    },

    onRemovePointEarner: function (pointEarner) {
        jQuery.ajax({
            method: "DELETE",
            url: "/api/PointEarner/" + pointEarner.Id,
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

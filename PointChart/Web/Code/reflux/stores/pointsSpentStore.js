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
    spentPoints: {},

    init: function () {
        this.pointsDetail = {};
        this.spentPoints = {};
    },

    onGetPointsDetail: function (pointEarnerId) {
        this.pointsDetail = {};

        jQuery.ajax({
            url: '/api/PointEarner/' + pointEarnerId + '/Points',
            async: false,
            dataType: 'json',
            success: function (restData) {
                this.pointsDetail = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger({ pointsDetail: this.pointsDetail, spentPoints: this.spentPoints });
        return this.pointsDetail;

    },

    onGetSpentPoints: function (pointEarnerId) {
        this.spentPoints = {};

        jQuery.ajax({
            url: '/api/PointEarner/' + pointEarnerId + '/SpentPoints',
            async: false,
            dataType: 'json',
            success: function (restData) {
                this.spentPoints = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger({ pointsDetail: this.pointsDetail, spentPoints: this.spentPoints });
        return this.spentPoints;

    },

    onSpendPoints: function (pointEarnerId, date, amountSpent, reason) {
        var spendPointsData = {
            DateSpent: date,
            AmountSpent: amountSpent,
            Description: reason
        };

        jQuery.ajax({
            method: "POST",
            url: "/api/PointEarner/" + pointEarnerId + '/SpentPoints',
            data: JSON.stringify(spendPointsData),
            contentType: "application/json; charset=utf-8",
            success: function (restData) {
                console.log(restData);
                this.pointsDetail = restData;
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(url, status, err.toString());
            }.bind(this)
        });

        this.trigger({ pointsDetail: this.pointsDetail, spentPoints: this.spentPoints });
        return this.pointsDetail;
    }
});

module.exports = pointsSpentStore;

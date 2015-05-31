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

        this.trigger((this.pointsDetail  || {}));
        return this.pointsDetail;

    },

    onSpendPoints: function (pointEarnerId, date, amountSpent, reason) {
        var spendPointsData = {
            Date: date,
            AmountSpent: amountSpent,
            Reason: reason
        };

        jQuery.ajax({
            method: "PUT",
            url: "/api/PointEarner/" + pointEarnerId + '/Points',
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

        this.trigger((this.pointsDetail || {}));
        return this.pointsDetail;
    }
});

module.exports = pointsSpentStore;

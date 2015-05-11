'use strict'
/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var pointsSpentActions = require('../../actions/pointsSpentActions');
var pointsSpentStore = require('../../stores/pointsSpentStore');

var PointsDetail = React.createClass({
    mixins: [
        Reflux.connect(pointsSpentStore, "pointsDetail")
    ],

    getInitialState: function() {
        return { 
            pointsDetail: {}
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(pointsSpentStore, this.updatePointsDetail);
        pointsSpentActions.getPointsDetail(this.props.chartId);
    },

    updatePointsDetail: function (updateMessage) {
        this.setState({pointsDetail: updateMessage.pointsDetail});
    },

    getOtherChartPointsEarned: function(){
        var retVal = 0.0;

        for(var i = 0; i < this.state.pointsDetail.PointsEarned.length; i++)
        {
            console.log(this.state.pointsDetail.PointsEarned[i]);
        }

        return retVal;
    },

    render: function(){
        return (
            <div>
                <div className="row">
                    <div className="col-md-3">
                        <span>Points Earned (this chart)</span>
                    </div>
                    <div className="col-md-3">
                        <span>Points Earned (other charts)</span>
                    </div>
                    <div className="col-md-3">
                        <span>Points Spent</span>
                    </div>
                    <div className="col-md-3">
                        <span>Total Points</span>
                    </div>
                </div>
                <div className="row">
                    <div className="col-md-3">
                        <span>{this.state.pointsDetail.PointsEarned[this.props.chartId]}</span>
                    </div>
                    <div className="col-md-3">
                        <span>{this.getOtherChartPointsEarned()}</span>
                    </div>
                    <div className="col-md-3">
                        <span>{this.state.pointsDetail.PointsSpent}</span>
                    </div>
                    <div className="col-md-3">
                        <span>Total Points</span>
                    </div>
                </div>
            </div>
        );
    }
});

module.exports = PointsDetail;
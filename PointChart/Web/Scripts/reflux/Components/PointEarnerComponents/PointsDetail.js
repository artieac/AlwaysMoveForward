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
        pointsSpentActions.getPointsDetail(this.props.pointEarnerId);
    },

    updatePointsDetail: function (updateMessage) {
        console.log(JSON.stringify(updateMessage));
        this.setState({pointsDetail: updateMessage});
    },

    getCurrentChartPointsEarned: function(){
        var retVal = 0.0;

        for(var chartId in this.state.pointsDetail.PointsEarned){
            if(chartId == this.props.chartId){
                retVal += this.state.pointsDetail.PointsEarned[chartId];
                break;
            }
        }

        return retVal;
    },

    getOtherChartPointsEarned: function(){
        var retVal = 0.0;

        for(var chartId in this.state.pointsDetail.PointsEarned){
            if(chartId != this.props.chartId){
                retVal += this.state.pointsDetail.PointsEarned[chartId];
            }
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
                        <span>{this.getCurrentChartPointsEarned()}</span>
                    </div>
                    <div className="col-md-3">
                        <span>{this.getOtherChartPointsEarned()}</span>
                    </div>
                    <div className="col-md-3">
                        <span>{this.state.pointsDetail.PointsSpent}</span>
                    </div>
                    <div className="col-md-3">
                        <span>{(this.getCurrentChartPointsEarned() + this.getOtherChartPointsEarned()) - this.state.pointsDetail.PointsSpent}</span>
                    </div>
                </div>
            </div>
        );
    }
});

module.exports = PointsDetail;
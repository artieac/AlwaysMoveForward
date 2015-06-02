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
        this.setState({pointsDetail: updateMessage.pointsDetail});
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
            <div className="row">
                <div className="col-md-9">
                    <div className="row">
                        <div className="col-md-3">Points Earned<br/>(this chart)</div>
                        <div className="col-md-3">Points Earned<br/>(other charts)</div>
                        <div className="col-md-3">Points Spent</div>
                        <div className="col-md-3">Total Points</div>
                    </div>
                    <div className="row">
                        <div className="col-md-3">{this.getCurrentChartPointsEarned()}</div>
                        <div className="col-md-3">{this.getOtherChartPointsEarned()}</div>
                        <div className="col-md-3">{this.state.pointsDetail.PointsSpent}</div>
                        <div className="col-md-3">{(this.getCurrentChartPointsEarned() + this.getOtherChartPointsEarned()) - this.state.pointsDetail.PointsSpent}</div>
                    </div>
                </div>
            </div>        
        );
    }
});

module.exports = PointsDetail;
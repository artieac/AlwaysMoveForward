/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var moment = require('moment');
var chartStore = require('../stores/chartStore');
var chartActions = require('../actions/chartActions');
var CollectPointsTable = require('../Components/ChartComponents/CollectPointsTable');
var CalendarControl = require('../Components/CalendarControl');
var PointsDetail = require('../Components/PointEarnerComponents/PointsDetail');

var CollectPointsApp = React.createClass({
    mixins: [
        Reflux.connect(chartStore, "currentChart")
    ],

    getInitialState: function() {
        console.log('get initial state');
        return { 
            currentChart: {}
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(chartStore, this.handleGetChart);
        chartActions.getChart(this.props.chartId);           
    },
    
    handleGetChart: function (updateMessage) {
        this.setState({currentChart: updateMessage});
    },

    getPointEarnerName: function(){
        var retVal = "";

        if(typeof this.state.currentChart !== 'undefined' &&
            typeof this.state.currentChart.PointEarner !== 'undefined'){
            retVal = this.state.currentChart.PointEarner.FirstName + ' ' + this.state.currentChart.PointEarner.LastName;
        }

        return retVal;
    },

    getMomentDate: function() {
        var retVal = null;

        if(typeof this.props.selectedDate !== 'undefined'){
            retVal = moment(this.props.selectedDate.getFullYear() + "-" + (this.props.selectedDate.getMonth() + 1) + "-" + this.props.selectedDate.getDate(), "YYYY-MM-DD");
        }

        return retVal;
    },

    getExportEmptyUrl: function(){
        return "/Export/Empty/" + this.state.currentChart.Id + "?fileType=excel";
    },

    getExportCompletedUrl: function(){
        return "/Export/Completed/" + this.state.currentChart.Id + "/" + this.props.selectedDate.getFullYear() + "/" + (this.props.selectedDate.getMonth() + 1) + "/" + (this.props.selectedDate.getDate()) + "?fileType=excel";
    },

    render: function(){
        return ( 
            <div>
                <div className="row">
                    <div className="col-md-9">
                        <div className="row">
                            <div className="col-md-6">Chart Name: {this.state.currentChart.Name}</div>
                            <div className="col-md-2">
                                <a href={this.getExportEmptyUrl()}><img src="/Content/images/paper_white.png" alt="Export Empty" /></a>
                                &nbsp;
                                <a href={this.getExportCompletedUrl()}><img src="/Content/images/download.png" alt="Download" /></a>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-8">Point Earner: {this.getPointEarnerName()}</div>
                        </div>
                        <br/>
                        <PointsDetail chartId={this.props.chartId} pointEarnerId={this.props.pointEarnerId}/>
                    </div>
                    <div className="col-md-3">
                        <CalendarControl selected={this.getMomentDate()} chartId={this.props.chartId}/>
                    </div>
                </div>
                <div>
                    <CollectPointsTable selectedDate={this.getMomentDate()} chartData={this.state.currentChart} chartId={this.props.chartId}/>
                </div>
            </div>
        );
    }
});

module.exports = CollectPointsApp;
React.render(<CollectPointsApp chartId={chartIdentifer} selectedDate={targetDate} pointEarnerId={pointEarnerId}/>, document.getElementById("collectPointsReactContent"));


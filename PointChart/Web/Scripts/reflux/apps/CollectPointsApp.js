/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var moment = require('moment');
var chartStore = require('../stores/chartStore');
var chartActions = require('../actions/chartActions');
var pointEarnerStore = require('../stores/chartStore');
var CollectPointsTable = require('../Components/ChartComponents/CollectPointsTable');
var CalendarControl = require('../Components/CalendarControl');

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

    render: function(){
        return ( 
            <div>
                <div className="row">
                    <div className="col-md-6">
                        <div>
                            <label>Chart Name: </label>
                            <label>{this.state.currentChart.Name}</label>
                        </div>
                        <div>
                            <label>Point Earner: </label>
                            <label>{this.getPointEarnerName()}</label>
                        </div>
                    </div>
                    <div className="col-md-4">
                        <CalendarControl selected={this.getMomentDate()} chartId={this.props.chartId}/>
                    </div>
                </div>
                <div>
                    <CollectPointsTable selectedDate={this.getMomentDate()} chartData={this.state.currentChart} />
                </div>
            </div>
        );
    }
});

module.exports = CollectPointsApp;
React.render(<CollectPointsApp chartId={chartIdentifer} selectedDate={targetDate}/>, document.getElementById("collectPointsReactContent"));


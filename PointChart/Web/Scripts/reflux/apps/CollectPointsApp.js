/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
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

    render: function(){
        return ( 
            <div>
                <div>
                    <CalendarControl selected={this.props.selectedDate}/>
                </div>
                <div>
                    <CollectPointsTable chartData={this.state.currentChart} />
                </div>
            </div>
        );
    }
});

module.exports = CollectPointsApp;
React.render(<CollectPointsApp chartId={chartIdentifer} selectedDate={selectedDate}/>, document.getElementById("collectPointsReactContent"));


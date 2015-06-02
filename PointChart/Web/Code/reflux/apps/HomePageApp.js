'use strict'
/** @jsx React.DOM */
var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var chartCollectionStore = require('../stores/chartCollectionStore');
var chartCollectionActions = require('../actions/chartCollectionActions');
var ChartSummaryTable = require('../Components/ChartComponents/ChartSummaryTable');

var HomePageApp = React.createClass({
    mixins: [
        Reflux.connect(chartCollectionStore, "chartCreatedCollection"),
        Reflux.connect(chartCollectionStore, "chartEarnerCollection"),
    ],

    getInitialState: function() {
        return { 
            chartCreatedCollection: [],
            chartEarnerCollection: []
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(chartCollectionStore, this.updateChartCreatedCollection);
        chartCollectionActions.updateChartCreatorCollection();
        this.listenTo(chartCollectionStore, this.updateChartEarnerCollection);
        chartCollectionActions.updateChartEarnerCollection();
    },

    updateChartCreatedCollection: function (updateMessage) {
        this.setState({chartCreatedCollection: updateMessage.chartCreatedCollection});
    },

    updateChartEarnerCollection: function (updateMessage) {
        this.setState({chartEarnerCollection: updateMessage.chartEarnerCollection});
    },

    render: function(){
        return ( 
            <div>
                <div>
                    <h2>Charts you Created</h2>
                    <ChartSummaryTable tableData={this.state.chartCreatedCollection} showNew={true}/> 
                </div>
                <div>
                    <h2>Charts you are assigned to</h2>
                    <ChartSummaryTable tableData={this.state.chartEarnerCollection} showNew={false}/> 
                </div>
            </div>
        );
    }
});

React.render(<HomePageApp />, document.getElementById("homePageReactContent"));

module.exports = HomePageApp;


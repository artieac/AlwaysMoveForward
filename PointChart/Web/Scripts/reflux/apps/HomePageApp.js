/** @jsx React.DOM */
var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var chartCollectionStore = require('../stores/chartCollectionStore');
var chartCollectionActions = require('../actions/chartCollectionActions');
var ChartTable = require('../Components/ChartTable');

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
        this.listenTo(chartCollectionStore, this.updateChartCollection);
        chartCollectionActions.updateChartCreatorCollection();
        this.listenTo(chartCollectionStore, this.updateChartCollection);
        chartCollectionActions.updateChartEarnerCollection();
    },

    updateChartCollection: function (updateMessage) {
        this.setState({chartCreatedCollection: updateMessage.chartCreatedCollection, chartEarnerCollection: updateMessage.chartEarnerCollection});
    },

    render: function(){
        return ( 
            <div>
                <div>
                    <h2>Charts you Created</h2>
                    <ChartTable chartData={this.state.chartCreatedCollection}/> 
                </div>
                <div>
                    <h2>Charts you are assigned to</h2>
                    <ChartTable chartData={this.state.chartEarnerCollection}/> 
                </div>
            </div>
        );
    }
});

React.render(<HomePageApp />, document.getElementById("homePageReactContent"));

module.exports = HomePageApp;


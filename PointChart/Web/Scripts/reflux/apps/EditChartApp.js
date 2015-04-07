/** @jsx React.DOM */
var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var chartStore = require('../stores/chartStore');
var chartActions = require('../actions/chartActions');
var ChartDetailTable = require('../Components/ChartDetailTable/ChartDetailTable.js');

var EditChartApp = React.createClass({
    mixins: [
        Reflux.connect(chartStore, "currentChart"),
    ],

    getInitialState: function() {
        return { 
            currentChart: []
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(chartStore, this.handleGetChart);
        chartActions.getChart(this.props.chartId);
    },

    handleGetChart: function (updateMessage) {
        this.setState({currentChart: updateMessage.currentChart});
    },

    render: function(){
        return ( 
            <div>
                <div>
                    <ChartDetailTable tableData={this.state.currentChart}/> 
                </div>
            </div>
        );
    }
});

module.exports = EditChartApp;
React.render(<EditChartApp chartId="6"/>, document.getElementById("editChartPageReactContent"));


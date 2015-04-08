/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var chartStore = require('../stores/chartStore');
var taskStore = require('../stores/taskStore');
var chartActions = require('../actions/chartActions');
var taskActions = require('../actions/taskActions');
var ChartPanelHeader = require('../Components/ChartPanelHeader');
var TaskSelectionTable = require('../Components/TaskSelectionTable/TaskSelectionTable');

var EditChartApp = React.createClass({
    mixins: [
        Reflux.connect(chartStore, "currentChart"),
        Reflux.connect(taskStore, "allTasks")
    ],

    getInitialState: function() {
        console.log('get initial state');
        return { 
            currentChart: {},
            allTasks: []
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(taskStore, this.handleGetAllTasks);
        taskActions.getAllTasks();

        this.listenTo(chartStore, this.handleGetChart);
        chartActions.getChart(this.props.chartId);
    },

    handleGetChart: function (updateMessage) {
        this.setState({currentChart: updateMessage});
    },

    handleGetAllTasks: function(updateMessage){        
        this.setState({allTasks: updateMessage});
    },

    render: function(){
        return ( 
            <div>
                <div>
                    <TaskSelectionTable chartData={this.state.currentChart} tableData={this.state.allTasks} />
                </div>
            </div>
        );
    }
});

module.exports = EditChartApp;
React.render(<EditChartApp chartId="6"/>, document.getElementById("editChartPageReactContent"));


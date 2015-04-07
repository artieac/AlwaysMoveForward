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
        this.setState({currentChart: updateMessage.currentChart});
    },

    handleGetAllTasks: function(updateMessage){        
        this.setState({allTasks: updateMessage.allTasks});
    },

    render: function(){
        return ( 
            <div>
                <div>
                    <ChartPanelHeader currentChart={this.state.currentChart} />
                    <TaskSelectionTable currentChart={this.state.currentChart} tableData={this.state.allTasks} />
                </div>
            </div>
        );
    }
});

module.exports = EditChartApp;
React.render(<EditChartApp chartId="6"/>, document.getElementById("editChartPageReactContent"));


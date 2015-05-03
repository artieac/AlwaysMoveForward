var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var moment = require('moment');
var completedTaskActions = require('../../actions/completedTaskActions');
var completedTaskStore = require('../../stores/completedTaskStore');

var CollectPointsRow = React.createClass({
    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Points}</td>
                <td>{this.props.rowData.MaxAllowedDaily}</td>
                <td><input type="text" refName="timesCompleted" defaultValue={this.props.rowData.TimesCompleted}/></td>
            </tr>
        );
    }    
});

var CollectPointsTableBody = React.createClass({  
    render: function () {
            return ( <tbody></tbody>);
    }
});

var CollectPointsTable = React.createClass({
    mixins: [
        Reflux.connect(completedTaskStore, "allTasks")
    ],

    getInitialState: function() {
        return { 
            allTasks: {}
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(completedTaskStore, this.handleGetAllTasks);
        completedTaskActions.getByChartId(6, this.props.selectedDate.month() + 1, this.props.selectedDate.date(), this.props.selectedDate.year());        
    },
    
    handleGetAllTasks: function (updateMessage) {
        this.setState({allTasks: updateMessage});
    },

    render: function() {
        var momentDate = moment();
        
        if(typeof this.state.allTasks !== 'undefined' && typeof this.state.allTasks.Calendar !== 'undefined'){
            momentDate = moment(this.state.allTasks.Calendar.WeekStartDate);
        }

        return (
            <div>
                <div>
                    <table className="table table-striped table-bordered">
                        <thead> 
                            <th width="20%">Task</th>
                            <th>{ momentDate.format("MM/DD/YYYY")}</th>
                            <th>{ momentDate.add(1, "days").format("MM/DD/YYYY")}</th>
                            <th>{ momentDate.add(1, "days").format("MM/DD/YYYY")}</th>
                            <th>{ momentDate.add(1, "days").format("MM/DD/YYYY")}</th>
                            <th>{ momentDate.add(1, "days").format("MM/DD/YYYY")}</th>
                            <th>{ momentDate.add(1, "days").format("MM/DD/YYYY")}</th>
                            <th>{ momentDate.add(1, "days").format("MM/DD/YYYY")}</th>
                        </thead>                    
                        <CollectPointsTableBody tableBodyData={this.state.allTasks.CompletedTasks}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = CollectPointsTable;
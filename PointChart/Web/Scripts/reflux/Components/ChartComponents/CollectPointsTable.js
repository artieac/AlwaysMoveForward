var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
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
        if(typeof this.props.tableBodyData !== 'undefined'){             
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <CollectPointsRow key={currentRow.Id} rowData={currentRow} />
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }        
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

    render: function() {
        return (
            <div>
                <div>
                    <table className="table table-striped table-bordered">
                        <thead> 
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                            <th width="20%">Times Completed</th>
                        </thead>                    
                        <CollectPointsTableBody tableBodyData={this.props.chartData.Tasks}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = CollectPointsTable;
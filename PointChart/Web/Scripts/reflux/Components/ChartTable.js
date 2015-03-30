var React = require('react');
var jQuery = require('jquery');

// Actions
var chartCollectionActions = require("../actions/chartCollectionActions");

var ChartRow = React.createClass({
    render: function () {
        return (
            <tbody>
                <tr>
                    <td>{this.props.Name}</td>
                </tr>
            </tbody>
        )
    }    
});

var ChartTable = React.createClass({
    handleRefresh: function(){
        chartCollectionActions.updateChartCollection();
    },

    render: function() {
        return (
            <div>
                <div>
                    <h3>buttondiv</h3>
                    <button onClick={this.handleRefresh}>Load</button>
                </div>
                <div>
                    <h3>tablediv</h3>
                    <table class="table table-striped">
                        <thead> 
                            <th>Name</th>
                        </thead>                    
                        <ChartRow Name="test"/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = ChartTable;
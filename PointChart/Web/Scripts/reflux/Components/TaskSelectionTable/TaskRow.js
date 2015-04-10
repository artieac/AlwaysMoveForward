var React = require('react');
var Button = require('react-bootstrap').Button;

// Actions
var taskActions = require("../../actions/taskActions");

var TaskRow = React.createClass({    
    handleIsInChartChecked: function(){        
        this.props.rowData.isInChart = React.findDOMNode(this.refs.isInChartCheckbox).value;   
    },

    isInChart: function(){
        var retVal = false;
        
        if(typeof this.props.chartData !== 'undefined' &&
            typeof this.props.chartData.Tasks !== 'undefined' &&
            typeof this.props.rowData !== 'undefined'){
            retVal = this.props.chartData.Tasks.some(function(task){
                if(task.Id == this.props.rowData.Id){
                    this.props.rowData.isInChart = 'on';
                    return true;
                }
            }.bind(this));
        }

        return retVal;
    },

    render: function () {
        this.isInChart();
        return (
            <tr>
                <td>
                    <input ref="isInChartCheckbox" type="checkbox" checked={this.props.rowData.isInChart} onChange={this.handleIsInChartChecked}/>
                </td>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Points}</td>
                <td>{this.props.rowData.MaxAllowedDaily}</td>
            </tr>
        );
    }    
});

module.exports = TaskRow;
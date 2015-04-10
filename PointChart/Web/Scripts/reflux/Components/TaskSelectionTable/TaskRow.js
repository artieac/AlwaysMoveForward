var React = require('react');
var Button = require('react-bootstrap').Button;

// Actions
var taskActions = require("../../actions/taskActions");

var TaskRow = React.createClass({    
    handleIsInChartChecked: function(event){  
        this.props.rowData.isInChart = event.target.checked;   
        this.forceUpdate();
    },

    render: function () {
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
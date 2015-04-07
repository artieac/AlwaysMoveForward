var React = require('react');
var Button = require('react-bootstrap').Button;

// Actions
var taskActions = require("../../actions/taskActions");

var TaskRow = React.createClass({    
    render: function () {
        return (
            <tr>
                <td>
                    <input type="checkbox" />
                </td>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Points}</td>
                <td>{this.props.rowData.MaxAllowedDaily}</td>
            </tr>
        );
    }    
});

module.exports = TaskRow;
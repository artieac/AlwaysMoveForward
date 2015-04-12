var React = require('react');
var Button = require('react-bootstrap').Button;

// Actions
var taskActions = require("../../actions/taskActions");

var PointEarnerRow = React.createClass({    
    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.FirstName}</td>
                <td>{this.props.rowData.LastName}</td>
                <td>{this.props.rowData.PointsEarned}</td>
                <td>{this.props.rowData.PointsSpent}</td>
            </tr>
        );
    }    
});

module.exports = TaskRow;
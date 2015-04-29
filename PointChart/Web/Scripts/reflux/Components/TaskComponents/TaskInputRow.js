var React = require('react');
var Button = require('react-bootstrap').Button;

// Actions
var taskActions = require("../../actions/taskActions");

var TaskInputRow = React.createClass({
    onAddTask: function() {
        taskActions.createTask(
            React.findDOMNode(this.refs.name).value, 
            React.findDOMNode(this.refs.points).value,
            React.findDOMNode(this.refs.maxPerDay).value);
    },

    render: function () {
        return (
            <tr>
                <td><input type="text" ref="name" defaultValue=''/></td>
                <td><input type="text" ref="points" defaultValue=''/></td>
                <td><input type="text" ref="maxPerDay" defaultValue=''/></td>
                <td>
                    <Button bsStyle='success' onClick={this.onAddTask}>Save</Button>
                </td>
            </tr>
        );
    }  
});

module.exports = TaskInputRow;
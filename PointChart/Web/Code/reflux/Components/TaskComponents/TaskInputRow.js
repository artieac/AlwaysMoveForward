'use strict'
var React = require('react');

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
                    <button type="button" className="btn btn-primary" onClick={this.onAddTask}>Save</button>
                </td>
            </tr>
        );
    }  
});

module.exports = TaskInputRow;
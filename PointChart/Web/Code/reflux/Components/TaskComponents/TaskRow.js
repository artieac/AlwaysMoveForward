'use strict'
var React = require('react');

// Actions
var taskActions = require("../../actions/taskActions");

var TaskRow = React.createClass({
    getInitialState: function() {
        return { showEditable: false };
    },

    handleOnClick: function() {
        this.setState({ showEditable: true });
    },

    handleSaveClick: function() {        
        this.setState({ showEditable: false });

        var newName = React.findDOMNode(this.refs.editableName).value;
        var newPoints = React.findDOMNode(this.refs.editablePoints).value;
        var newMaxPerDay = React.findDOMNode(this.refs.editableMaxPerDay).value;

        if(newName != this.props.rowData.Name ||
           newPoints != this.props.rowData.Points ||
           newMaxPerDay != this.props.rowData.MaxAllowedDaily)
        {
            taskActions.updateTask(
                this.props.rowData.Id,             
                newName, 
                newPoints,
                newMaxPerDay);
        }
    },

    render: function () {
        return (
            <tr onClick={this.handleOnClick}>
                <td>
                     { 
                         this.state.showEditable ? 
                             <input ref="editableName" type="text" defaultValue={this.props.rowData.Name}/> : 
                             <span ref="readOnlyName">{this.props.rowData.Name}</span>
                    }
                </td>
                <td>
                    { 
                        this.state.showEditable ? 
                            <input ref="editablePoints" type="text" defaultValue={this.props.rowData.Points}/> : 
                            <span ref="readOnlyPoints">{this.props.rowData.Points}</span>
                    }
                </td>
                <td>
                    { 
                        this.state.showEditable ? 
                            <input ref="editableMaxPerDay" type="text" defaultValue={this.props.rowData.MaxAllowedDaily}/> : 
                            <span ref="readOnlyMaxPerDay">{this.props.rowData.MaxAllowedDaily}</span>
                    }
                </td>
                <td>
                    { 
                        this.state.showEditable ? 
                            <button type="button" className="btn btn-primary" onClick={this.handleSaveClick}>Save</button> :
						    <img src="/Content/images/action_delete.png" class="deleteList" alt=""/>
				    }
                </td>
            </tr>
        );
    }    
});

module.exports = TaskRow;
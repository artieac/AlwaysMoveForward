var React = require('react');
var Button = require('react-bootstrap').Button;

// Actions
var taskActions = require("../../actions/taskActions");

var TaskRow = React.createClass({
    getInitialState: function() {
        return { showEditable: false };
    },

    handleOnClick: function() {
        this.setState({ showEditable: true });
    },

    handleOnMouseLeave: function() {
        this.setState({showEditable: false});
    },

    handleSaveClick: function() {
        taskActions.updateTask(
            this.props.rowData.Id,             
            React.findDOMNode(this.refs.editableName).value, 
            React.findDOMNode(this.refs.editablePoints).value,
            React.findDOMNode(this.refs.editableMaxPerDay).value);
    },

    render: function () {
        return (
            <tr onClick={this.handleOnClick} onMouseLeave={this.handleOnMouseLeave}>
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
                            <Button bsStyle='success' onClick={this.handleSaveClick}>Save</Button> : 
						    <img src="/Content/images/action_delete.png" class="deleteList" alt=""/>
				    }
                </td>
            </tr>
        );
    }    
});

module.exports = TaskRow;
var React = require('react');
var Reflux = require('reflux');
var Modal = require('react-bootstrap').Modal;
var ModalTrigger = require('react-bootstrap').ModalTrigger;
var Button = require('react-bootstrap').Button;
var TaskSelectionTable = require('./TaskSelectionTable/TaskSelectionTable');

var AddTaskModal = React.createClass({   
    onSaveClick: function(){
        this.props.onRequestHide();
    },

    onCloseClick: function(){
        this.props.onRequestHide();
    },

    render: function() {
        return (
            <Modal title='Add Task'
              bsStyle='primary'
              backdrop={false}>
              <div className='modal-body'>
                   <TaskSelectionTable currentChart={this.props.currentChart} tableData={this.props.allTasks} />
              </div>
              <div className='modal-footer'>
                <Button onClick={this.onCloseClick}>Close</Button>
                <Button bsStyle='primary' onClick={this.onSaveClick}>Save</Button>
              </div>
            </Modal>
        );
    }
});

var AddTaskModalTriger = React.createClass({
    render: function() {
        return (
            <ModalTrigger modal={<AddTaskModal currentChart={this.props.currentChart} allTasks={this.props.allTasks}/>}>
                <Button bsStyle='primary'>Add Tasks</Button>
            </ModalTrigger>
        );
    }
});

module.exports = AddTaskModalTriger;


/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var pointEarnerStore = require('../stores/pointEarnerStore');
var pointEarnerActions = require('../actions/pointEarnerActions');
var PointEarnerTable = require('../Components/PointEarnerTable/PointEarnerTable.js');

var PointEarnerManagementApp = React.createClass({
    mixins: [
        Reflux.connect(pointEarnerStore, "allPointEarners"),
        Reflux.connect(pointEarnerStore, "currentPointEarner")
    ],

    getInitialState: function() {
        return { 
            emailSearch: '',
            currentPointEarner: {},
            allPointEarners: []
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(pointEarnerStore, this.updatePointEarners);
        pointEarnerActions.getAll();

        this.listenTo(pointEarnerStore, this.updateCurrentPointEarner);
    },

    updatePointEarners: function (updateMessage) {
        this.setState({allPointEarners: updateMessage});
    },

    updateCurrentPointEarner: function(updateMessage) {
        this.setState({currentPointEarner: updateMessage});
    },

    handleEmailSearchClick: function(){
        var emailAddress = React.findDOMNode(this.refs.searchEmail).value;
        this.setState({emailSearch: emailAddress});
        pointEarnerActions.findPointEarnerByEmail(emailAddress);
    },

    handleAddPointEarnerClick: function(){
        pointEarnerActions.addPointEarner(this.state.currentPointEarner.Id);
    },

    render: function(){
        return ( 
            <div>
                <div className="row">
                    <div className="col-md-4">
                        <div className="panel panel-default">
                            <div className="panel-heading">
                                <h3 className="panel-title">Search</h3>
                            </div>
                            <div className="panel-body">
                                <div className="row">
                                    <div>                            
                                        <label for="searchEmail">Email:</label>
                                        <input type="text" id="searchEmail" ref="searchEmail" name="emailAddress" defaultValue={this.state.emailSearch} />
                                    </div>
                                </div>
                                <div className="row">
                                    <button className="btn btn-primary" onClick={this.handleEmailSearchClick}>Search</button>
                                </div>  
                                <br/>
                                <div className="row">                            
                                    <label for="firstName">First Name:</label>
                                    <span id="firstName">{this.state.currentPointEarner.FirstName}</span>
                                </div>
                                <div className="row">     
                                    <label for="lastName">Last Name:</label>
                                    <span id="lastName">{this.state.currentPointEarner.LastName}</span>
                                </div>
                                <div className="row">     
                                    <button className="btn btn-primary" onClick={this.handleAddPointEarnerClick}>Add</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-md-8">
                        <PointEarnerTable tableData={this.state.allPointEarners}/> 
                    </div>
                </div>
            </div>
        );
}
});

module.exports = PointEarnerManagementApp;
React.render(<PointEarnerManagementApp />, document.getElementById("pointEarnerManagerReactContent"));
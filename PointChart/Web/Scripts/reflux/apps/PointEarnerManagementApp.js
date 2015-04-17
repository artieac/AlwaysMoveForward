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
                    <div className="col-md-6">
                        <div>                            
                            <label for="searchEmail">Email</label>
                            <input type="text" id="searchEmail" ref="searchEmail" name="emailAddress" defaultValue={this.state.emailSearch} />
                        </div>
                        <button className="btn btn-primary" onClick={this.handleEmailSearchClick}>Search</button>
                    </div>
                </div>
                { this.state.currentPointEarner ?
                    <div className="row">
                        <div className="col-md-3">
                            <span>{this.state.currentPointEarner.FirstName}</span>
                        </div>
                        <div className="col-md-3">
                            <span>{this.state.currentPointEarner.LastName}</span>
                        </div>
                        <div className="col-md-3">
                            <button className="btn btn-primary" onClick={this.handleAddPointEarnerClick}>Add</button>
                        </div>
                    </div>
                        : null 
                }
                            
                <div>
                    <PointEarnerTable tableData={this.state.allPointEarners}/> 
                </div>
            </div>
        );
}
});

module.exports = PointEarnerManagementApp;
React.render(<PointEarnerManagementApp />, document.getElementById("pointEarnerManagerReactContent"));
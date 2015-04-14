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
    },

    updatePointEarners: function (updateMessage) {
        this.setState({allPointEarners: updateMessage.allPointEarners});
    },

    handleEmailSearchClick: function(){
        pointEarnerActions.findPointEarnerByEmail(this.state.searchEmail);
    },

    render: function(){
        return ( 
            <div>
                <div className="row">
                    <div className="col-md-6">
                        <div>                            
                            <label for="searchEmail">Email</label>
                            <input type="text" id="searchEmail" ref="searchEmail" defaultValue={this.state.emailSearch} />
                        </div>
                        <button className="btn btn-primary" onClick={this.handleEmailSearchClick}>Search</button>
                    </div>
                </div>
                { this.state.currentPointEarner ?
                    <div className="row">
                        <div className="col-md-6">
                            <span>{this.state.currentPointEarner.FirstName}</span>
                        </div>
                        <div className="col-md-6">
                            <span>{this.state.currentPointEarner.LastName}</span>
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
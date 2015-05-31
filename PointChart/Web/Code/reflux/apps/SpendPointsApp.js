'use strict'
/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var PointsSpentTable = require('../Components/PointEarnerComponents/PointsSpentTable');

var SpendPointsApp = React.createClass({
    mixins: [
    ],

    getInitialState: function() {
        return { 
        };
    },

    componentDidMount: function () {
    },
    
    handleGetChart: function (updateMessage) {
    },

    render: function(){
        return ( 
            <div>
                <div>
                    <PointsSpentTable pointEarnerId={this.props.pointEarnerId}/>
                </div>
            </div>
        );
    }
});

module.exports = SpendPointsApp;
React.render(<SpendPointsApp pointEarnerId={pointEarnerId}/>, document.getElementById("spendPointsReactContent"));


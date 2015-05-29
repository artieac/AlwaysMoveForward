/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var moment = require('moment');
var pointsSpentStore = require('../stores/pointsSpentStore');
var pointsSpentActions = require('../actions/pointsSpentActions');

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
            </div>
        );
    }
});

module.exports = SpendPointsApp;
React.render(<SpendPointsApp />, document.getElementById("spendPointsReactContent"));


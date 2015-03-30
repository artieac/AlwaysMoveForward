/** @jsx React.DOM */
var jQuery = require('jquery');
var React = require('react');
var Route = require('react-router');
var chartCollectionStore = require('../stores/chartCollectionStore');
var chartCollectionActions = require('../actions/chartCollectionActions');
var ChartTable = require('../Components/ChartTable');

var HomePageApp = React.createClass({
    getInitialState: function() {
        alert('here');
        chartCollectionActions.updateChartCollection();
        return { };
    },

    onUpdate: function(postData) {
        alert('in on update');
    },

    render: function(){
        return ( 
            <div>
                <ChartTable /> 
            </div>
        );
    }
});

React.render(<HomePageApp />, jQuery("#reactContent"));

module.exports = HomePageApp;
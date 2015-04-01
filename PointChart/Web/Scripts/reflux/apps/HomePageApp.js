/** @jsx React.DOM */
var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var chartCollectionStore = require('../stores/chartCollectionStore');
var chartCollectionActions = require('../actions/chartCollectionActions');
var ChartTable = require('../Components/ChartTable');

var HomePageApp = React.createClass({
    mixins: [Reflux.connect(chartCollectionStore, "chartCreatedCollection")],

    getInitialState: function() {
        return { chartCreatedCollection: []};
    },

    onUpdate: function(postData) {
        alert('in on update');
    },

    render: function(){
        return ( 
            <div>
                <ChartTable chartData={this.state.chartCreatedCollection}/> 
            </div>
        );
    }
});

React.render(<HomePageApp />, document.getElementById("reactContent"));

module.exports = HomePageApp;
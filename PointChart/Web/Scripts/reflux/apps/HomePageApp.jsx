/** @jsx React.DOM */
var React = require('react');
var Route = require('react-router');
var chartCollectionStore = require('../stores/chartCollectionStore');
var chartCollectionActions = require('../actions/chartCollectionActions');
var ChartTable = require('../Components/ChartTable.jsx');

var HomePageApp = React.createClass({
    getInitialState: function() {
        return { 
//            chartCollectionStore.getData();
        }
    },

    onUpdate: function(postData) {
        alert(postData);
    },

    render: function(){
        return ( <ChartTable /> );
    }
});

React.render(
<HomePageApp />,
document.getElementById("reactContent")
);

module.exports = HomePageApp;
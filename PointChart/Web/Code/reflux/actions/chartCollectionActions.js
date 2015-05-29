'use strict'
var Reflux = require('reflux');

// Define a set of actions to be triggered from the domains control panel

// Had to use object syntax vs array syntax due to https://github.com/spoike/refluxjs/issues/206

var chartCollectionActions = ({
    'updateChartCreatorCollection': {},
    'updateChartEarnerCollection': {}
});

module.exports = Reflux.createActions(chartCollectionActions);
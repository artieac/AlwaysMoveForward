/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var pointsSpentStore = require('../stores/pointsSpentStore');
var pointsSpentActions = require('../actions/pointsSpentActions');

var SpendPointsApp = React.createClass({
    mixins: [
        Reflux.connect(pointsSpentStore, "pointsDetail")
    ],

    getInitialState: function() {
        console.log('get initial state');
        return { 
            pointsDetail: []
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(pointsSpentStore, this.handleGetPointsDetail);
        pointsSpentActions.getPointsDetail(this.props.PointEarnerId);
    },
    
    handleGetPointsDetail: function (updateMessage) {
        this.setState({pointsDetail: updateMessage});
    },

    render: function(){
        return ( 
            <div>
                <div>
                    <div className="row">
                        <div>Point Earner: </div>
                    </div>
                    <div className="row">
                        <div class="col-md-3">
                            Points to Spend: <input type="text" ref="pointsToSpend" />
                        </div>
                        <div class="col-md-4">
                            Description: <input type="text" ref="spendingDescription" />
                        </div>
                    </div>
                </div>
            </div>
        );
}
});

module.exports = SpendPointsApp;
React.render(<SpendPointsApp pointEarnerId={pointEarnerId}/>, document.getElementById("pointsSpentReactContent"));


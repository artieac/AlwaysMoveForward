/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var pointEarnerStore = require('../stores/pointEarnerStore');
var pointEarnerActions = require('../actions/pointEarnerActions');
var PointEarnerTable = require('../Components/PointEarnerTable/PointEarnerTable.js');
var PointEarnerSearch = require('../Components/PointEarnerSearch/PointEarnerSearch.js');

var PointEarnerManagementApp = React.createClass({
    selectedPointEarners: [],

    mixins: [
        Reflux.connect(pointEarnerStore, "allPointEarners")
    ],

    getInitialState: function() {
        return { 
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

    onHandlePointEarnerSelection: function(pointEarner, isAdding){
        pointEarnerActions.addPointEarner(pointEarner);

        if(isAdding){
            var foundPointEarner = $.grep(this.selectedPointEarners, function(e){ return e.OAuthServiceUserId == pointEarner.OAuthServiceUserId; });         

            if(foundPointEarner != null){
                this.selectedPointEarners[this.selectedPointEarners.length] = pointEarner;
            }
        }
        else{
            for(var i = 0; i < this.selectedPointEarners.length; i++){
                if (this.selectedPointEarners[i].OAuthServiceUserId === pointEarner.OAuthServiceUserId) { 
                    this.selectedPointEarners.splice(i, 1);
                    break;
                }
            }
        }
    },

    render: function(){
        return ( 
            <div>
                <div className="row">
                    <div className="col-md-4">
                        <PointEarnerSearch handlePointEarnerSelection={this.onHandlePointEarnerSelection}/>
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
/** @jsx React.DOM */
var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var pointEarnerStore = require('../stores/pointEarnerStore');
var pointEarnerActions = require('../actions/pointEarnerActions');
var TaskTable = require('../Components/TaskTable/TaskTable.js');

var PointEarnerManagementApp = React.createClass({
    mixins: [
        Reflux.connect(pointEarnerStore, "allPointEarners"),
    ],

    getInitialState: function() {
        return { 
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

    render: function(){
        return ( 
            <div>
                <div className="row">
                    <Panel>
                        <div className={this.nameDivStyle}>                            
                            <input type="text" ref="searchEmail" value={this.props.chartData.Name} onChange={this.handleNameChange}/>
                        </div>
                        <div style={this.pointEarnerDivStyle}>
                            <input type="text" ref="pointEarnerId" defaultValue={this.props.chartData.PointEarnerId} />
                        </div>
                        <Button bsStyle='primary' onClick={this.handleSaveClick}>Save</Button>
                    </span>
                </div>
                <div>
                    <PointEarnerTable tableData={this.state.allPointEarners}/> 
                </div>
            </div>
        );
}
});

module.exports = PointEarnerManagementApp;
React.render(<PointEarnerManagementApp />, document.getElementById("pointEarnerManagerReactContent"));
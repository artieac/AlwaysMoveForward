'use strict'
var React = require('react');
var Reflux = require('reflux');
var pointsSpentStore = require('../../stores/pointsSpentStore');
var pointsSpentActions = require('../../actions/pointsSpentActions');

var SpentPointsInputRow = React.createClass({
    onSpendPoints: function() {
        pointsSpentActions.spendPoints(
            this.props.pointEarnerId,
            React.findDOMNode(this.refs.dateSpent).value, 
            React.findDOMNode(this.refs.amountSpent).value,
            React.findDOMNode(this.refs.reason).value);
    },

    render: function () {
        return (
            <tr>
                <td><input type="text" ref="dateSpent" defaultValue=''/></td>
                <td><input type="text" ref="amountSpent" defaultValue=''/></td>
                <td><input type="text" ref="reason" defaultValue=''/></td>
                <td>
                    <button type="button" className="btn btn-primary" onClick={this.onSpendPoints}>Save</button>
                </td>
            </tr>
        );
    }  
});

var PointsSpentRow = React.createClass({    
    handleRemoveClick: function(){
        this.props.handleRemoveSpentPoints(this.props.rowData.PointEarner);
    },

    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.DateSpent}</td>
                <td>{this.props.rowData.Amount}</td>
                <td>{this.props.rowData.Description}</td>
                <td>
                    <img src="/Content/images/action_delete.png" class="deleteList" alt="" onClick={this.handleRemoveClick} />
                </td>
            </tr>
        );
}    
});

var PointsSpentTableBody = React.createClass({      
    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined' && this.props.tableBodyData.constructor === Array){
            return (
                <tbody>
                    <SpentPointsInputRow pointEarnerId={this.props.pointEarnerId}/>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <PointsSpentRow key={currentRow.Id} rowData={currentRow} handleRemoveSpentPoints={this.props.handleRemoveSpentPoints}/>
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( 
                <tbody>
                    <SpentPointsInputRow pointEarnerId={this.props.pointEarnerId}/>
                </tbody>
            );
        }        
    }
});

var PointsSpentTable = React.createClass({   
    mixins: [
        Reflux.connect(pointsSpentStore, "spentPoints"),
    ],

    getInitialState: function() {
        return { 
            spentPoints: []
        };    
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(pointsSpentStore, this.updatePointsSpent);
        pointsSpentActions.getSpentPoints(this.props.pointEarnerId);
    },

    updatePointsSpent: function (updateMessage) {
        this.setState({spentPoints: updateMessage.spentPoints});
    },

    handleRemoveSpentPoints: function() {

    },

    render: function() {
        return (
            <div>
                <table className="table table-striped">
                    <thead> 
                        <th width="20%">Date</th>
                        <th width="20%">Amount</th>
                        <th width="20%">Description</th>
                        <th width="20%"></th>
                        <td></td>
                    </thead>                    
                    <PointsSpentTableBody tableBodyData={this.state.spentPoints} pointEarnerId={this.props.pointEarnerId} handleRemoveSpentPoints={this.props.handleRemoveSpentPoints}/>
                </table>
            </div>
        );
    }
});

module.exports = PointsSpentTable;
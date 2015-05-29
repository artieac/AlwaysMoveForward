'use strict'
/** @jsx React.DOM */
var React = require('react');
var Reflux = require('reflux');
var Route = require('react-router');
var pointEarnerStore = require('../../stores/pointEarnerStore');
var pointEarnerActions = require('../../actions/pointEarnerActions');

var PointEarnerSearchRow = React.createClass({    
    handleCheckboxSelection: function(){
        this.props.handlePointEarnerSelection(this.props.rowData);
    },

    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.FirstName}</td>
                <td>{this.props.rowData.LastName}</td>
                <td>
                    <button type="button" className="btn-small btn-primary" onClick={this.handleCheckboxSelection}>Add</button>
                </td>
            </tr>
        );
    }    
});

var PointEarnerSearchTableBody = React.createClass({      
    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined' && this.props.tableBodyData.constructor === Array){
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <PointEarnerSearchRow key={currentRow.OAuthServiceUserId} rowData={currentRow} handlePointEarnerSelection={this.props.handlePointEarnerSelection}/>
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }       
    }
});

var PointEarnerSearch = React.createClass({
    mixins: [
        Reflux.connect(pointEarnerStore, "foundPointEarners")
    ],

    getInitialState: function() {
        return { 
            emailSearch: '',
            foundPointEarners: []
        };
    },

    componentDidMount: function () {
        // Add event listeners in componentDidMount
        this.listenTo(pointEarnerStore, this.updateFoundPointEarners);
    },

    updateFoundPointEarners: function (updateMessage) {
        this.setState({foundPointEarners: updateMessage.foundPointEarners});
    },

    handleEmailSearchClick: function(){
        var emailAddress = React.findDOMNode(this.refs.searchEmail).value;
        this.setState({emailSearch: emailAddress});
        pointEarnerActions.findPointEarnersByEmail(emailAddress);
    },

    render: function(){
        return (
            <div className="panel panel-default">
                <div className="panel-heading">
                    <h3 className="panel-title">Search</h3>
                </div>
                <div className="panel-body">
                    <div className="row">
                        <label for="searchEmail">Email:</label>
                        <input type="text" id="searchEmail" ref="searchEmail" name="emailAddress" defaultValue={this.state.emailSearch} />
                        <button className="btn btn-primary" onClick={this.handleEmailSearchClick}>Search</button>
                    </div>  
                    <br/>
                    <div className="row">
                        <table className="table table-hover table-bordered">
                            <thead> 
                                <th width="10%">First Name</th>
                                <th width="10%">Last Name</th>
                                <th width="5%"/>
                            </thead>                    
                            <PointEarnerSearchTableBody tableBodyData={this.state.foundPointEarners} handlePointEarnerSelection={this.props.handlePointEarnerSelection}/>
                        </table>
                    </div>
                </div>
            </div>
        );
    }
});

module.exports = PointEarnerSearch;
'use strict'
var React = require('react');

var PointEarnerRow = React.createClass({    
    handleRemoveClick: function(){
        this.props.handleRemovePointEarner(this.props.rowData.PointEarner);
    },

    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.PointEarner.FirstName}</td>
                <td>{this.props.rowData.PointEarner.LastName}</td>
                <td>{this.props.rowData.PointsEarned}</td>
                <td><a href={'/Home/SpendPoints/' + this.props.rowData.PointEarner.Id}>{this.props.rowData.PointsSpent}</a></td>
                <td>
                    <img src="/Content/images/action_delete.png" class="deleteList" alt="" onClick={this.handleRemoveClick} />
                </td>
            </tr>
        );
    }    
});

var PointEarnerTableBody = React.createClass({      
    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined' && this.props.tableBodyData.constructor === Array){
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <PointEarnerRow key={currentRow.Id} rowData={currentRow} handleRemovePointEarner={this.props.handleRemovePointEarner}/>
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }

        
    }
});

var PointEarnerTable = React.createClass({   
    render: function() {
        return (
            <div>
                <table className="table table-striped">
                    <thead> 
                        <th width="20%">First Name</th>
                        <th width="20%">Last Name</th>
                        <th width="20%">Points Earned</th>
                        <th width="20%">Points Spent</th>
                        <td></td>
                    </thead>                    
                    <PointEarnerTableBody tableBodyData={this.props.tableData} handleRemovePointEarner={this.props.handleRemovePointEarner}/>
                </table>
            </div>
        );
    }
});

module.exports = PointEarnerTable;
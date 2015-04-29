var jQuery = require('jquery');
var React = require('react');
var Reflux = require('reflux');
var chartActions = require('../../actions/chartActions');

var CollectPointsRow = React.CreateClass({
    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Points}</td>
                <td>{this.props.rowData.MaxAllowedDaily}</td>
                <td><input type="text" refName="timesCompleted" defaultValue={this.props.rowData.TimesCompleted}/></td>
            </tr>
        );
    }    
});

var CollectPointsTableBody = React.createClass({  
    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined'){             
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <CollectPointsRow key={currentRow.Id} rowData={currentRow} />
                        }.bind(this))}              
                </tbody>
            );        
        }
        else{
            return ( <tbody></tbody>);
        }        
    }
});

var CollectPointsTable = React.createClass({
    handleSaveClick: function(){

    },

    render: function() {
        return (
            <div>
                <div className="row">
                    <div className="col-md-3">   
                        <label>{this.props.chartData.Name}</label>
                    </div>
                    <div className="col-md-3">
                        <label>{this.props.chartData.PointEarner.FirstName + ' ' + this.props.chartData.PointEarner.LastName}</label>
                    </div>
                    <div className="col-md-3">
                        <button type="button" className="btn btn-primary" onClick={this.handleSaveClick}>Save</button>
                    </div>
                </div>
                <div>
                    <table className="table table-striped table-bordered">
                        <thead> 
                            <th width="20%">Name</th>
                            <th width="20%">Points</th>
                            <th width="20%">Max Per Day</th>
                            <th width="20%">Times Completed</th>
                        </thead>                    
                        <CollectPointsTableBody chartData={this.props.chartData} tableBodyData={this.props.tableData}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = TaskSelectionTable;
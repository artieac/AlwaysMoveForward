var React = require('react');
var PointEarnerRow = require('./PointEarnerRow');

var PointEarnerTableBody = React.createClass({      
    render: function () {
        if(typeof this.props.tableBodyData !== 'undefined' && this.props.tableBodyData.constructor === Array){
            return (
                <tbody>
                    {this.props.tableBodyData.map(function (currentRow) {
                        return <PointEarnerRow key={currentRow.Id} rowData={currentRow} />
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
                    <PointEarnerTableBody tableBodyData={this.props.tableData}/>
                </table>
            </div>
        );
    }
});

module.exports = PointEarnerTable;
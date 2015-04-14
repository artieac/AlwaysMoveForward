var React = require('react');
var ChartDetailRow = require('./ChartDetailRow');

var ChartDetailTableBody = React.createClass({
    render: function () {
        return (
            <tbody>
                {this.props.tableBodyData.map(function (currentRow) {
                    return <ChartDetailRow key={currentRow.Id} rowData={currentRow}/>
                    })}
            </tbody>
        );
    }
});

var ChartDetailTable = React.createClass({
    render: function() {
        return (
            <div>
                <div>
                    <Table className="table table-striped">
                        <thead> 
                            <th>Name</th>
                            <th>Task Count</th>
                            <th>Point Earner</th>
                            <th>Points Earned</th>
                        </thead>                    
                        <ChartDetailTableBody tableBodyData={this.props.tableData}/>
                    </Table>
                </div>
            </div>
        );
    }
});

module.exports = ChartDetailTable;
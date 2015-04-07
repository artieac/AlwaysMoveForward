var React = require('react');
var Table = require('react-bootstrap').Table;
var Button = require('react-bootstrap').Button;
var ChartSummaryRow = require('./ChartSummaryRow');

var ChartSummaryTableBody = React.createClass({
    render: function () {
        return (
            <tbody>
                {this.props.tableBodyData.map(function (currentRow) {
                    return <ChartSummaryRow rowData={currentRow}/>
                    })}
            </tbody>
        );
    }
});

var ChartSummaryTable = React.createClass({
    handlNewChartClick: function(){
        location.href="/chart/-1";
    },

    render: function() {
        return (
            <div>
                <div>
                    <div>
                        
                    </div>
                    <Table striped bordered condensed hover>
                        <thead> 
                            <th>Name</th>
                            <th>Task Count</th>
                            <th>Point Earner</th>
                            <th>Points Earned</th>
                            <th></th>
                        </thead>                    
                        <ChartSummaryTableBody tableBodyData={this.props.tableData}/>
                    </Table>
                </div>
            </div>
        );
    }
});

module.exports = ChartSummaryTable;
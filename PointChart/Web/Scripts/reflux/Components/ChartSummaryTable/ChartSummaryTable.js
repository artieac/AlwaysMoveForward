var React = require('react');
var ChartSummaryRow = require('./ChartSummaryRow');

var ChartSummaryTableBody = React.createClass({
    render: function () {
        return (
            <tbody>
                {this.props.tableBodyData.map(function (currentRow) {
                    return <ChartSummaryRow key={currentRow.Id} rowData={currentRow}/>
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
                    <table className="table table-striped table-bordered">
                        <thead> 
                            <th>Name</th>
                            <th>Task Count</th>
                            <th>Point Earner</th>
                            <th>Points Earned</th>
                            <th></th>
                        </thead>                    
                        <ChartSummaryTableBody tableBodyData={this.props.tableData}/>
                    </table>
                </div>
            </div>
        );
    }
});

module.exports = ChartSummaryTable;
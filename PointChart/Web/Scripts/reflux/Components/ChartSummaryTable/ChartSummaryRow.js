var React = require('react');
var Table = require('react-bootstrap').Table;

var ChartSummaryRow = React.createClass({
    handleEditClick: function() {
        window.location.href="/Chart/" + this.props.rowData.Id;
    },

    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Tasks.length}</td>
                <td>{this.props.rowData.PointEarnerId}</td>
                <td>0</td>
                <td>
                    <img src="/Content/images/paper_pencil.png" class="deleteList" alt="" onClick={this.handleEditClick} />
                </td>
            </tr>
        );
    }
});

module.exports = ChartSummaryRow;
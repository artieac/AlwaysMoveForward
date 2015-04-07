var React = require('react');
var Table = require('react-bootstrap').Table;

var ChartDetailRow = React.createClass({
    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.Name}</td>
                <td>{this.props.rowData.Tasks.length}</td>
                <td>{this.props.rowData.PointEarnerId}</td>
                <td>0</td>
            </tr>
       );
    }    
});

module.exports = ChartDetailRow;
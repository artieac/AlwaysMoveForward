var React = require('react');

var PointEarnerRow = React.createClass({    
    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.FirstName}</td>
                <td>{this.props.rowData.LastName}</td>
                <td>{this.props.rowData.PointsEarned}</td>
                <td>{this.props.rowData.PointsSpent}</td>
            </tr>
        );
    }    
});

module.exports = PointEarnerRow;
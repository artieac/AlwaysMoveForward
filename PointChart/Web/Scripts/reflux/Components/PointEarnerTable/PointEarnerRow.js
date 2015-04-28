var React = require('react');

var PointEarnerRow = React.createClass({    
    handleRemoveClick: function(){
        this.props.handleRemovePointEarner(this.props.rowData);
    },

    render: function () {
        return (
            <tr>
                <td>{this.props.rowData.FirstName}</td>
                <td>{this.props.rowData.LastName}</td>
                <td>{this.props.rowData.PointsEarned}</td>
                <td>{this.props.rowData.PointsSpent}</td>
                <td>
                    <img src="/Content/images/action_delete.png" class="deleteList" alt="" onClick={this.handleRemoveClick} />
                </td>
            </tr>
        );
    }    
});

module.exports = PointEarnerRow;
var React = require('react');
var jQuery = require('jquery');

var ChartRow = React.createClass({
    render: function () {
        return (
            <tbody>
                <tr>
                    <td>{this.props.Name}</td>
                </tr>
            </tbody>
        )
    }    
});

var ChartTable = React.createClass({
    render: function() {
        return (
            <div>
                <table class="table table-striped">
                    <thead> 
                        <th>Name</th>
                    </thead>                    
                    <ChartRow Name="test"/>
                </table>
            </div>
        );
    }
});

React.render(
<ChartTable />,
document.getElementById("reactContent")
);
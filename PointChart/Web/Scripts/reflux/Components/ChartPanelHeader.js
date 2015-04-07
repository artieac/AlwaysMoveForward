var React = require('react');
var Panel = require('react-bootstrap').Panel;

var ChartPanelHeader = React.createClass({
    nameDivStyle: {width: '50%', display: 'block'},
    pointEarnerDivStyle: {width: '50%', display: 'float-right', marginleft: '50%'},

    render: function () {
        return (
            <div>
                <Panel>
                    <span style={this.nameDivStyle}>
                        <input type="text" name="chartName" defaultValue={this.props.currentChart.Name}/>
                    </span>
                    <span style={this.pointEarnerDivStyle}>
                        <input type="text" name="pointEarnerId"/>
                    </span>
                </Panel>
            </div>
        );
    }
});

module.exports = ChartPanelHeader;
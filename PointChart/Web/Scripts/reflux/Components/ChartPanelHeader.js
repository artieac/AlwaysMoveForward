var React = require('react');
var Panel = require('react-bootstrap').Panel;
var Button = require('react-bootstrap').Button;

var ChartPanelHeader = React.createClass({
    nameDivStyle: {width: '50%', display: 'block'},
    pointEarnerDivStyle: {width: '50%', display: 'float-right', marginleft: '50%'},

    render: function () {
        return (
            <div>
                <Panel>
                    <span>
                        <div style={this.nameDivStyle}>                            
                            <input type="text" name="chartName" defaultValue={this.props.chartData.Name}/>
                        </div>
                        <div style={this.pointEarnerDivStyle}>
                            <input type="text" name="pointEarnerId"/>
                        </div>
                        <Button bsStyle='primary'>Save</Button>
                    </span>
                </Panel>
            </div>
        );
    }
});

module.exports = ChartPanelHeader;
'use strict'
var React = require('react');

var GenericDropDown = React.createClass({
	getInitialState: function() {
		return {
			listVisible: false
		};
	},
	
	select: function(item) {
		this.props.selected = item;
		this.props.onSelectedChange(item);
	},
				
	show: function() {
		this.setState({ listVisible: true });
		document.addEventListener("click", this.hide);
	},
				
	hide: function() {
		this.setState({ listVisible: false });
		document.removeEventListener("click", this.hide);
	},
			
	render: function() {
		var selectionText = "";

		if(typeof this.props.selected !== 'undefined' && this.props.selected !== null){
			selectionText = this.props.selected.Name;
		}

		return (
			<div>
				<a href="#" className="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">
					<span refName="currentSelection">{selectionText}</span>
					<span className="caret"></span>
				</a>
				<ul className="dropdown-menu" id="dropdown">
					{this.renderListItems()}
				</ul>
			</div>
		);
	},			

	renderListItems: function() {
		var items = [];
		if(typeof this.props.listData !== 'undefined'){
			for (var i = 0; i < this.props.listData.length; i++) {
				var item = this.props.listData[i];
				items.push(<li onClick={this.select.bind(null, item)}>
					<span>{item.Name}</span>
				</li>);
			}
		}

		return items;
	}
});
			
module.exports = GenericDropDown;

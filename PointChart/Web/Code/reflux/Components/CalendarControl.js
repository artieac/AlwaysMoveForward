'use strict'
var React = require('react');
var Moment = require('moment');

var WeekDates = React.createClass({
    generateUrl: function(dayItem){
        return "/Home/CollectPoints/" + this.props.chartId
            + "/" + dayItem.date.year()
            + "/" + (dayItem.date.month() + 1)
            + "/" + dayItem.number;
    },

    isCurrentWeek: function(weekStartDate) {
        var retVal = false;

        var weekEndDate = weekStartDate.clone();
        weekEndDate = weekEndDate.add(7, "days");

        if(this.props.selected >= weekStartDate && this.props.selected <= weekEndDate){
            retVal = true;
        }

        return retVal;
    },

    render: function() {
        var days = [],
            date = this.props.date,
            month = this.props.month;

        var isCurrentWeekCheck = this.isCurrentWeek(this.props.date);

        for (var i = 0; i < 7; i++) {
            var day = {
                name: date.format("dd").substring(0, 1),
                number: date.date(),
                isCurrentMonth: date.month() === month.month(),
                isToday: date.isSame(new Date(), "day"),                
                date: date
            };
            days.push(
                <td key={day.date.toString()} className={"weekDayCell" + (day.isToday ? " weekDayCellCurrentDay" : " weekDayCell") + (isCurrentWeekCheck ? " weekDayCellCurrentWeek" : " ") + (day.isCurrentMonth ? " weekDayCellThisMonth" : " weekdayCellOtherMonth") + (day.date.isSame(this.props.selected) ? " weekDayCellCurrentDay" : "")}>
                    <a href={this.generateUrl(day)}>{day.number}</a>    
                </td>);
            date = date.clone();
            date.add(1, "d");
        }

        return( 
            <tr className="weekRow" key={days[0].toString()}>
                {days}
            </tr>);
    }
});

var CalendarControl = React.createClass({
    getInitialState: function() {
        return {
            month: this.props.selected.clone()
        };
    },

    changeMonth: function(){
        var newLocation = "/Home/CollectPoints/" + this.props.chartId 
                    + "/" + this.state.month.year()
                    + "/" + (this.state.month.month() + 1);

        window.location.href = newLocation;
    },

    previous: function() {
        var month = this.state.month;
        month.add(-1, "M");
        this.setState({ month: month });

        this.changeMonth();
    },

    next: function() {
        var month = this.state.month;
        month.add(1, "M");
        this.setState({ month: month });

        this.changeMonth();
    },

    render: function() {
        return ( 
            <div className="contentSection">
                <div className="monthTitle">
                    <span className="changeMonth" onClick={this.previous}> &lt; </span>
                    {this.renderMonthLabel()}
                    <span className="changeMonth" onClick={this.next}> &gt; </span>
                </div>
                <table className="calendarDates">
                    {this.renderWeeks()}                
                </table>
                </div>);
    },

    renderWeeks: function() {
        var weeks = [],
            done = false,
            date = this.state.month.clone().startOf("month").add("w" -1).day("Sunday"),
            monthIndex = date.month(),
            count = 0;

        while (!done) {
            weeks.push(<WeekDates key={date.toString()} date={date.clone()} month={this.state.month} selected={this.props.selected} chartId={this.props.chartId}/>);
            date.add(1, "w");
            done = count++ > 2 && monthIndex !== date.month();
            monthIndex = date.month();
        }

        return weeks;
    },

    renderMonthLabel: function() {
        return <span>{this.state.month.format("MMMM, YYYY")}</span>;
    }
});
            
module.exports = CalendarControl;

/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
	fs = require("fs"),
	less = require("gulp-less");

gulp.task("less", function () {	
	gulp.src('Styles/styles.less')
		.pipe(less())
		.pipe(gulp.dest('wwwroot/css'));
	gulp.src('Styles/Navbar.less')
		.pipe(less())
		.pipe(gulp.dest('wwwroot/css'));
	gulp.src('Styles/Admin.less')
		.pipe(less())
		.pipe(gulp.dest('wwwroot/css'));
	return;
});
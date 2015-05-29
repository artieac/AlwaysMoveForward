var gulp = require('gulp');
var react = require('gulp-react');
var config = require('../config');

gulp.task('transform', [''], function () {
    gulp.src(config.filePaths.appSource)
      .pipe(react())
      .pipe(gulp.dest(config.filePaths.sourceDestination));
});
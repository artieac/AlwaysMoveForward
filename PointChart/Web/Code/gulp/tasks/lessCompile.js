var gulp = require('gulp');
var config = require('../config');
var Less = require('gulp-less');

gulp.task('lessCompile', function () {
    return gulp.src(config.lessPaths.srcPath)
      .pipe(Less())
      .pipe(gulp.dest(config.lessPaths.destPath));
});
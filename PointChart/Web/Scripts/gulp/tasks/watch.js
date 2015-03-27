/* Notes:
   - gulp/tasks/browserify.js handles js recompiling with watchify
   - gulp/tasks/browserSync.js watches and reloads compiled files
*/

var gulp = require('gulp');
var config = require('../config');
var react = require('gulp-react');

gulp.task('transform', function () {
    gulp.src(config.filePaths.appSource)
      .pipe(react())
      .pipe(gulp.dest(config.filePaths.sourceDestination));
});

gulp.task('watch', ['transform'], function () {
    gulp.watch(config.watch.appSource);
});
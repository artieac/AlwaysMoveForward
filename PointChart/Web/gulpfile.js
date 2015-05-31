//var gulp = require('./Scripts/gulp')([
//    'vendor',
//]);

//// Actions
////var contactCollectionActions = require("../actions/contactCollectionActions");
////var trackingActions = require("../actions/trackingActions");
////var applicationActions = require("../actions/applicationActions");

//// Stores
////var contactCollectionStore = require("../stores/contactCollectionStore");
////var countryInfoStore = require("../stores/countryInfoStore");
////var notificationStore = require("../stores/notificationStore");

//var paths = {
//    scripts: ['./node_modules/less/dist/*.js', './node_modules/react/dist/*.js'],
//};

//// Not all tasks need to use streams 
//// A gulpfile is just another node program and you can use all packages available on npm 
//gulp.task('clean', function (cb) {
//    // You can use multiple globbing patterns as you would with `gulp.src` 
//    del(['build'], cb);
//});

//gulp.task('scripts', ['clean'], function () {
//    // Minify and copy all JavaScript (except vendor scripts) 
//    // with sourcemaps all the way down 
//    return gulp.src(paths.scripts)
//      .pipe(sourcemaps.init())
//        .pipe(uglify())
//        .pipe(concat('gulpscripts.min.js'))
//      .pipe(sourcemaps.write())
//      .pipe(gulp.dest('Scripts/gulp'));
//});

//// Rerun the task when a file changes 
//gulp.task('watch', function () {
//    gulp.watch(paths.scripts, ['scripts']);
//});

//// The default task (called when you run `gulp` from cli) 
//gulp.task('default', ['watch', 'scripts']);

//gulp.task('build', ['browserify']);

var gulp = require('gulp');
var browserify = require('./Code/gulp/tasks/browserify');
var watchify = require('./Code/gulp/tasks/watchify');
var lessCompile = require('./Code/gulp/tasks/lessCompile');

gulp.task('build', ['lessCompile', 'browserify']);
gulp.task('default', ['build', 'watchify']);

//var gulp = require('./Scripts/gulp')([
//    'browserify',
//    'watch'
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
var uglify = require('gulp-uglify');
var source = require('vinyl-source-stream');
var browserify = require('browserify');
var watchify = require('watchify');
var reactify = require('reactify');
var streamify = require('gulp-streamify');

var path = {
    HTML: 'src/index.html',
    MINIFIED_OUT: 'build.min.js',
    OUT: 'build.js',
    DEST: './Scripts/dist',
    DEST_BUILD: './Scripts/dist/build',
    DEST_SRC: './Scripts/dist/src',
    ENTRY_POINT: './Scripts/reflux/apps/HomePageApp.jsx'
};

gulp.task('copy', function () {
    gulp.src(path.HTML)
      .pipe(gulp.dest(path.DEST));
});

gulp.task('watch', function () {
    gulp.watch(path.HTML, ['copy']);

    var watcher = watchify(browserify({
        entries: [path.ENTRY_POINT],
        transform: [reactify],
        debug: true,
        cache: {}, packageCache: {}, fullPaths: true
    }));

    return watcher.on('update', function () {
        watcher.bundle()
          .pipe(source(path.OUT))
          .pipe(gulp.dest(path.DEST_SRC))
        console.log('Updated');
    })
      .bundle()
      .pipe(source(path.OUT))
      .pipe(gulp.dest(path.DEST_SRC));
});

gulp.task('build', function () {
    browserify({
        entries: [path.ENTRY_POINT],
        transform: [reactify],
    })
      .bundle()
      .pipe(source(path.MINIFIED_OUT))
      .pipe(streamify(uglify(path.MINIFIED_OUT)))
      .pipe(gulp.dest(path.DEST_BUILD));
});

gulp.task('production', ['build']);

gulp.task('default', ['watch']);
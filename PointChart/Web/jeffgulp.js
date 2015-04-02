var gulp = require('gulp');

var serverInputPaths = ['./Content/Scripts/app/components/controlPanelIndex.jsx', './Content/Scripts/app/components/contactCollectionIndex.jsx'];
var serverOutputNames = {
    'controlPanelIndex': 'controlPanelApp-server',
    'contactCollectionIndex': 'contactCollectionApp-server'
};
var clientPaths = ['./Content/Scripts/app/controlPanelApp.js', './Content/Scripts/app/contactCollectionApp.js']
var bundlesDestination = './Content/Scripts/dist';
var paths = [{
    src: {
        jsx: './Content/Scripts/app/components/*.jsx',
        app: './Content/Scripts/app/controlPanelApp.js',
        serverApp: './Content/Scri`pts/app/components/controlPanelIndex.jsx',
        scripts: './Content/Scripts/app/**/*.js'
    },
    dest: {
        bundles: './Content/Scripts/dist',
        bundlesFilter: '!./Content/Scripts/dist/**/*.js',
        serverBundle: 'controlPanelApp-server.js',
        clientBundle: 'controlPanelApp.js',
        jsx: './Content/Scripts/app/components'
    }
}, {
    src: {
        jsx: './Content/Scripts/app/components/*.jsx',
        app: './Content/Scripts/app/contactCollectionApp.js',
        serverApp: './Content/Scripts/app/components/contactCollectionIndex.jsx',
        scripts: './Content/Scripts/app/**/*.js'
    },
    dest: {
        bundles: './Content/Scripts/dist',
        bundlesFilter: '!./Content/Scripts/dist/**/*.js',
        serverBundle: 'contactCollectionApp-server.js',
        clientBundle: 'contactCollectionApp.js',
        jsx: './Content/Scripts/app/components'
    }
}];

var path = require('path');
var rename = require('gulp-rename');
var gutil = require('gulp-util');
var source = require('vinyl-source-stream');
var transform = require('vinyl-transform');
var stream = require('stream');
var streamify = require('gulp-streamify');
var footer = require('gulp-footer');
var os = require('os');
var del = require('del');
var reactify = require('reactify');
var browserSync = require('browser-sync');
var _ = require('lodash');
var exorcist = require('exorcist');
var buffer = require('vinyl-buffer');
var uglify = require('gulp-uglify');
var gulpif = require('gulp-if');
var watch = false;
var debugging = false;

var getSharedData = function () {
    var commonRequires = [];
    var exposedVariables = "";

    var utils = {
        parseConfig: function (config) {
            if (config) {
                if (config.expose) {
                    var components = {};
                    //1. parse the configuration
                    config.expose.forEach(function (component) {
                        var path, name;

                        if (typeof component === 'string') {
                            path = component;
                        }
                        else {
                            path = component.path;
                            if (component.name) {
                                name = component.name;
                            }
                        }
                        if (name === undefined) {
                            var splitted = path.split('/');
                            name = splitted[splitted.length - 1];
                        }
                        components[name] = path;
                    });
                    return components;
                }
            }
        }
    };

    var config = require('./reactServerConfig.json');

    var sharedComponents = utils.parseConfig(config);
    if (sharedComponents) {
        exposedVariables = ';' + os.EOL + 'var React = require("react");' + os.EOL;
        commonRequires.push({ file: "react" });
        for (var name in sharedComponents) {
            var path = sharedComponents[name];
            commonRequires.push({ file: path, expose: name });
            exposedVariables = exposedVariables.concat('var ' + name + ' = React.createFactory(require("' + name + '"));' + os.EOL);
        }
        return { exposedVariables: exposedVariables, commonRequires: commonRequires };
    }
    return { exposedVariables: null, commonRequires: null };
};

var createServerBundle = function (browserify, configPath) {
    var utils = {
        parseConfig: function (config) {
            if (config) {
                if (config.expose) {
                    var components = {};
                    //1. parse the configuration
                    config.expose.forEach(function (component) {
                        var path, name;

                        if (typeof component === 'string') {
                            path = component;
                        }
                        else {
                            path = component.path;
                            if (component.name) {
                                name = component.name;
                            }
                        }
                        if (name === undefined) {
                            var splitted = path.split('/');
                            name = splitted[splitted.length - 1];
                        }
                        components[name] = path;
                    });
                    return components;
                }
            }
        },
        exposeReact: function (exposedVariables, requires) {
            requires.push({ file: "react" });
            exposedVariables.push('var React = require("react");' + os.EOL);
        }
    };

    if (configPath === undefined) {
        configPath = './reactServerConfig.json';
    }
    var config = require(configPath);

    var serverComponents = utils.parseConfig(config);
    if (serverComponents) {
        var requires = [];
        exposedVariables = new stream.Readable();
        exposedVariables._read = function noop() { };
        exposedVariables.push(';' + os.EOL);
        utils.exposeReact(exposedVariables, requires);

        for (var name in serverComponents) {
            var path = serverComponents[name];
            requires.push({ file: path, expose: name });
            exposedVariables.push('var ' + name + ' = React.createFactory(require("' + name + '"));' + os.EOL);
        }

        exposedVariables.push('function setTimeout(cb, ms){ var args = Array.prototype.slice.call(arguments, 2); cb(args);}; ');
        browserify.require(requires);
        browserify.transform(reactify);
        return browserify.bundle()
            .pipe(addStream(exposedVariables));
    }
};

gulp.task('server-shims', function (cb) {
    gutil.log(gutil.colors.cyan('Generating server shims...'));
    var browserified = transform(function (filename) {
        var b = browserify(filename);
        return b.bundle();
    });

    gulp.src('./Content/Scripts/app/serverShim.js')
        .pipe(browserified)
        .pipe(footer('if (!window) { var window = this; }')) // Add alias so that it emulates a window in clearscript
        //.pipe(streamify(uglify()))
        .pipe(gulp.dest(bundlesDestination));
    return cb(null);
});

var gulpServerBundle = function (cb) {
    var sharedData = getSharedData();

    var serverInclude = sharedData.exposedVariables + os.EOL
        + 'function setTimeout(cb, ms){ var args = Array.prototype.slice.call(arguments, 2); cb(args);};';

    var browserified = transform(function (filename) {
        var b = browserify(filename,
            {
                extensions: ['.jsx', '.js'],
                debug: debugging
            }
        );
        b.require(sharedData.commonRequires);
        b.transform(reactify);

        return b.bundle();
    });
    gulp.src(serverInputPaths)
        .pipe(browserified)
        .pipe(footer(serverInclude))
        .pipe(rename(function (path) {
            path.basename = serverOutputNames[path.basename];
            path.extname = ".js";
        }))
        .pipe(gulp.dest(bundlesDestination));

    return cb(null);
};

var watch = false;
var watchify = require('watchify');
var browserify = require('browserify');

var gulpClientBundle = function (cb) {
    var sharedData = getSharedData();

    var opts = {
        extensions: ['.jsx', '.js'],
        hasExports: true,
        debug: debugging
    };

    if (watch) {
        opts.cache = {};
        opts.packageCache = {};
        opts.fullPaths = true;
        opts.debug = true;
        opts: watch = true;
    }

    var browserified = transform(function (filename) {
        gutil.log(gutil.colors.cyan('Processing ' + filename));
        var b = browserify(filename, opts);

        if (watch) {
            b = watchify(b);
            b.on('update', function () {
                var b2 = transform(function (filename) {
                    b3 = browserify(filename, opts);
                    b3.require(sharedData.commonRequires);
                    b3.transform(reactify);
                    return b3.bundle();;
                });

                gulp.src(clientPaths)
                    .pipe(b2)
                    .pipe(footer(sharedData.exposedVariables))
                    .pipe(gulpif(!debugging, streamify(uglify())))
                    .pipe(gulp.dest(bundlesDestination))
                    .pipe(browserSync.reload({ stream: true }));
                b2 = null;
            });
        }
        _.forEach(clientPaths, function (clientPath) {
            if (path.basename(clientPath) !== path.basename(filename)) {
                gutil.log(gutil.colors.cyan('Excluding ' + clientPath));
                b.exclude(clientPath);
            }
        });
        b.require(sharedData.commonRequires);
        b.transform(reactify);
        return b.bundle();
    });

    gulp.src(clientPaths)
        .pipe(browserified)
        .pipe(footer(sharedData.exposedVariables))
        .pipe(gulpif(!debugging, streamify(uglify())))
        .pipe(gulp.dest(bundlesDestination))
        .pipe(browserSync.reload({ stream: true }));

    cb(null);
};

gulp.task('client-build', function (cb) {
    return gulpClientBundle(cb);
});

gulp.task('server-build', ['server-shims'], function (cb) {
    return gulpServerBundle(cb);
});

gulp.task('clean', function (cb) {
    del([
        'Content/Scripts/dist/**'
    ], cb);
});

gulp.task('set-watch', function (cb) {
    watch = true;
    cb(null);
});

gulp.task('set-debug', function (cb) {
    debugging = true;
    process.env.NODE_ENV = "Dev";
    cb(null);
});

gulp.task('serve-client', ['client-build'], function (cb) {
    browserSync({
        notify: true,
        startPath: "/ControlPanel?alt_id=8b1de758-f2cd-40f7-b16d-09fcc6886f14&st=13&locale=en-us",
        ghostMode: true,
        proxy: "http://localhost:51337",
        reloadDelay: 5000,
        logLevel: "debug"
    });
});

gulp.task('default', ['server-build', 'client-build']);
gulp.task('debug', ['set-debug', 'server-build', 'client-build']);
gulp.task('client-debug', ['set-debug', 'client-build']);
gulp.task('live', ['set-watch', 'set-debug', 'client-build']);
gulp.task('serve', ['set-watch', 'set-debug', 'serve-client']);
var dest = "./Scripts/dist";
var src = './Scripts/reflux';

var reactify = require('reactify');

module.exports = {
    browserify: {
        // A separate bundle will be generated for each
        // bundle config in the list below
        bundleConfigs: [
            {
                entries: [
                    './node_modules/jquery/dist/jquery.js',
                    './node_modules/react/dist/react.js',
                    './node_modules/reflux/dist/reflux.js',
                    './node_modules/moment/moment.js'
//                    './node_modules/react-router/lib/index.js'
                ],
                dest: dest,
                outputName: 'vendorBundle.js',                
            },
            {
                entries: ['./Scripts/reflux/apps/HomePageApp.js'],
                transform: [reactify],
                dest: dest,
                outputName: 'HomePageApp.js',
                // list of externally available modules to exclude from the bundle
                external: ['jquery', 'react', 'reflux', 'react-router']
            },
            {
                entries: ['./Scripts/reflux/apps/TaskPageApp.js'],
                transform: [reactify],
                dest: dest,
                outputName: 'TaskPageApp.js',
                // list of externally available modules to exclude from the bundle
                external: ['jquery', 'react', 'reflux', 'react-router']
            },
            {
                entries: ['./Scripts/reflux/apps/EditChartApp.js'],
                transform: [reactify],
                dest: dest,
                outputName: 'EditChartApp.js',
                // list of externally available modules to exclude from the bundle
                external: ['jquery', 'react', 'reflux', 'react-router']
            },
            {
                entries: ['./Scripts/reflux/apps/PointEarnerManagementApp.js'],
                transform: [reactify],
                dest: dest,
                outputName: 'PointEarnerManagementApp.js',
                // list of externally available modules to exclude from the bundle
                external: ['jquery', 'react', 'reflux', 'react-router']
            },
            {
                entries: ['./Scripts/reflux/apps/CollectPointsApp.js'],
                transform: [reactify],
                dest: dest,
                outputName: 'CollectPointsApp.js'
            }
]
    },
    filePaths: {
        appSource: ['./Scripts/reflux/actions/*.js', './Scripts/reflux/Components/*.js', './Scripts/reflux/stores/*.js'],
        sourceDestination: '/Scripts/reflux',
        outputPath: './Scripts/dist',
        buildDestination: './Scripts/dist/build',
        vendorBundleFileName: 'vendor',
    },
    lessPaths: {
        srcPath: './Content/less/*.less',
        destPath: './Content/css'
    }
};
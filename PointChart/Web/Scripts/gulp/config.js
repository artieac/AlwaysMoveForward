var dest = "./Scripts/dist";
var src = './Scripts/reflux';

var reactify = require('reactify');

module.exports = {
    browserify: {
        // A separate bundle will be generated for each
        // bundle config in the list below
        bundleConfigs: [
            {
                entries: ['./node_modules/react/dist/react.js'],
                dest: dest,
                outputName: 'vendorBundle.js',
                // list of externally available modules to exclude from the bundle
                external: ['jquery']
            },
            {
                entries: ['./Scripts/reflux/apps/HomePageApp.js'],
                transform: [reactify],
                dest: dest,
                outputName: 'HomePageApp.js',
            },
            {
                entries: ['./Scripts/reflux/apps/TaskPageApp.js'],
                transform: [reactify],
                dest: dest,
                outputName: 'TaskPageApp.js',
            }
]
    },
    filePaths: {
        appSource: ['./Scripts/reflux/actions/*.js', './Scripts/reflux/Components/*.js', './Scripts/reflux/stores/*.js'],
        sourceDestination: '/Scripts/reflux',
        outputPath: './Scripts/dist',
        buildDestination: './Scripts/dist/build',
        vendorBundleFileName: 'vendor',
    }
};
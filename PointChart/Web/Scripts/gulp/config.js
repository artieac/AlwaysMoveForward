var dest = "./Scripts";
var src = './Scripts/reflux';

module.exports = {
    browserify: {
        // A separate bundle will be generated for each
        // bundle config in the list below
        bundleConfigs: [{
            entries: ['./node_modules/less/dist/*.js', './node_modules/react/dist/*.js'],
            dest: dest,
            outputName: 'appBundle.js',
            // list of externally available modules to exclude from the bundle
            external: ['jquery']
        }]
    }
};
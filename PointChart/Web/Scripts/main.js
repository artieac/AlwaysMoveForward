requirejs.config({
    // relative url from where modules will load
    paths: {
        'jquery': './require_jquery'

    },
    shim: {
        'jquery.form': {
            deps: ['jquery'],
            exports: 'jquery.fn.form'
        }
    }
});
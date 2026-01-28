const path = require('path');
const webpack = require('webpack');
const BundleAnalyzerPlugin =
    require("webpack-bundle-analyzer").BundleAnalyzerPlugin

module.exports = {
    target: 'web',
    entry: {
        LandingPageApp: './src/react/LandingPageApp/index.js',
    },
	output: {
		filename: '[name].js',
//		path: path.resolve(__dirname, './static/script/dist')
		path: path.resolve(__dirname, './target/script/dist'),
	},
    module: {
        rules: [
           {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
           },
           {
                test: /\.js$/,
                use: [
                    {
                         loader: "babel-loader",
                         options: {
                            plugins: ['lodash'],
                            presets: [['@babel/env', { 'targets': { 'node': 6}}]]
                         }
                     }
                ],
                exclude: /node_modules/,
            },
            {
                test: /\.css$/,
                exclude: /node_modules/,
                use: [
                    { loader: 'style-loader' },
                    { loader: 'css-loader' },
                    {
                        loader: 'postcss-loader',
                        options: {
                            postcssOptions: {
                                plugins: [
                                    [ 'autoprefixer', {}, ],
                                ],
                            },
                        }
                    }
                ]
            },
        ]
    },
    plugins: [
//        new BundleAnalyzerPlugin()
    ],
	resolve: {
		extensions: ['*', '.ts', '.tsx', '.js', '.jsx', '.less', '.css'],
        alias: {
          SharedComponents: path.resolve(__dirname, 'src/react/components'),
          CSS: path.resolve(__dirname, 'static/css'),
          Redux: path.resolve(__dirname, 'src/react/Redux'),
          Repositories: path.resolve(__dirname, 'src/react/Repositories')
        }
	},
};
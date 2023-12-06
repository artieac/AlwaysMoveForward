const path = require('path');
const webpack = require('webpack');

module.exports = {
    mode: 'development',
    target: 'web',
    entry: {
		AdminApp: './src/react/LandingPageApp/index.js',
	},
	output: {
		filename: '[name].js',
//		path: path.resolve(__dirname, './static/script/dist')
		path: path.resolve(__dirname, './target/classes/static/script/dist'),
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
                    { loader: "babel-loader" }
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
	resolve: {
		extensions: ['*', '.tsx', '.js', '.jsx', '.less', '.css'],
        alias: {
          SharedComponents: path.resolve(__dirname, 'src/react/components'),
          CSS: path.resolve(__dirname, 'static/css'),
        }
	},
};
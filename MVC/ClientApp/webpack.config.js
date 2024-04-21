const path = require('path');
const TsconfigPathsPlugin = require('tsconfig-paths-webpack-plugin')
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: {
        site: './src/js/site.ts',
        index: './src/js/Index.ts',
        validation: './src/js/Validation.ts',
        step: './src/js/Flow/StepAPI.ts',
        theme: './src/js/Theme/ThemeAPI.ts',
        mainThemeDetails: './src/js/Theme/MainThemeDetailsAPI.ts',
        subThemeDetails: './src/js/Theme/SubThemeDetailsAPI.ts',
        webcam: './src/js/Webcam/WebCamDetection.ts',
        login: './src/js/Login/login-animation.ts',
        dashboard: './src/js/Dashboard/Dashboard.ts',
    },
    output: {
        filename: '[name].entry.js',
        path: path.resolve(__dirname, '..', 'wwwroot', 'dist'),
        clean: true,
    },
    devtool: 'source-map',
    mode: 'development',
    resolve: {
        extensions: [".ts", ".js"],
        extensionAlias: {
            '.js': ['.js', '.ts'],
        },
        modules: ['node_modules', path.resolve(__dirname, "src")],
        fallback: {
            fs: false,
            path: false,
            crypto: false,
        },
        plugins: [
            new TsconfigPathsPlugin({
                configFile: path.resolve(__dirname, 'tsconfig.json'),
                extensions: ['.ts', '.js'],
            })
        ]
    },
    module: {
        rules: [
            {
                test: /\.ts?$/i,
                use: 'ts-loader',
                exclude: [/node_modules/, /custom npm packages/]
            },
            {
                test: /\.s?css$/,
                use: [{loader: MiniCssExtractPlugin.loader}, 'css-loader', 'sass-loader']
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif|webp)$/i,
                type: "asset/resource"
            },
            {
                test: /\.(eot|woff(2)?|ttf|otf|svg)$/i,
                type: 'asset/resource'
            },
            {
                test: /\.(mp3|wav|ogg|mp4|webm|mkv)$/i,
                type: 'asset/resource'
            },
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: "[name].css"
        })
    ]
}

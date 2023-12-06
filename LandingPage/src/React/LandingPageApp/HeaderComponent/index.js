'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';

export const MyStoryComponent = () => {

    return (
        <div id="my-story" class="section">
            <div id="header" class="header">
                <div className="container">
                    <div className="navigation">
                        <a href="#header" class="logo-link inline-block">
                            <h1 className="logo">Arthur Correa</h1>
                        </a>
                        <a href="#my-story" className="nav-link">About me</a>
                        <a href="#personalprojects" className="nav-link">Personal Projects</a>
                        <a href="#online" className="nav-link">Recent Blog Entries</a>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MyStoryComponent
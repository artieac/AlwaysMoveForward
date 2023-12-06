'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';
import MyRadarsComponent from './MyRadarsComponent';
import BlogComponent from './BlogComponent';

export const PersonalProjectsSection = () => {

    return (
        <div className="section grey">
            <div className="container">
                <div className="row">
                    <div className="align-right col col-4">
                        <h2>Personal Projects</h2>
                    </div>
                    <div className="col-8">
                        <div className="row">
                            <MyRadarsComponent />
                            <BlogComponent />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PersonalProjectsSection;
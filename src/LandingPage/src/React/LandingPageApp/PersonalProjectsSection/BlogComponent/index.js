'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';
import ProjectCardComponent from '../ProjectCardComponent';

export const BlogComponent = () => {
    return (
        <div className="row">
            <div className="card">
                <div className="card-body text-center">
                    <h3 className="card-title">My Blog</h3>
                    <p className="card-text">My personal blog software.  This is a multi tenant blog site that I wrote in .Net years ago.  It was originally written in .Net MVC3 upgraded to 5 with Angular, and most recently upgraded to .Net Core with React and Docker.</p>
                    <div className="row">
                        <ProjectCardComponent
                            targetUrl="http://blog.alwaysmoveforward.com"
                            cardImage="images/noun_Blog_1563318.png"
                            cardTitle="My blog"
                            cardText="Here I blog about anything on the topics from Programming to Hockey to Disney."/>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default BlogComponent;
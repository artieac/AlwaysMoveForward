'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';

export const ProjectCardComponent = ({ targetUrl, cardImage, cardTitle, cardText }) => {

    return (
        <a href={ targetUrl } className="journal-entry inline-block">
            <div className="card">
                <div class="card-body">
                    <img src={ cardImage } className="grid-image" alt="..."/>
                    <h5 className="card-title">{ cardTitle }</h5>
                    <p className="card-text">{ cardText }</p>
                </div>
            </div>
        </a>
    );
}

export default ProjectCardComponent;
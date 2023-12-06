'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';
import ProjectCardComponent from '../ProjectCardComponent/index';

export const MyRadarsComponent = () => {
    const getTechnologyRadarUrlRoot = () => {
        return "https://technologyradar.alwaysmoveforward.com";
    }

    const buildTechnologyRadarUrl = (targetEndpoint) => {
        return getTechnologyRadarUrlRoot() + targetEndpoint;
    }

    return (
        <div className="row">
            <div className="card">
                <div className="card-body text-center">
                    <h3 className="card-title">My Radars</h3>
                    <p className="card-text">My personal Radars. The site also lets you manage your own Radar once you&#x27;ve logged in with Auth0. I started with a Technology Radar but since then I've evolved it to allow a user to define their own Radar types.  I wrote this with Java, Spring Boot, React, and Docker.</p>
                    <div className="card-body">
                        <div className="card-group">
                            <div className="col-md-6">
                                <ProjectCardComponent
                                    targetUrl={ buildTechnologyRadarUrl("?userId=1&radarTemplateId=3&fullView=true") }
                                    cardImage="images/noun_Radar_9219.png"
                                    cardTitle="Technology Radar"
                                    cardText="This is my Technology Radar.  The one that started everything.  It's my own personal instance of Thoughtwork's original radar." />
                            </div>
                            <div className="col-md-6">
                                <ProjectCardComponent
                                    targetUrl={ buildTechnologyRadarUrl("?userId=1&radarTemplateId=1&mostRecent=true") }
                                    cardImage="images/noun_Radar_9219.png"
                                    cardTitle="Disney Radar"
                                    cardText="This is my Disney radar.  As I worked on the Technology one I realized there were other things I wanted to rate, and Disney was the top of the list."/>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MyRadarsComponent;
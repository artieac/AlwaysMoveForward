'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';
import ProjectCardComponent from '../ProjectCardComponent';

export const PlanningComponent = () => {
    return (
        <div className="row">
            <div className="card">
                <div className="card-body text-center">
                    <h3 className="card-title">Planning Manager</h3>
                    <p className="card-text">This is an app I built help me deal with capacity planning and questions from people  I found I was often asked who was doing what when.  I was also asked a lot of \"what if we added more people\", or \"when is this going to be done\".  This gave me a way to put all of that together.  On top of that I was able to formalize my Milestone mindset that I have historically managed in a spreadsheet into something that makes it easier to see the evolution of Milestone changes.  One last thing of note.  Probably a good 80%, if not more, of this code base was built using AI (a combination of CLaude Code and Gemini), I was able to build this in a little over a week an I am pleased with the ressults.</p>
                    <div className="row">
                        <ProjectCardComponent
                            targetUrl="https://planning.alwaysmoveforward.com"
                            cardImage="images/noun_Blog_1563318.png"
                            cardTitle="Planning Manager"
                            cardText="A planning app that allows for team capacity planning, and Milestone management."/>

                    </div>
                </div>
            </div>
        </div>
    );
}

export default PlanningComponent;
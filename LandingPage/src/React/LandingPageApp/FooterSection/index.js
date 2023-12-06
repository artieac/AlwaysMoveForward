'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';
import SocialLinkComponent from './SocialLinkComponent/index';

export const FooterSection = () => {

    return (
        <div className="section grey">
            <div className="footer">
                <div className="container">
                    <div className="row">
                        <div className="col-md-5">
                            <h5>about AlwaysMoveForward.com</h5>
                            <p>AlwaysMoveForward.com is the personal site of Arthur Correa.</p>
                        </div>
                        <div className="col-md-4">
                            <h5>useful links</h5>
                            <a className="footer-link">AlwaysMoveForward.com</a>
                            <a className="footer-link">Blog</a>
                            <a className="footer-link">Your Radar</a>
                        </div>
                        <div className="col-md-3">
                            <h5>social</h5>
                            <SocialLinkComponent
                                image="images/social-09.svg"
                                link="https://www.linkedin.com/in/arthur-correa-00864a1"
                                title="Linked In" />
                            <SocialLinkComponent
                                image="images/social-11.svg"
                                link="https://www.pinterest.com/artieac/"
                                title="Pinterest" />
                            <SocialLinkComponent
                                image="images/social-07.svg"
                                link="https://www.instagram.com/artiecorrea/"
                                title="Instagram" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default FooterSection
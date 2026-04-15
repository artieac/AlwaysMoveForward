'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';

export const SocialLinkCompoonent = ({ image, link, title }) => {

    return (
        <div className="footer-link-wrapper clearfix">
            <img src={ image } width="20" alt="" className="info-icon"/>
            <a href={ link } className="footer-link with-icon">{ title }</a>
        </div>
    );
}

export default SocialLinkCompoonent
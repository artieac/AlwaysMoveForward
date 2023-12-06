'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';

export const BlogPostComponent = ({ rowData }) => {
    const formatDate = (date) => {
        var dateOut = new Date(date);
        return dateOut;
    }

    return(
        <a href={ rowData.BlogPostUrl } className="journal-entry w-inline-block grey">
            <h2>{ rowData.Title }</h2>
            <p>Posted { rowData.DatePosted }</p>
            <p>{ rowData.ShortEntryText }</p>
        </a>
    );
}

export default BlogPostComponent;
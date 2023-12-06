import React from 'react'
import BlogPostComponent from '../BlogPostComponent';

export const BlogPostRowDefinition = () => {

    return (
    {
        render: rowData => {
            return <BlogPostComponent rowData = { rowData }  />
        }
    });
};

export default BlogPostRowDefinition;
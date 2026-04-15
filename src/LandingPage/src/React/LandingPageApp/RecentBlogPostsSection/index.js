'use strict'
import jQuery from 'jquery';
import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';
import { BlogRepository } from 'Repositories/BlogRepository';
import BlogPostRowDefinition from './BlogPostRowDefinition';
import ListComponent from 'SharedComponents/ListComponent';
import { addRecentBlogPostsToState } from 'Redux/BlogReducer';

export const RecentBlogPostsComponent = () => {
    const [isLoading, setIsLoading] = useState(true);

    const dispatch = useDispatch();

    const recentPosts = useSelector((state) => state.blogReducer.recentPosts);

    useEffect(() => {
        getRecentPosts();
    }, []);

    const getRecentPosts = () => {
        let blogRepository = new BlogRepository();
        blogRepository.getMostRecentPosts(2, handleGetRecentPostsResponse);
    }

    const handleGetRecentPostsResponse = (wasSuccessful, data) => {
        if(wasSuccessful==true){
            dispatch(addRecentBlogPostsToState(data));
            setIsLoading(false);
        }
    }

    return (
        <div class="section">
            <div className="row">
                <div className="container">
                    <div className="row">
                        <div className="align-right col col-4">
                            <h2>Recent Blog Entries</h2>
                        </div>
                        <div className="col col-8">
                            <ListComponent
                                itemMap = { BlogPostRowDefinition() }
                                data = { recentPosts } />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default RecentBlogPostsComponent;
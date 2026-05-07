'use strict'
import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from "react-redux"
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
        <div id="recent-blog" className="py-16 md:py-24 bg-white">
            <div className="container mx-auto px-4">
                <div className="flex flex-col md:flex-row">
                    <div className="md:w-1/3 md:pr-10 text-right mb-6 md:mb-0">
                        <h2 className="text-3xl font-light">Recent Blog Entries</h2>
                    </div>
                    <div className="md:w-2/3">
                        <ListComponent
                            itemMap = { BlogPostRowDefinition() }
                            data = { recentPosts } />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default RecentBlogPostsComponent;

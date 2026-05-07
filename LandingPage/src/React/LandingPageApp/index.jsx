'use strict'
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom'
import { appsProviderStore } from './ProviderStore'
import HeaderComponent from './HeaderComponent'
import MyStorySection from './MyStorySection';
import PersonalProjectsSection from './PersonalProjectsSection';
import FooterSection from './FooterSection';
import RecentBlogPostsSection from './RecentBlogPostsSection';
import { ErrorBoundary } from 'react-error-boundary'
import ErrorBoundaryHandler from 'SharedComponents/ErrorBoundaryHandler'
import NavBarComponent from 'SharedComponents/NavBarComponent';
import '../../index.css';

export default function LandingPageApp() {
    return (
        <ErrorBoundary
            FallbackComponent={ErrorBoundaryHandler}
            onReset={() => {
                // reset the state of your app here
            }}
            resetKeys={['someKey']}
        >
            <div>
                <NavBarComponent />
                <HeaderComponent />
                <MyStorySection />
                <PersonalProjectsSection />
                <RecentBlogPostsSection />
                <FooterSection />
            </div>
        </ErrorBoundary>
    );
}

ReactDOM.render(  
    <Provider store={appsProviderStore}>
        <BrowserRouter>
            <LandingPageApp />
        </BrowserRouter>
    </Provider>,
    document.getElementById("landingPageAppContent")
)

'use strict'
import jQuery from 'jquery';
import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux"
import ReactDOM from 'react-dom';

export const MyStorySection = () => {

    return (
        <div className="section">
            <div className="container">
                <div className="row">
                    <div className="align-right col col-4">
                        <h2>About me</h2>
                    </div>
                    <div className="col col-8">
                        <p>I am a Director of Engineering/Architect, hockey player,, Disney fan, and family man.</p>
                        <p>As a Director of Engineering I&#x27;m a firm believer in modern practices such as Servant Leadership, Domain Driven Design, Microservices, Containers, CI/CD, Dev Ops and deploying to the cloud. I most enjoy designing and building distributed systems built using microservices, but I&#x27;m not too particular about what technology I use to build them. I have built production systems in Java, .Net/.Net Core, and Node deployed to AWS and Azure, but I'm open to other things as well.</p>
                        <p>As a hockey player. I&#x27;ve gone from someone that wants to play as competitively as I can and keep getting better to someone that just does it for fun and exercise. Nowadays I&#x27;m just happy if I can get up and down the ice without looking like an idiot.</p>
                        <p>I&#x27;m a big Disney fan. I've loved going to Disney World since I was a kid,, and I never seem to get sick of it. Everything from the theming, to the rides, to the food. Every time I go there I'm trying to soak it all in. My favorite park is probably Magic Kingdom, and my favorite ride is Splash Mountain.</p>
                        <p>And the best for last. As a family man I love to spend my time out of work with my family.  Either driving my kids to or from all their sports, playing board games, or going to Disney with them. &#x27;</p>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MyStorySection;
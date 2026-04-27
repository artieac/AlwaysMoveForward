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
                        <p>Whether I&apos;m on the ice for an early morning session or scaling a global engineering team, I&rsquo;m driven by a love for complex problem-solving and a high-performance culture. As a technology executive with over 25 years of experience, I&rsquo;ve made a career out of bridging the gap between deep technical code reviews and high-level boardroom strategy.</p>
                        <p>On the professional side, I am a VP of Engineering and Chief Architect who is a firm believer in modern practices such as Servant Leadership, Domain-Driven Design, and DevOps. My professional focus is on the strategic &ldquo;heavy lifting&rdquo; of technology: breaking down complex monoliths into distributed, cloud-native systems. I am particularly passionate about driving engineering organizations to leverage AI and machine learning to accelerate these modernization efforts, ensuring that technical evolution directly serves measurable business value.</p>
                        <p>I&apos;ve spent my career leading massive architecture shifts at companies like Vistaprint, Wayfair, and athenahealth. While I have built production systems in Java, .NET/Core, and Node on AWS and Azure, I remain technology-agnostic. I&rsquo;m far more interested in whether a system is scalable, secure, and solving a real human problem than what language it&rsquo;s written in.</p>
                        <p>When I&rsquo;m not working, I like to be with my family. Most of my free time is spent with my family; while the days of taxiing them around to their sports are gone, we still enjoy playing board games or planning our next trip. Recent travels have taken us from the canals of Venice to the vibrant markets of Marrakech.</p>
                        <p>I&rsquo;m also an unapologetic Disney fan. I&rsquo;ve loved the parks since I was a kid and never tire of the incredible theming and food. While Magic Kingdom is my classic favorite, Rise of the Resistance has officially taken the top spot for my favorite ride (though a piece of my heart will always stay with the original Splash Mountain). As for hockey? I&rsquo;ve gone from someone who wanted to play as competitively as possible to someone who just does it for the fun and exercise. These days, I&rsquo;m just happy if I can get up and down the ice without looking like an idiot.</p>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MyStorySection;
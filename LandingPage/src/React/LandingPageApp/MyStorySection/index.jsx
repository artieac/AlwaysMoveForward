import React from 'react';

export const MyStorySection = () => {
    return (
        <div id="my-story" className="py-16 md:py-24 bg-white">
            <div className="container mx-auto px-4">
                <div className="flex flex-col md:flex-row">
                    <div className="md:w-1/3 md:pr-10 text-right mb-6 md:mb-0">
                        <h2 className="text-3xl font-light">About me</h2>
                    </div>
                    <div className="md:w-2/3">
                        <div className="space-y-4 text-gray-400 text-sm leading-relaxed">
                            <p>Whether I'm on the ice for an early morning session or scaling a global engineering team, I’m driven by a love for complex problem-solving and a high-performance culture. As a technology executive with over 25 years of experience, I’ve made a career out of bridging the gap between deep technical code reviews and high-level boardroom strategy.</p>
                            <p>On the professional side, I am a VP of Engineering and Chief Architect who is a firm believer in modern practices such as Servant Leadership, Domain-Driven Design, and DevOps. My professional focus is on the strategic “heavy lifting” of technology: breaking down complex monoliths into distributed, cloud-native systems. I am particularly passionate about driving engineering organizations to leverage AI and machine learning to accelerate these modernization efforts, ensuring that technical evolution directly serves measurable business value.</p>
                            <p>I've spent my career leading massive architecture shifts at companies like Vistaprint, Wayfair, and athenahealth. While I have built production systems in Java, .NET/Core, and Node on AWS and Azure, I remain technology-agnostic. I’m far more interested in whether a system is scalable, secure, and solving a real human problem than what language it’s written in.</p>
                            <p>When I’m not working, I like to be with my family. Most of my free time is spent with my family; while the days of taxiing them around to their sports are gone, we still enjoy playing board games or planning our next trip. Recent travels have taken us from the canals of Venice to the vibrant markets of Marrakech.</p>
                            <p>I’m also an unapologetic Disney fan. I’ve loved the parks since I was a kid and never tire of the incredible theming and food. While Magic Kingdom is my classic favorite, Rise of the Resistance has officially taken the top spot for my favorite ride (though a piece of my heart will always stay with the original Splash Mountain). As for hockey? I’ve gone from someone who wanted to play as competitively as possible to someone who just does it for the fun and exercise. These days, I’m just happy if I can get up and down the ice without looking like an idiot.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MyStorySection;

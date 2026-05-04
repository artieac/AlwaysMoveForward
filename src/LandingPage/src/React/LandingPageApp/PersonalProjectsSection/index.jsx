import React from 'react';
import ProjectCardComponent from './ProjectCardComponent';
import { Radar, Sparkles, Newspaper, ClipboardList } from 'lucide-react';

export const PersonalProjectsSection = () => {
    const buildRadarUrl = (radarTemplateId) => {
        return `https://radars.alwaysmoveforward.com?userId=1&radarTemplateId=${radarTemplateId}&fullView=true`;
    }

    return (
        <div id="personal-projects" className="py-16 md:py-24 bg-gray-100">
            <div className="container mx-auto px-4">
                <div className="flex flex-col md:flex-row mb-12">
                    <div className="md:w-1/3 md:pr-10 text-right mb-6 md:mb-0">
                        <h2 className="text-3xl font-light">Personal Projects</h2>
                    </div>
                    <div className="md:w-2/3">
                        <div className="space-y-12">
                            {/* Row 1: Radars */}
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                <ProjectCardComponent
                                    targetUrl={buildRadarUrl(3)}
                                    Icon={Radar}
                                    cardTitle="Technology Radar"
                                    cardText="My personal assessment of the technology landscape. It tracks the tools, frameworks, and languages I'm currently using, exploring, or keeping an eye on."
                                />
                                <ProjectCardComponent
                                    targetUrl={buildRadarUrl(1)}
                                    Icon={Sparkles}
                                    cardTitle="Disney Radar"
                                    cardText="Applying the radar methodology to the world of Disney. A fun way to track my favorite rides, restaurants, and experiences across the parks."
                                />
                            </div>

                            {/* Row 2: Blog and Planning */}
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                <ProjectCardComponent
                                    targetUrl="https://blog.alwaysmoveforward.com"
                                    Icon={Newspaper}
                                    cardTitle="Another Blog"
                                    cardText="Insights on programming, leadership, hockey, and my adventures in the Disney parks."
                                />
                                <ProjectCardComponent
                                    targetUrl="https://planning.alwaysmoveforward.com"
                                    Icon={ClipboardList}
                                    cardTitle="Planning Manager"
                                    cardText="A streamlined application for team capacity forecasting and visual milestone tracking."
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PersonalProjectsSection;

import React from 'react';

export const ProjectCardComponent = ({ targetUrl, Icon, cardImage, cardTitle, cardText }) => {
    return (
        <a href={targetUrl} className="block border border-gray-200 p-6 hover:bg-gray-50 transition-colors h-full">
            <div className="flex flex-col items-center text-center">
                <div className="w-16 h-16 md:w-20 md:h-20 mb-4 rounded-full bg-blue-400 flex items-center justify-center border-8 border-white shadow-[0_0_0_1px_#2e9dff]">
                    {Icon ? (
                        <Icon size={32} className="text-white" strokeWidth={1.5} />
                    ) : (
                        <img src={cardImage} className="w-10 h-10 object-contain" alt={cardTitle} />
                    )}
                </div>
                <h5 className="text-lg font-light uppercase tracking-widest text-gray-600 mb-2">{cardTitle}</h5>
                <p className="text-sm text-gray-400 leading-relaxed">{cardText}</p>
            </div>
        </a>
    );
}

export default ProjectCardComponent;

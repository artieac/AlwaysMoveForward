import React from 'react';

const SocialLinkComponent = ({ image, link, title }) => (
    <a href={link} className="flex items-center gap-3 py-2 border-b border-gray-200 text-gray-500 hover:text-blue-500 transition-colors">
        <img src={image} className="w-5 h-5 opacity-50" alt={title} />
        <span className="text-sm font-light">{title}</span>
    </a>
);

export const FooterSection = () => {
    return (
        <div className="py-12 bg-gray-100">
            <div className="container mx-auto px-4">
                <div className="grid grid-cols-1 md:grid-cols-3 gap-12">
                    {/* Column 1: About */}
                    <div>
                        <h5 className="text-lg font-light uppercase tracking-[4px] text-gray-500 mb-6">about AlwaysMoveForward.com</h5>
                        <p className="text-sm text-gray-400 leading-relaxed">AlwaysMoveForward.com is the personal site of Arthur Correa.</p>
                    </div>

                    {/* Column 2: Useful Links */}
                    <div>
                        <h5 className="text-lg font-light uppercase tracking-[4px] text-gray-500 mb-6">useful links</h5>
                        <div className="flex flex-col space-y-2">
                            <a href="/" className="text-sm font-light text-blue-400 hover:text-blue-600 border-b border-gray-200 pb-2">AlwaysMoveForward.com</a>
                            <a href="http://blog.alwaysmoveforward.com" className="text-sm font-light text-blue-400 hover:text-blue-600 border-b border-gray-200 pb-2">Blog</a>
                            <a href="https://technologyradar.alwaysmoveforward.com" className="text-sm font-light text-blue-400 hover:text-blue-600 border-b border-gray-200 pb-2">Your Radar</a>
                            <a href="https://planning.alwaysmoveforward.com" className="text-sm font-light text-blue-400 hover:text-blue-600 border-b border-gray-200 pb-2">Planning Manager</a>
                        </div>
                    </div>

                    {/* Column 3: Social */}
                    <div>
                        <h5 className="text-lg font-light uppercase tracking-[4px] text-gray-500 mb-6">social</h5>
                        <div className="flex flex-col space-y-1">
                            <SocialLinkComponent
                                image="images/social-09.svg"
                                link="https://www.linkedin.com/in/arthur--correa"
                                title="Linked In" />
                            <SocialLinkComponent
                                image="images/social-11.svg"
                                link="https://www.pinterest.com/artieac/"
                                title="Pinterest" />
                            <SocialLinkComponent
                                image="images/social-07.svg"
                                link="https://www.instagram.com/artiecorrea/"
                                title="Instagram" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default FooterSection;

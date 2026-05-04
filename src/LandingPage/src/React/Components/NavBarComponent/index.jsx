import React, { useEffect, useState } from 'react';

export const NavBarComponent = () => {
    return (
        <nav className="fixed top-0 left-0 w-full z-50 bg-white shadow-sm border-t-[3px] border-brand-green py-4 md:py-6">
            <div className="container mx-auto px-4 flex flex-col md:flex-row justify-between items-center md:items-end">
                <a href="#header" className="text-right mb-4 md:mb-0">
                    <h1 className="text-3xl font-light">Arthur Correa</h1>
                </a>
                <div className="flex gap-6 md:gap-8">
                    <a href="#my-story" className="text-lg font-light text-blue-400 hover:text-blue-600 capitalize transition-colors">About me</a>
                    <a href="#personal-projects" className="text-lg font-light text-blue-400 hover:text-blue-600 capitalize transition-colors">Personal Projects</a>
                    <a href="#recent-blog" className="text-lg font-light text-blue-400 hover:text-blue-600 capitalize transition-colors">Blog</a>
                </div>
            </div>
        </nav>
    );
}

export default NavBarComponent;

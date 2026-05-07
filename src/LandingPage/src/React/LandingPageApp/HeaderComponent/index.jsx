import React from 'react';
import CarouselComponent from 'SharedComponents/CarouselComponent';

export const HeaderComponent = () => {
    return (
        <div id="header" className="h-[530px] border-t-[3px] border-black relative">
            <CarouselComponent />
            {/* The title is in the NavBar, this provides the height and repeated background */}
        </div>
    );
}

export default HeaderComponent;

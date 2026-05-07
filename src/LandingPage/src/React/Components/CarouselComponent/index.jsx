import React, { useState, useEffect } from 'react';

export const CarouselComponent = () => {
    const [images, setImages] = useState([]);
    const [currentIndex, setCurrentIndex] = useState(0);

    useEffect(() => {
        // Use Vite's import.meta.glob to find all images in the src/images/Carousel folder
        // Note: This happens at build time/dev time.
        const imageModules = import.meta.glob('../../../images/Carousel/*.{JPG,jpg,png,jpeg,svg}', { 
            eager: true,
            import: 'default'
        });
        
        const urls = Object.values(imageModules);

        setImages(urls);
    }, []);

    useEffect(() => {
        if (images.length === 0) return;

        const interval = setInterval(() => {
            setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
        }, 5000); // Change image every 5 seconds

        return () => clearInterval(interval);
    }, [images]);

    if (images.length === 0) {
        return <div className="h-full w-full bg-gray-200 animate-pulse" />;
    }

    return (
        <div className="relative w-full h-full overflow-hidden">
            {images.map((src, index) => (
                <div
                    key={src}
                    className={`absolute inset-0 transition-opacity duration-1000 ease-in-out ${
                        index === currentIndex ? 'opacity-100' : 'opacity-0'
                    }`}
                    style={{
                        backgroundImage: `url('${src}')`,
                        backgroundPosition: 'center',
                        backgroundSize: 'cover',
                        backgroundRepeat: 'no-repeat',
                        backgroundAttachment: 'fixed' // Keeping the original fixed effect
                    }}
                />
            ))}
        </div>
    );
};

export default CarouselComponent;

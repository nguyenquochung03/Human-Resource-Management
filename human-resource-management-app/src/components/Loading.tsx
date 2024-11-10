import React from 'react'

export default function Loading() {
    return (
        <div className='fixed top-0 left-0 right-0 bottom-0 bg-gray-950 bg-opacity-70 flex items-center justify-center z-[1000]'>
            <div
                className="inline-block text-green-600 h-10 w-10 animate-spin rounded-full border-4 border-solid border-current border-r-transparent align-[-0.125em] text-warning motion-reduce:animate-[spin_1.5s_linear_infinite]"
                role="status"
            >
                <span
                    className="!absolute !-m-px !h-px !w-px !overflow-hidden !whitespace-nowrap !border-0 !p-0 ![clip:rect(0,0,0,0)]"
                >Loading...</span>
            </div>
        </div>
    )
}

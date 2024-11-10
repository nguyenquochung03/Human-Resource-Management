import React from 'react'

export default function TextInput ({ ...props}) {
    return (
        <input 
            className='px-4 py-2 rounded-md outline-none border-[1px] border-lime-600 text-black text-md font-normal'
            readOnly
            {...props}
        />
    )
}

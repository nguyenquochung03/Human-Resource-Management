import React, { useEffect, useState } from 'react'

interface StatusProps {
    title: string,
    status: EStatus
}

export enum EStatus {
    AVAILABLE,
    UNAVAILABLE
}

const Status:React.FC<StatusProps> = (props) => {

    const { title, status } = props;


    return <span className={`px-1 text-xs py-1 rounded-md ${status === EStatus.UNAVAILABLE ? 'bg-red-200 text-red-500' : 'bg-green-200 text-green-600'}`}>
        {title}
    </span>
}

export default Status;
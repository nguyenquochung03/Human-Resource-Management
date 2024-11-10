
import React from 'react'

export default function Page({ params }: { params: { testId: string } }) {

    return <div>
        My Post: {params.testId}
        </div>
  }
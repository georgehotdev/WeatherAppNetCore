/* eslint-disable @typescript-eslint/ban-types */
import React from 'react'

export default function Card({ children }: React.PropsWithChildren<{}>) {
  return (
    <div className="w-full max-w-md rounded-2xl bg-white/20 p-6 text-white shadow-xl backdrop-blur-xl">
      {children}
    </div>
  )
}

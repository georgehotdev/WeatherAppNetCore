import React from 'react'

export default function Carousel({ children }: React.PropsWithChildren<{}>) {
  return <div className="grid grid-cols-3 gap-4 text-center">{children}</div>
}

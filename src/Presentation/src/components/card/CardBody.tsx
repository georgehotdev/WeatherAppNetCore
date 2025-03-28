/* eslint-disable @typescript-eslint/ban-types */
import React from 'react'

export default function CardBody({ children }: React.PropsWithChildren<{}>) {
  return <div className="mt-6">{children}</div>
}

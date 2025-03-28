import React from 'react'

type Props = {
  text: string
}

export default function CardTitle({ text }: Props) {
  return <h1 className="text-3xl font-bold">{text}</h1>
}

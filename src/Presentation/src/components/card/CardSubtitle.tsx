import React from 'react'

type Props = {
  text: string
}

export default function CardSubtitle({ text }: Props) {
  return <p className="mt-4 text-6xl font-light">{text}</p>
}

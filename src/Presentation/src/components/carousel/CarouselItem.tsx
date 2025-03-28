import React from 'react'

type CarouselItemProps = {
  heading: string
  paragraphs: string[]
}

export default function CarouselItem({
  heading,
  paragraphs
}: CarouselItemProps) {
  return (
    <div className="rounded-xl bg-white/10 p-3">
      {heading && <p className="font-semibold">{heading}</p>}
      {paragraphs && paragraphs.map((text, index) => <p key={index}>{text}</p>)}
    </div>
  )
}

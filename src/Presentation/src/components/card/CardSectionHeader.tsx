/* eslint-disable @typescript-eslint/ban-types */

type CardSectionHeaderProps = {
  text: string
}

export default function CardSectionHeader({
  text
}: React.PropsWithChildren<CardSectionHeaderProps>) {
  return <h2 className="mb-2 text-lg font-semibold">{text}</h2>
}

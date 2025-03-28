/* eslint-disable tailwindcss/no-custom-classname */
import {
  Listbox,
  ListboxButton,
  ListboxOption,
  ListboxOptions
} from '@headlessui/react'
import { CheckIcon, ChevronDownIcon } from '@heroicons/react/20/solid'
import clsx from 'clsx'
import { useEffect, useState } from 'react'
import './ComboBox.css'

type ComboBoxProps = {
  values: string[]
  onChange: (value: string) => void
}

export default function ComboBox({ values, onChange }: ComboBoxProps) {
  const [selected, setSelected] = useState<string>(values[0])

  const onSelectionChanged = (value: string) => {
    setSelected(value)
    onChange(value)
  }

  useEffect(() => {
    console.log(values)
  }, [values])

  return (
    <div className="mx-auto w-52 pt-6">
      <Listbox value={selected} onChange={onSelectionChanged}>
        <ListboxButton
          className={clsx(
            'relative block w-full rounded-lg bg-white/10 py-1.5 pl-3 pr-8 text-left text-sm text-white',
            'focus:outline-none focus-visible:outline-2 focus-visible:-outline-offset-2 focus-visible:outline-white/25',
            'combobox-button'
          )}
        >
          {selected || 'Select a city'}
          <ChevronDownIcon
            className="pointer-events-none absolute right-2.5 top-2.5 size-4 fill-white/60"
            aria-hidden="true"
          />
        </ListboxButton>
        <ListboxOptions
          anchor="bottom"
          transition
          className={clsx(
            'w-[var(--button-width)] rounded-xl border border-white/5 bg-white/10 p-1',
            'transition duration-100 ease-in focus:outline-none data-[leave]:data-[closed]:opacity-0',
            'combobox-option'
          )}
        >
          {values.map((value) => (
            <ListboxOption
              key={value}
              value={value}
              className="group flex cursor-default select-none items-center gap-2 rounded-lg px-3 py-1.5 data-[focus]:bg-white/10"
            >
              <CheckIcon className="invisible size-4 fill-white group-data-[selected]:visible" />
              <div className="text-sm text-white">{value}</div>
            </ListboxOption>
          ))}
        </ListboxOptions>
      </Listbox>
    </div>
  )
}

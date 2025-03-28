import React from 'react'

export default function Spinner() {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50">
      <div className="size-12 animate-spin rounded-full border-4 border-white border-t-transparent" />
    </div>
  )
}

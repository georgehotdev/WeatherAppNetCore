import React from 'react'
import { Line } from 'react-chartjs-2'

export type LineChartSerie = {
  label: string
  values: number[]
}

export type LineChartProps = {
  labels: string[]
  series: LineChartSerie[]
}

const seriesTheme = [
  {
    borderColor: '#ff6384',
    backgroundColor: 'rgba(255, 99, 132, 0.15)',
    pointBackgroundColor: '#ff6384',
    pointBorderColor: '#fff',
    pointRadius: 6,
    pointHoverRadius: 8,
    pointStyle: 'circle',
    fill: true,
    tension: 0.4
  },
  {
    borderColor: '#36a2eb',
    backgroundColor: 'rgba(54, 162, 235, 0.15)',
    pointBackgroundColor: '#36a2eb',
    pointBorderColor: '#fff',
    pointRadius: 6,
    pointHoverRadius: 8,
    pointStyle: 'circle',
    fill: true,
    tension: 0.4
  }
]

const options = {
  scales: {
    y: {
      beginAtZero: false,
      ticks: {
        color: 'white',
        font: {
          size: 14
        }
      },
      grid: {
        color: 'rgba(255, 255, 255, 0.1)'
      }
    },
    x: {
      ticks: {
        color: 'white',
        font: {
          size: 14
        }
      },
      grid: {
        color: 'rgba(255, 255, 255, 0.05)'
      }
    }
  },
  responsive: true,
  plugins: {
    legend: {
      labels: {
        color: 'white',
        font: {
          size: 14
        }
      }
    },
    tooltip: {
      backgroundColor: 'rgba(0,0,0,0.7)',
      titleFont: { size: 14 },
      bodyFont: { size: 14 },
      cornerRadius: 4,
      padding: 10
    }
  }
}

export default function LineChart({ labels, series }: LineChartProps) {
  const data = {
    labels,
    datasets: series.map((serie, index) => ({
      label: serie.label,
      data: serie.values,
      ...seriesTheme[index]
    }))
  }
  console.log('🚀 ~ LineChart ~ data:', data)

  return <Line data={data} options={options} />
}

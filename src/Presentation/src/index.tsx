import { createRoot } from 'react-dom/client'
import 'tailwindcss/tailwind.css'
import IndexPage from '@pages/Index/IndexPage'
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
} from 'chart.js'

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
)

const container = document.getElementById('root') as HTMLDivElement
const root = createRoot(container)

root.render(<IndexPage />)

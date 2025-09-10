import type { FC } from 'react'
import { Paper, Table, TableBody, TableCell, TableHead, TableRow, Typography } from '@mui/material'

import TableContainer from '@mui/material/TableContainer'
import { useEffect, useState } from 'react'
import { Link } from 'react-router'
import './App.css'

interface Forecast {
  date: string
  temperatureC: number
  temperatureF: number
  summary: string
}

const App: FC = () => {
  const [
    forecasts,
    setForecasts,
  ] = useState<Forecast[]>()

  useEffect(() => {
    void populateWeatherData()
  }, [])

  const contents
    = (
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Date</TableCell>
              <TableCell>Temp. (C)</TableCell>
              <TableCell>Temp. (F)</TableCell>
              <TableCell>Summary</TableCell>
            </TableRow>
          </TableHead>

          <TableBody>
            {forecasts?.map(forecast => (
              <TableRow key={forecast.date}>
                <TableCell>{forecast.date}</TableCell>
                <TableCell>{forecast.temperatureC}</TableCell>
                <TableCell>{forecast.temperatureF}</TableCell>
                <TableCell>{forecast.summary}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    )

  return (
    // <Typography variant="h1" component="h2">
    <>

      <div>
        <nav>
          <Link to="/containers">Go to containers</Link>
        </nav>
      </div>

      <Typography
        variant="h3"
        component="h2"
      >
        Weather forecast
      </Typography>

      {contents}

    </>
    //
  )

  async function populateWeatherData() {
    const response = await fetch('api/weatherforecast')
    if (response.ok) {
      const data: Forecast[] = await response.json()
      setForecasts(data)
    }
  }
}

export default App

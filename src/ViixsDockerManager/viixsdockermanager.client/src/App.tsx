import {useEffect, useState} from 'react';
import './App.css';

import TableContainer from '@mui/material/TableContainer';
import {Paper, Table, TableBody, TableCell, TableHead, TableRow, Typography} from "@mui/material";

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

function App() {
    const [forecasts, setForecasts] = useState<Forecast[]>();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents =
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

    // <table className="table table-striped" aria-labelledby="tableLabel">
    //     <thead>
    //     <tr>
    //         <th>Date</th>
    //         <th>Temp. (C)</th>
    //         <th>Temp. (F)</th>
    //         <th>Summary</th>
    //     </tr>
    //     </thead>
    //     <tbody>
    //     {forecasts.map(forecast =>
    //         <tr key={forecast.date}>
    //             <td>{forecast.date}</td>
    //             <td>{forecast.temperatureC}</td>
    //             <td>{forecast.temperatureF}</td>
    //             <td>{forecast.summary}</td>
    //         </tr>
    //     )}
    //     </tbody>
    // </table>;

    return (
        // <Typography variant="h1" component="h2">
        <>
            <Typography variant="h3" component="h2">Weather forecast</Typography>
            <Typography variant={"body1"}>
                This component demonstrates fetching data from the server.
                {contents}
            </Typography>
        </>
        //
    );

    async function populateWeatherData() {
        const response = await fetch('api/weatherforecast');
        console.log(response)
        if (response.ok) {
            const data = await response.json();
            setForecasts(data);
        }
    }
}

export default App;
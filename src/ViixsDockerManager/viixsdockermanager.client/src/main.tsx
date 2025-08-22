import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import './index.css'
import {BrowserRouter} from "react-router";
import ViixsDockerManagerRoutes from "./ViixsDockerManagerRoutes.tsx";

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <BrowserRouter>
            <ViixsDockerManagerRoutes />
        </BrowserRouter>
    </StrictMode>,
)

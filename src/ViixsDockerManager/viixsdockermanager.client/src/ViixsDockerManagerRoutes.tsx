import {Routes, Route} from "react-router";
import App from "./App.tsx";
import Containers from "./docklight/Containers.tsx";

export default function ViixsDockerManagerRoutes() {
  return (
    <Routes>
      <Route index element={<App/>}/>

      <Route path='docker'>
        <Route index element={<Containers/>} path='containers'/>
      </Route>
    </Routes>
  )
}

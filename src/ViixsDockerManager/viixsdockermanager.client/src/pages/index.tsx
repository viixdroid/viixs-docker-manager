import { useState, useEffect } from "react";
import { Navigate } from "../router.ts";
import { ListItemText, ListItem, List, Link, ListItemButton } from "@mui/material";
import type { DockLightEnvironment } from "./dock-light-environment.ts";
import type { ApiObject } from "../models/api-object.ts";
import { useNavigate } from 'react-router';

const IndexRedirectPage = () => {
  const navigate = useNavigate()
  const [environments, setEnvironments] = useState<DockLightEnvironment[]>([])

  useEffect(() => {
    getEnvironments()
  }, [])

  const getEnvironments = async () => {
    console.log('start fetching data')
    const response = await fetch('/api/docklightenvironments')

    console.log('fetched data')
    if (!response.ok) {
      navigate('/setup')
    }

    const data: ApiObject<DockLightEnvironment[]> = await response.json()
    console.log(data)
    if (!data.isSuccess) {
      navigate('/setup')
    }

    if (data.result) {
      setEnvironments(data.result)
    }
  }

  const environmentList =
    <List sx={{ width: '100%' }}>
      {environments?.map(environment => (
        <>
          <ListItem
            disablePadding
            alignItems='flex-start'
            key={environment.environmentId}
          >
            <ListItemButton component={Link} to={`${environment.environmentId}/containers`} divider={true}>
              <ListItemText
                primary={environment.name}
                secondary={environment.apiLocation}
              />
            </ListItemButton>
          </ListItem>
        </>
      ))}
    </List>

  return (
    <>
      {environmentList}
    </>
  )
}

export default IndexRedirectPage
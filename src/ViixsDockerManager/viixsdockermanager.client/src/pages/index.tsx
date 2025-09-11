import type { FC } from 'react'
import type { ApiObject } from '../models/api-object.ts'
import type { DockLightEnvironment } from './dock-light-environment.ts'
import { List, ListItem, ListItemButton, ListItemText } from '@mui/material'
import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router'

const IndexRedirectPage: FC = () => {
  const navigate = useNavigate()
  const [
    environments,
    setEnvironments,
  ] = useState<DockLightEnvironment[]>([])

  const getEnvironments = async () => {
    const response = await fetch('/api/docklightenvironments')

    if (!response.ok) {
      navigate('/setup')
    }

    const data: ApiObject<DockLightEnvironment[]> = await response.json()

    if (!data.isSuccess) {
      navigate('/setup')
    }

    if (data.result) {
      setEnvironments(data.result)
    }
  }

  useEffect(() => {
    void getEnvironments()
  }, [])

  const environmentList
    = (
      <List sx={{ width: '100%' }}>
        {environments?.map(environment => (
          <>
            <ListItem
              disablePadding
              alignItems="flex-start"
              key={environment.environmentId}
            >
              <ListItemButton
                component={Link}
                to={`${environment.environmentId}/containers`}
                divider={true}
              >
                <ListItemText
                  primary={environment.name}
                  secondary={environment.apiLocation}
                />
              </ListItemButton>
            </ListItem>
          </>
        ))}
      </List>
    )

  return (
    <>
      {environmentList}
    </>
  )
}

export default IndexRedirectPage

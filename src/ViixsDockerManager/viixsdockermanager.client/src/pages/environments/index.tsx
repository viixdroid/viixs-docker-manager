import type { FC } from 'react'
import type { ApiObject } from '../../models/api-object'
import type { DockLightEnvironment } from '../dock-light-environment'
import { List, ListItem, ListItemButton, ListItemText } from '@mui/material'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'
import { DockLightHubProvider, useDockLightHub } from '../../components/signalr/DockLightHubProvider'
import ViixsDockerManagerList from '../../components/ViixsDockerManagerList'

const EnvironmentListPage: FC = () => {
  const navigate = useNavigate()
  const { connect } = useDockLightHub()

  const [environments, setEnvironments] = useState<DockLightEnvironment[]>([])

  const connectWebSocket = async (environmentId: string) => {
    await connect(environmentId)
  }

  const navigateToContainers = (environment: DockLightEnvironment) => {
    connectWebSocket(environment.environmentId)
    navigate(`${environment.environmentId}/containers`, { state: { environment } })
  }

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

  return (
  // <DockLightHubProvider>
    <List sx={{ width: '100%' }}>
      {environments?.map(environment => (
        <ListItem
          disablePadding
          alignItems="flex-start"
          key={environment.id}
        >
          <ListItemButton
            // component={Link}
            onClick={_e => navigateToContainers(environment)}
            // to={{ pathname: `${environment.environmentId}/containers` }}
            // state={{ environment }}
            divider={true}
          >
            <ListItemText
              primary={environment.name}
              secondary={environment.apiLocation}
            />
          </ListItemButton>
        </ListItem>
      ))}
    </List>
  // </DockLightHubProvider>
  )
}

export default EnvironmentListPage

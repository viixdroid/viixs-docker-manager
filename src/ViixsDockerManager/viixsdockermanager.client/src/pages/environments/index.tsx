import type { FC } from 'react'
import type { DockLightEnvironment } from '../../models/dock-light-environment'
import { List, ListItem, ListItemButton, ListItemText } from '@mui/material'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'
import { useDockLightHub } from '../../components/providers/DockLightHubProvider'
import { useEnvironment } from '../../components/providers/EnvironmentProvider'
import DockLightEnvironmentService from '../../services/DockLightEnvironmentService'

const EnvironmentListPage: FC = () => {
  const { chooseEnvironment } = useEnvironment()
  const navigate = useNavigate()
  const { connect } = useDockLightHub()

  const [environments, setEnvironments] = useState<DockLightEnvironment[]>([])

  const connectWebSocket = async (environmentId: string) => {
    await connect(environmentId)
  }

  const navigateToContainers = (environment: DockLightEnvironment) => {
    connectWebSocket(environment.environmentId)
    chooseEnvironment(environment)
  }

  const getEnvironments = async () => {
    try {
      const response = await DockLightEnvironmentService.getAllDockLightEnvironments()
      if (response && response.length > 0) {
        setEnvironments(response)
        return
      }
      navigate('/setup')
    }
    catch {
      navigate('/setup')
    }
  }

  useEffect(() => {
    void getEnvironments()
  }, [])

  return (
    <List sx={{ width: '100%' }}>
      {environments?.map(environment => (
        <ListItem
          disablePadding
          alignItems="flex-start"
          key={environment.id}
        >
          <ListItemButton
            onClick={_e => navigateToContainers(environment)}
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
  )
}

export default EnvironmentListPage

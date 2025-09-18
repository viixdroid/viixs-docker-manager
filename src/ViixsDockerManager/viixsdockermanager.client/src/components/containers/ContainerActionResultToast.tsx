import type { AlertColor, SnackbarCloseReason } from '@mui/material'
import type { FC } from 'react'
import type { ContainerEventType } from '../../constants/ContainerActions'
import { Alert, Snackbar } from '@mui/material'
import { useEffect, useState } from 'react'
import { OnContainerKilled, OnContainerRestarted, OnContainerStarted, OnContainerStopped } from '../../constants/ContainerActions'
import { useDockLightHub } from '../providers/DockLightHubProvider'

interface ContainerActionResultToastProps {
  afterToastShown: (containerId: string) => void
}

interface ToastConfig {
  containerEventType: ContainerEventType
  message: (containerName: string, success?: boolean) => string
  severity: (success?: boolean) => AlertColor
}

const ContainerActionResultToast: FC<ContainerActionResultToastProps> = ({
  afterToastShown,
}: ContainerActionResultToastProps) => {
  const { connection } = useDockLightHub()

  const [message, setMessage] = useState<string>()
  const [openSnackBar, setOpenSnackBar] = useState<boolean>()
  const [severity, setSeverity] = useState<AlertColor>()

  const toastConfig: Record<ContainerEventType, ToastConfig> = {
    OnContainerStarted: {
      containerEventType: OnContainerStarted,
      message: (name, success = true) =>
        success
          ? `Container ${name} was started successfully`
          : `Container ${name} was NOT started successfully`,
      severity: (success = true) => (success ? 'success' : 'error'),
    },
    OnContainerStopped: {
      containerEventType: OnContainerStopped,
      message: name => `Container ${name} was stopped successfully`,
      severity: () => 'info',
    },
    OnContainerRestarted: {
      containerEventType: OnContainerRestarted,
      message: name => `Container ${name} was restarted successfully`,
      severity: () => 'info',
    },
    OnContainerKilled: {
      containerEventType: OnContainerKilled,
      message: name => `Container ${name} was killed successfully`,
      severity: () => 'error',
    },
  }

  const showContainerToast = (containerId: string, containerName: string, event: ContainerEventType, success?: boolean) => {
    const config = toastConfig[event]

    setMessage(config.message(containerName, success))
    setSeverity(config.severity(success))
    setOpenSnackBar(true)
    afterToastShown(containerId)
  }

  useEffect(() => {
    if (!connection) {
      return
    }

    Object.entries(toastConfig).forEach(([event, config]) => {
      connection.on(config.containerEventType, (containerId: string, containerName: string, isSuccesfullyStarted: boolean) =>
        showContainerToast(containerId, containerName, event as ContainerEventType, isSuccesfullyStarted))
    })
  }, [connection])

  const handleClose = (
    _event?: React.SyntheticEvent | Event,
    _reason?: SnackbarCloseReason,
  ) => {
    setOpenSnackBar(false)
  }

  return (
    <Snackbar
      anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
      onClose={handleClose}
      open={openSnackBar}
      autoHideDuration={3000}
    >
      <Alert
        onClose={handleClose}
        severity={severity}
        variant="standard"
        sx={{ width: '100%' }}
      >
        {message}
      </Alert>
    </Snackbar>

  )
}

export default ContainerActionResultToast

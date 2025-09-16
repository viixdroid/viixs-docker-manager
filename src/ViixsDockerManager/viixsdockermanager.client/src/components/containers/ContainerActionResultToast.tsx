import type { AlertColor, SnackbarCloseReason } from '@mui/material'
import type { FC } from 'react'
import { Alert, Snackbar } from '@mui/material'
import { useEffect, useState } from 'react'
import { useDockLightHub } from '../providers/DockLightHubProvider'
import { OnContainerKilled, OnContainerRestarted, OnContainerStarted, OnContainerStopped } from './ContainerActions'

interface ContainerActionResultToastProps {
  afterToastShown: (containerId: string) => void
  // handleOnContainerStopped: () => Promise<void>
}

const ContainerActionResultToast: FC<ContainerActionResultToastProps> = ({
  afterToastShown,
}: ContainerActionResultToastProps) => {
  const { connection } = useDockLightHub()
  const [message, setMessage] = useState<string>()
  const [openSnackBar, setOpenSnackBar] = useState<boolean>()
  const [severity, setSeverity] = useState<AlertColor>()

  useEffect(() => {
    if (!connection) {
      return
    }

    connection.on(OnContainerStarted, (containerId: string, containerName: string, isSuccesfullyStarted: boolean) => {
      let message: string = 'was started successfully'
      let severity: AlertColor = 'success'
      if (!isSuccesfullyStarted) {
        message = 'was NOT started succesfully'
        severity = 'error'
      }

      setMessage(`Container ${containerName} ${message}`)
      setSeverity(severity)
      setOpenSnackBar(true)
      afterToastShown(containerId)
    })

    connection.on(OnContainerStopped, (containerId: string, containerName: string) => {
      const message: string = `Container ${containerName} was stopped succesfully`
      const severity: AlertColor = 'info'
      setMessage(message)
      setSeverity(severity)
      setOpenSnackBar(true)
      afterToastShown(containerId)
    })

    connection.on(OnContainerRestarted, (containerId: string, containerName: string) => {
      const message: string = `Container ${containerName} was restarted succesfully`
      const severity: AlertColor = 'warning'
      setMessage(message)
      setSeverity(severity)
      setOpenSnackBar(true)
      afterToastShown(containerId)
    })

    connection.on(OnContainerKilled, (containerId: string, containerName: string) => {
      const message: string = `Container ${containerName} was killed succesfully`
      const severity: AlertColor = 'error'
      setMessage(message)
      setSeverity(severity)
      setOpenSnackBar(true)
      afterToastShown(containerId)
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

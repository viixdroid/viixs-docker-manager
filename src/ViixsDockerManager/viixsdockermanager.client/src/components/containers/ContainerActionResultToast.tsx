import type { AlertColor, SnackbarCloseReason } from '@mui/material'
import type { FC } from 'react'
import { Alert, Snackbar } from '@mui/material'
import { useEffect, useState } from 'react'
import { useDockLightHub } from '../signalr/DockLightHubProvider'
import { OnContainerStarted } from './ContainerActions'

interface ContainerActionResultToastProps {
  handleOnContainerStarted: () => Promise<void>
  // handleOnContainerStopped: () => Promise<void>
}

const ContainerActionResultToast: FC<ContainerActionResultToastProps> = ({
  handleOnContainerStarted,
}: ContainerActionResultToastProps) => {
  const { connection } = useDockLightHub()
  const [message, setMessage] = useState<string>()
  const [openSnackBar, setOpenSnackBar] = useState<boolean>()
  const [severity, setSeverity] = useState<AlertColor>()

  useEffect(() => {
    if (!connection) {
      return
    }

    connection.on(OnContainerStarted, (containerName: string, isSuccesfullyStarted: boolean) => {
      let message: string = 'was started successfully'
      let severity: AlertColor = 'success'
      if (!isSuccesfullyStarted) {
        message = 'was NOT started succesfully'
        severity = 'error'
      }

      setMessage(`Container ${containerName} ${message}`)
      setSeverity(severity)
      setOpenSnackBar(true)
      handleOnContainerStarted()
    })
  }, [connection])

  const handleClose = (
    _event?: React.SyntheticEvent | Event,
    reason?: SnackbarCloseReason,
  ) => {
    if (reason === 'clickaway') {
      return
    }

    setOpenSnackBar(false)
  }

  return (
    <Snackbar
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

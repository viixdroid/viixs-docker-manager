import type { FC } from 'react'
import { Box, CircularProgress } from '@mui/material'
import { useEffect, useState } from 'react'
import { Navigate } from 'react-router'
import { IsSetupFinishedQuery } from './_models/setup'

const IndexRedirectPage: FC = () => {
  const [isSetupFinished, setIsSetupFinished] = useState<boolean | null>(null)

  useEffect(() => {
    const fetchSetupStatus = async () => {
      const isSetupFinishedQuery = new IsSetupFinishedQuery()
      const result = await isSetupFinishedQuery.execute()
      setIsSetupFinished(result.isFinished)
    }
    fetchSetupStatus()
  }, [])

  return (
    <>
      {isSetupFinished === null
        ? (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh">
              {isSetupFinished === null ? <CircularProgress /> : null}
            </Box>
          )
        : (
            <>
              {isSetupFinished && <Navigate to="/environments" replace={true} />}
              {!isSetupFinished && <Navigate to="/setup" replace={true} />}
            </>
          )}
    </>
  )
}

export default IndexRedirectPage

import type { FC, SetStateAction } from 'react'
import type { ContainerDetails } from '../container-models'
import TabContext from '@mui/lab/TabContext'
import TabList from '@mui/lab/TabList'
import TabPanel from '@mui/lab/TabPanel'
import { Box, Paper, Tab, Table, TableBody, TableContainer, Typography } from '@mui/material'
import { useEffect, useState } from 'react'
import { useLocation, useParams } from 'react-router'
import ContainerDetailTableRow from '../../../../../../components/containers/ContainerDetailTableRow'
import DateTimeAgo from '../../../../../../components/DateTimeAgo'
import DockLightService from '../../../../../../services/DockLightServices'

const ContainerDetailsPage: FC = () => {
  const { environmentid } = useParams()
  const { state } = useLocation()
  const [currentTabIndex, setCurrentTabIndex] = useState<number>(1)
  const [containerDetails, setContainerDetails] = useState<ContainerDetails | null>()

  const getContainerDetails = async () => {
    if (!environmentid) {
      throw new Error('No environment found. Cannot get container details')
    }
    try {
      const result = await DockLightService.getContainerDetails(environmentid, state.container.id)
      setContainerDetails(result)
    }
    catch (error) {
      setContainerDetails(null)
      console.error(error)
    }
  }

  const handleTabChange = (_event: any, newValue: SetStateAction<number>) => {
    setCurrentTabIndex(newValue)
  }

  useEffect(() => {
    void getContainerDetails()
  }, [])

  return (
    <>
      <Typography variant="h5">
        Container details for:
        {' '}
        {`${containerDetails?.name}`}
      </Typography>
      <Box sx={{ width: '100%', typography: 'body1' }}>
        <TabContext value={currentTabIndex}>
          <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
            <TabList onChange={handleTabChange} aria-label="Container Details tabs">
              <Tab label="Overview" value={1} />
              <Tab label="Configuration" value={2} />
              {/* <Tab label="Terminal" value="3" />
              <Tab label="Logs" value="4" />
              <Tab label="Stats" value="5" /> */}
            </TabList>
          </Box>
          <TabPanel value={1}>
            <TableContainer component={Paper}>
              <Table>
                <TableBody>
                  <ContainerDetailTableRow title="Container id" content={containerDetails?.id} />
                  <ContainerDetailTableRow title="Status" content={containerDetails?.status.state} />
                  {containerDetails != null && containerDetails.status != null && containerDetails?.status?.health !== 'unknown'
                    && (
                      <ContainerDetailTableRow title="Health" content={containerDetails?.status.health} />
                    )}
                  {containerDetails?.createdTime
                    && (
                      <ContainerDetailTableRow title="Created" content={<DateTimeAgo dateTime={containerDetails.createdTime} variant="body2" />} />
                    )}
                  {containerDetails?.status.startedTime
                    && (
                      <ContainerDetailTableRow title="Started" content={<DateTimeAgo dateTime={containerDetails?.status.startedTime} variant="body2" />} />
                    )}
                </TableBody>
              </Table>
            </TableContainer>

          </TabPanel>
          <TabPanel value={2}>Item Two</TabPanel>
          {/* <TabPanel value="3">Item Three</TabPanel>
          <TabPanel value="4">Item Three</TabPanel>
          <TabPanel value="5">Item Three</TabPanel> */}
        </TabContext>
      </Box>
    </>
  )
}

export default ContainerDetailsPage

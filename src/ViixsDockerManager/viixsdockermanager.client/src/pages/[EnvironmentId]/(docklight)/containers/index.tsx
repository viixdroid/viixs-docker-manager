import type { FC } from 'react'
import type { To } from 'react-router'
import type { ApiObject } from '../../../../models/api-object.ts'
import type { ContainerSummary } from './container-models.ts'
import { PlayArrow, Stop } from '@mui/icons-material'
import { Button, Grid, IconButton, List, ListItem, ListItemText, Typography } from '@mui/material'
import ButtonGroup from '@mui/material/ButtonGroup'
import ListItemButton from '@mui/material/ListItemButton'
import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router'

const Index: FC = () => {
  const { environmentid } = useParams()
  const [containers, setContainers] = useState<ContainerSummary[]>()
  const [isError, setIsError] = useState<boolean>(false)
  const [isLoading, setIsLoading] = useState<boolean>(false)

  const getContainerData = async () => {
    try {
      setIsLoading(true)
      const response = await fetch(`/api/docklightenvironments/${environmentid}/containers`)

      if (!response.ok) {
        setContainers([])
        setIsLoading(false)
        return
      }
      // console.log(await response.text())
      const data: ApiObject<ContainerSummary[]> = await response.json()

      if (!data.isSuccess) {
        setIsError(true)
        setIsLoading(false)
        return
      }
      if (data.result) {
        setContainers(data.result)
        setIsError(false)
      }
      setIsLoading(false)
    }
    catch (error) {
      console.error(error)
      setIsError(true)
      setIsLoading(false)
    }
  }

  useEffect(() => {
    void getContainerData()
  }, [])

  const containerContent
    = (
      <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
        {containers?.map(container => (
          <>
            <ListItem
              disablePadding
              alignItems="flex-start"
              key={container.id}
              secondaryAction={(
                <>
                  <IconButton>
                    <PlayArrow />
                  </IconButton>

                  <IconButton>
                    <Stop />
                  </IconButton>
                </>
              )}
            >
              <ListItemButton
                component={Link}
                to={{ pathname: `${container.id}`, state: container.id } as To}
                divider={true}
              >
                <ListItemText
                  primary={container.name}
                  secondary={(
                    <>
                      <Typography
                        component="span"
                        variant="body2"
                        sx={{ color: 'text.secondary' }}
                      >
                        created at
                        {' '}
                        {container.created.toLocaleString()}
                        {' '}
                        with image
                        {' '}
                        {container.image}
                      </Typography>

                      { /* TODO: Fix this. */}
                      {/* <Box */}
                      {/*  component='div' */}
                      {/*  sx={{ */}
                      {/*    mt: 1, // Margin top to create space */}
                      {/*    display: 'flex', */}
                      {/*    flexWrap: 'wrap', */}
                      {/*    gap: 0.5, // Space between chips */}
                      {/*  }} */}
                      {/* > */}
                      {/*  {container.ports.map(port => ( */}
                      {/*    <Chip label={`${port.privatePort}:${port.publicPort}`} variant='filled' size='small'/> */}
                      {/*  ))} */}
                      {/* </Box> */}
                    </>
                  )}
                />
              </ListItemButton>
            </ListItem>

            {/* <Divider/> */}
          </>
        ))}
      </List>
    )

  return (
    <>
      <Typography variant="h5">
        {environmentid}
      </Typography>

      <Grid
        container
        justifyContent="space-between"
      >
        <Button onClick={getContainerData}>Refresh</Button>

        <ButtonGroup
          variant="outlined"
          aria-label="Basic button group"
        >
          <Button>Only Be</Button>
          <Button>Visible</Button>
          <Button>with mutlitple selections?</Button>
        </ButtonGroup>
      </Grid>

      {isLoading && !isError && <div><p>We loading data atm... pls wait..</p></div>}
      {!isLoading && isError && <div><p>An error from the backend.</p></div>}
      {containers && containerContent}
    </>
  )
}

export default Index

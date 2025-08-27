import {Button, IconButton, List, ListItem, ListItemText, Typography} from "@mui/material";
import type {ContainerSummary} from "./container-summary.ts";
import {useEffect, useState} from "react";
import {PlayArrow, Stop} from "@mui/icons-material";
import ListItemButton from "@mui/material/ListItemButton";
import {Link} from "react-router";
import type {ApiObject} from "../../../models/api-object.ts";

const Index = () => {
  const [containers, setContainers] = useState<ContainerSummary[]>()
  const [isError, setIsError] = useState<boolean>(false)
  const [isLoading, setIsLoading] = useState<boolean>(false)

  useEffect(() => {
    getContainerData()
  }, []);

  const getContainerData = async () => {
    console.log(
      'We started'
    )
    try {
      setIsLoading(true)
      const response = await fetch('/api/containers')

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
        setContainers(data.result);
        setIsError(false)
      }
      setIsLoading(false)
    } catch (error) {
      console.error(error)
      setIsError(true)
      setIsLoading(false)
    }
  }


  const containerContent =
    <List sx={{width: '100%', bgcolor: 'background.paper'}}>
      {containers?.map(container => (
        <ListItem

          alignItems={'flex-start'}
          key={container.id}
          secondaryAction={
            <>
              <IconButton>
                <PlayArrow/>
              </IconButton>
              <IconButton>
                <Stop/>
              </IconButton>
            </>
          }>
          <ListItemButton component={Link} to={`${container.name}`}>
            <ListItemText
              primary={container.name}
              secondary={
                <>
                  <Typography
                    component='span'
                    variant='body2'
                    sx={{color: 'text.secondary'}}
                  >
                    created at {container.created.toLocaleString()} with image {container.image}
                  </Typography>
                  { /* TODO: Fix this. */}
                  {/*<Box*/}
                  {/*  component='div'*/}
                  {/*  sx={{*/}
                  {/*    mt: 1, // Margin top to create space*/}
                  {/*    display: 'flex',*/}
                  {/*    flexWrap: 'wrap',*/}
                  {/*    gap: 0.5, // Space between chips*/}
                  {/*  }}*/}
                  {/*>*/}
                  {/*  {container.ports.map(port => (*/}
                  {/*    <Chip label={`${port.privatePort}:${port.publicPort}`} variant='filled' size='small'/>*/}
                  {/*  ))}*/}
                  {/*</Box>*/}
                </>
              }
            />
          </ListItemButton>
        </ListItem>
      ))}
    </List>

  return (
    <>
      <Button onClick={getContainerData}>Refresh</Button>
      {isLoading && !isError && <div><p>We loading data atm... pls wait..</p></div>}
      {!isLoading && isError && <div><p>An error from the backend.</p></div>}
      {containers && containerContent}
    </>
  )
    ;
}

export default Index;
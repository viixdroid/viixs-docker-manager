import {Box, Chip, IconButton, List, ListItem, ListItemText, Typography} from "@mui/material";
import type {ContainerSummary} from "./container-summary.ts";
import {useEffect, useState} from "react";
import React from "react";
import {PlayArrow, Stop} from "@mui/icons-material";
import ListItemButton from "@mui/material/ListItemButton";
import {Link} from "react-router";

const Index = () => {
  const [containers, setContainers] = useState<ContainerSummary[]>()

  useEffect(() => {
    getContainerData()
  }, []);

  const getContainerData = async () => {
    try {
      const response = await fetch('/api/containers')
      console.log(response)
      if (!response.ok) {
        setContainers([])
      }
      // console.log(await response.text())
      const data: ContainerSummary[] = await response.json()
      console.log(data);
      setContainers(data);
    } catch (error) {
      console.error('Error fetching container data:', error)
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
                  { /* TODO: Fix this. */ }
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
      {containerContent}
    </>
  )
    ;
}

export default Index;
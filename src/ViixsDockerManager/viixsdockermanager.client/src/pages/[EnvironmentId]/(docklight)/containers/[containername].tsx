import type { FC } from 'react'
// src/pages/index.tsx
import { React } from 'react'
import { useParams } from 'react-router'

const ContainerName: FC = () => {
  const params = useParams()

  return (
    <>
      <h1>
        Container details for:
        {`${params.containername}`}
      </h1>
    </>
  )
}

export default ContainerName

import type { FC } from 'react'
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

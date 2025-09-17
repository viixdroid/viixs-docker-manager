import type { FC } from 'react'
import { Navigate } from 'react-router'

const IndexRedirectPage: FC = () => {
  return (
    <Navigate to="/environments" replace={true} />
  )
}

export default IndexRedirectPage

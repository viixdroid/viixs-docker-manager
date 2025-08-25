import {Navigate} from "../router.ts";

const IndexRedirectPage = () => {
  return <Navigate to={'/containers'} replace={true}/>
}

export default IndexRedirectPage
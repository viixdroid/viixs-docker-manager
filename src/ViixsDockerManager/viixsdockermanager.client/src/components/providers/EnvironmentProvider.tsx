import type { FC, ReactNode } from 'react'
import type { DockLightEnvironment } from '../../models/dock-light-environment'
import { createContext, useContext, useState } from 'react'
import { useNavigate } from 'react-router'
import { EnvironmentStorageKey } from '../../constants/StorageKeys'

interface EnvironmentContextValue {
  environment: DockLightEnvironment | null
  chooseEnvironment: (environment: DockLightEnvironment) => void
}

interface EnvironmentProviderProps {
  children: ReactNode
}

const EnvironmentContext = createContext<EnvironmentContextValue | undefined>(undefined)

const EnvironmentProvider: FC<EnvironmentProviderProps> = ({ children }: EnvironmentProviderProps) => {
  const navigate = useNavigate()
  const [environment, setEnvironment] = useState<DockLightEnvironment | null>(() => {
    const storedEnvironment = sessionStorage.getItem(EnvironmentStorageKey)
    return storedEnvironment ? JSON.parse(atob(storedEnvironment)) : null
  })

  const chooseEnvironment = (environment: DockLightEnvironment) => {
    setEnvironment(environment)
    sessionStorage.setItem(EnvironmentStorageKey, btoa(JSON.stringify(environment)))
    navigate(`${environment.environmentId}/containers`)
  }

  return (
    <EnvironmentContext.Provider value={{ environment, chooseEnvironment }}>
      {children}
    </EnvironmentContext.Provider>
  )
}

export function useEnvironment() {
  const context = useContext(EnvironmentContext)
  if (!context)
    throw new Error('useEnvironment must be used within EnvironmentProvider')
  return context
}

export default EnvironmentProvider

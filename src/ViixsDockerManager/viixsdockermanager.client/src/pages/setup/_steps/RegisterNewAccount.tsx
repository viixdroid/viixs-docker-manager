import type { FC } from 'react'
import type { ICommand } from '../../../models/command-model'
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { Box, Button, FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput, Stack, styled, TextField, Typography } from '@mui/material'
import { useEffect, useState } from 'react'
import { useOutletContext } from 'react-router'
import { CreateUserAccountCommand } from '../_models/createuserAccount'

const FormBox = styled(Box)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  gap: theme.spacing(4),
}))

// TODO: check if there is already a user
const RegisterNewUserStep: FC = () => {
  const { registerOnBeforeNavigate, onNextStepCallback, isDisabled } = useOutletContext<{
    isDisabled: boolean
    registerOnBeforeNavigate: (callback: () => Promise<boolean>) => void
    onNextStepCallback: (callback: () => ICommand | undefined) => void
  }>()

  const [emailAddress, setEmailAddress] = useState<string>('')
  const [password, setPassword] = useState<string>('')

  const [showPassword, setShowPassword] = useState<boolean>(false)

  const [disableInput, setDisableInput] = useState<boolean>(false)

  const handleClickShowPassword = () => setShowPassword(show => !show)

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault()
  }

  const handleMouseUpPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault()
  }

  const createUserAccountCommand = (): ICommand => {
    return new CreateUserAccountCommand(emailAddress, password)
  }

  const registerNewUser = async (): Promise<boolean> => {
    if (disableInput) {
      // continue to next step if we already registered.
      return true
    }

    setDisableInput(true)
    const command = new CreateUserAccountCommand(emailAddress, password)

    try {
      await command.execute()
      return true
    }
    catch (err) {
      console.error(err)
      setDisableInput(false)
      return false
    }
  }
  useEffect(() => {
    registerOnBeforeNavigate(registerNewUser)
    onNextStepCallback(createUserAccountCommand)

    // Cleanup function to remove the callback when navigating away
    return () => {
      registerOnBeforeNavigate(() => Promise.resolve(true)) // Reset to a default no-op callback
      onNextStepCallback(() => undefined)
    }
  }, [])

  return (
    <FormBox>
      <Stack spacing={2}>
        <TextField
          fullWidth
          label="Email Address"
          type="email"
          variant="outlined"
          required
          disabled={isDisabled}
          onChange={e => setEmailAddress(e.target.value)}
        />
        <FormControl variant="outlined" fullWidth required>
          <InputLabel htmlFor="useraccount-password" required>
            Password
          </InputLabel>
          <OutlinedInput
            id="useraccount-password"
            type={showPassword ? 'text' : 'password'}
            endAdornment={(
              <InputAdornment position="end">
                <IconButton
                  aria-label={
                    showPassword ? 'hide the password' : 'display the password'
                  }
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  onMouseUp={handleMouseUpPassword}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            )}
            label="Password"
            required
            disabled={isDisabled}
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
        </FormControl>
      </Stack>
    </FormBox>
  )
}

export default RegisterNewUserStep

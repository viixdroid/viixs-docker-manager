import type { FC } from 'react'
import type { SetupStepOutletContext } from '../_models/setupHandler'
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { Box, FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput, Stack, styled, TextField } from '@mui/material'
import { useEffect, useRef, useState } from 'react'
import { useOutletContext } from 'react-router'
import { CreateUserAccountCommand } from '../_models/createuserAccount'

const FormBox = styled(Box)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  gap: theme.spacing(4),
}))

// TODO: check if there is already a user
const RegisterNewUserStep: FC = () => {
  const { onNextStepCallback, isDisabled } = useOutletContext<SetupStepOutletContext<CreateUserAccountCommand>>()

  const [emailAddress, setEmailAddress] = useState<string>('')
  const [password, setPassword] = useState<string>('')

  const [showPassword, setShowPassword] = useState<boolean>(false)

  const emailAddressRef = useRef(emailAddress)
  const passwordRef = useRef(password)

  const handleClickShowPassword = () => setShowPassword(show => !show)

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault()
  }

  const handleMouseUpPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault()
  }

  const createUserAccountCommand = (): CreateUserAccountCommand => {
    return new CreateUserAccountCommand(emailAddressRef.current, passwordRef.current)
  }

  useEffect(() => {
    emailAddressRef.current = emailAddress
    passwordRef.current = password
  }, [emailAddress, password])

  useEffect(() => {
  // Register a function once, on mount
    onNextStepCallback(() => createUserAccountCommand())

    return () => {
    // Clean up when unmounting
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
          value={emailAddress}
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

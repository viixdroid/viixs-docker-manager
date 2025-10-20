import type { FC } from 'react'
import type { ErrorDetail } from '../../../models/error-detail'
import type { SetupStepOutletContext } from '../_models/setupHandler'
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { Box, FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput, Stack, styled, TextField, useTheme } from '@mui/material'
import { useEffect, useRef, useState } from 'react'
import { useOutletContext } from 'react-router'
import { useWebSocketContext } from '../../../components/providers/WebSocketHubProvider'
import ErrorLabel from '../_components/(content)/(createNewAccount)/ErrorLabel'
import { CreateUserAccountCommand } from '../_models/userAccount'

const FormBox = styled(Box)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  gap: theme.spacing(4),
}))

// TODO: check if there is already a user
const CreateNewAccountStep: FC = () => {
  const { connection } = useWebSocketContext()
  const theme = useTheme()
  const { onNextStepCallback, isDisabled } = useOutletContext<SetupStepOutletContext<CreateUserAccountCommand>>()

  const [emailAddress, setEmailAddress] = useState<string>('')
  const [password, setPassword] = useState<string>('')

  const [showPassword, setShowPassword] = useState<boolean>(false)

  const [isPasswordError, setIsPasswordError] = useState<boolean>(false)
  const [isEmailError, setIsEmailError] = useState<boolean>(false)

  const [emailErrorTextArray, setEmailErrorTextArray] = useState<string[]>([])
  const [passwordErrorTextArray, setPasswordErrorTextArray] = useState<string[]>([])

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
    if (connection) {
      console.log(connection.connectionId)
      connection.on('OnUserCreationFailed', (errorDetails: ErrorDetail[]) => {
        console.log(errorDetails)

        // Collect all error messages for each category
        const emailErrors = Array.from(
          new Set(
            errorDetails
              .filter(errorDetail => errorDetail.category === 'Email')
              .map(errorDetail => errorDetail.description),
          ),
        )
        const passwordErrors = errorDetails
          .filter(errorDetail => errorDetail.category === 'Password')
          .map(errorDetail => errorDetail.description)

        setIsEmailError(emailErrors.length > 0)
        setEmailErrorTextArray(emailErrors)

        setIsPasswordError(passwordErrors.length > 0)
        setPasswordErrorTextArray(passwordErrors)
        console.log(passwordErrors)
      })
    }
  }, [connection])

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
          error={isEmailError}
          value={emailAddress}
          onChange={e => setEmailAddress(e.target.value)}
        />
        {isEmailError ? (<ErrorLabel errors={emailErrorTextArray} />) : null}
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
            error={isPasswordError}
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
          {isPasswordError ? (<ErrorLabel errors={passwordErrorTextArray} />) : null}
        </FormControl>
      </Stack>
    </FormBox>
  )
}

export default CreateNewAccountStep

import type { FC } from 'react'
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { Box, Button, FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput, Stack, styled, TextField, Typography } from '@mui/material'
import { useState } from 'react'
import { CreateUserAccountCommand } from '../(models)/createuserAccount'

const FormBox = styled(Box)(({ theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  gap: theme.spacing(4),
}))

// TODO: check if there is already a user
const RegisterNewUserStep: FC = () => {
  const [emailAddress, setEmailAddress] = useState<string>('')
  const [password, setPassword] = useState<string>('')

  const [showPassword, setShowPassword] = useState<boolean>(false)

  const handleClickShowPassword = () => setShowPassword(show => !show)

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault()
  }

  const handleMouseUpPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault()
  }

  const registerNewUser = async () => {
    const command = new CreateUserAccountCommand(emailAddress, password)

    try {
      await command.execute()
      // Notify next step after succes
    }
    catch (err) {
      console.error(err)
    }
  }

  return (
    <>
      <FormBox>
        <Stack spacing={2}>
          <TextField fullWidth label="Email Address" type="email" variant="outlined" required />
          <FormControl variant="outlined" fullWidth required>
            <InputLabel htmlFor="outlined-adornment-password" required>
              Password
            </InputLabel>
            <OutlinedInput
              id="outlined-adornment-password"
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
              value={password}
              onChange={e => setPassword(e.target.value)}
            />
          </FormControl>
        </Stack>
      </FormBox>
      {/* <StyledAlert severity="info">Create your account to get started with our platform</StyledAlert> */}
      {/* <Stack>
        <Typography variant="body1">
          To use Viixs docker manager, please create an account first.
        </Typography>
        <Stack spacing={1} direction="column" alignItems="start">
          <Typography variant="body1" component="div" sx={{ textAlign: 'start' }}>
            <Box component="span" sx={{ fontStyle: 'oblique' }}>
              Email
            </Box>
          </Typography>

          <TextField
            id="emailAddress"
            fullWidth
            placeholder="Your email address you wish to login with"
            variant="outlined"
            type="email"
            value={emailAddress}
            onChange={e => setEmailAddress(e.target.value)}
          />
        </Stack>

        <Stack spacing={1} direction="column" alignItems="start">
          <Typography variant="body1" component="div" sx={{ textAlign: 'start' }}>
            <Box component="span" sx={{ fontStyle: 'oblique' }}>
              Password
            </Box>
          </Typography>

          <FormControl variant="outlined">
            <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
            <OutlinedInput
              id="outlined-adornment-password"
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
              value={password}
              onChange={e => setPassword(e.target.value)}
            />
          </FormControl>
        </Stack>

        <Button
          variant="contained"
          onClick={registerNewUser}
        >
          Temporary save button
        </Button>
      </Stack> */}

    </>
  )
}

export default RegisterNewUserStep

import child_process from 'node:child_process'

import fs from 'node:fs'
import path from 'node:path'
import { env } from 'node:process'
import { fileURLToPath, URL } from 'node:url'
import generouted from '@generouted/react-router/plugin'
import react from '@vitejs/plugin-react'
import { defineConfig } from 'vite'

const baseFolder
  = env.APPDATA !== undefined && env.APPDATA !== ''
    ? `${env.APPDATA}/ASP.NET/https`
    : `${env.HOME}/.aspnet/https`

const certificateName = 'viixsdockermanager.client'
const certFilePath = path.join(baseFolder, `${certificateName}.pem`)
const keyFilePath = path.join(baseFolder, `${certificateName}.key`)

if (!fs.existsSync(baseFolder)) {
  fs.mkdirSync(baseFolder, { recursive: true })
}

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (child_process.spawnSync('dotnet', [
    'dev-certs',
    'https',
    '--export-path',
    certFilePath,
    '--format',
    'Pem',
    '--no-password',
  ], { stdio: 'inherit' }).status !== 0) { throw new Error('Could not create certificate.') }
}

const target = env['services__viixsdockermanager-server__https__0'] ?? 'https://localhost:7015'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react(),
    generouted(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  server: {
    proxy: {
      '^/api': {
        target,
        secure: false,
      },
      '^/ws': {
        target,
        secure: false,
      },
    },
    port: Number.parseInt(env.DEV_SERVER_PORT || '55596'),
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath),
    },
  },
})

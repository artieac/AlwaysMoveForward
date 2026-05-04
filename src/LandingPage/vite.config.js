import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      SharedComponents: path.resolve(__dirname, 'src/React/Components'),
      CSS: path.resolve(__dirname, 'public/css'),
      Redux: path.resolve(__dirname, 'src/React/redux'),
      Repositories: path.resolve(__dirname, 'src/React/Repositories'),
    },
  },
  server: {
    port: 9000,
  },
  root: '.',
  build: {
    outDir: 'dist',
  },
});

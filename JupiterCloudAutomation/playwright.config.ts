import { defineConfig } from '@playwright/test';
export default defineConfig({
  reporter: [['html', { outputFolder: 'playwright-report', open: 'never' }]],
  use: {
    baseURL: 'http://jupiter.cloud.planittesting.com',
    headless: true,
    screenshot: 'only-on-failure',
    video: 'retain-on-failure',
    timeout: 30000,
    navigationTimeout: 30000
  },
});
import { test as base } from '@playwright/test';

export type TestOptions = {
  testDataDir: string;
  clientId: string;
  clientKey: string;
  audience: string;
};

export const test = base.extend<TestOptions>({
  // Define an option and provide a default value.
  // We can later override it in the config.
  testDataDir: ['./test-data', { option: true }],
  clientId: ['', { option: true }],
  clientKey: ['', { option: true }],
  audience: ['', { option: true }],
});
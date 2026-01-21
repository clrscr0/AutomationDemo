import { expect } from '@playwright/test';
import { test } from '../../../test-options';
import type { User } from '../../../src/data-models/user';
import { LoginPage } from '../../../src/pages/pmdam/__commons/LoginPage';

test.describe('PMDAM UI Smoke Tests', () => {
    let loginPage: LoginPage;
    let testData: any;

    test.beforeEach(async ({ page, testDataDir }) => {
        testData = require(`${testDataDir}/pmdam.smoke.json`);
        loginPage = new LoginPage(page);
        await loginPage.navigate();
    });

    test('[1] Login to Marketing Ops', async ({ page }) => {
        const user = testData.toMarketingOps as User;
        console.log("Logging in with user: ", user);
        const marketingOpsLandingPage = await loginPage.loginToMarketingOps(user);
        await marketingOpsLandingPage.waitForPageLoaded();
        const title = await marketingOpsLandingPage.getTitle();
        expect(title).toBe('My Aprimo');
        await marketingOpsLandingPage.logoutFromMarketingOps();
    });

    test('[2] Login to Dam Spaces', async ({ page }) => {
        const user = testData.toDamSpaces as User;
        console.log("Logging in with user: ", user);
        const damspacesLandingPage = await loginPage.loginToDamSpaces(user);
        await damspacesLandingPage.waitForPageLoaded();
        const title = await damspacesLandingPage.getTitle();
        expect(title).toBe('Spaces - Aprimo');
        await damspacesLandingPage.logoutFromDam();
    });
});
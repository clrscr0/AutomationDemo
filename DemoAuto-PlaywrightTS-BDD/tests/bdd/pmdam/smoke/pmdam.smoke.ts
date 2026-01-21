import { expect, Page } from '@playwright/test';
import { User } from '../../../../src/data-models/user';
import { MarketingOpsLandingPage } from '../../../../src/pages/pmdam/__commons/MarketingOpsLandingPage';
import { createBdd } from 'playwright-bdd';
import { LoginPage } from '../../../../src/pages/pm/__commons/LoginPage';

const { Given, When, Then } = createBdd();

let loginPage: LoginPage;
let marketingOpsLandingPage: MarketingOpsLandingPage

Given('the user is on the login page', async ({ page }: { page: Page }) => {
    loginPage = new LoginPage(page);
    await loginPage.navigate();
});

When('the user logs in with valid credentials {string} and {string}', async ({ }, username: string, password: string) => {
    const user = new User(username, password);
    marketingOpsLandingPage = await loginPage.loginToMarketingOps(user);
    await marketingOpsLandingPage.waitForPageLoaded();
});

Then('the user should be redirected to the MarketingOps page', async ({ }) => {
    expect(await marketingOpsLandingPage.getTitle()).toBe("My Aprimo");
});

Then('the user logs out of the MarketingOps page', async ({ }) => {
    loginPage = await marketingOpsLandingPage.logoutFromMarketingOps();
    expect(await loginPage.getTitle()).toBe('Aprimo');
});
// @ts-check
import { Locator, Page } from '@playwright/test';
import { User } from '../../../data-models/user';
import { BasePageTemplate } from '../__templates/BasePageTemplate';
import { MarketingOpsLandingPage } from './MarketingOpsLandingPage';
import { DamSpacesLandingPage } from './DamSpacesLandingPage';

export class LoginPage extends BasePageTemplate {

  async waitForPageLoaded(): Promise<void> {
    throw new Error("Method not implemented.");
  }

  readonly usernameFld: Locator;
  readonly passwordFld: Locator;
  readonly loginBtn: Locator;
  readonly loginAsEmployeeBtn: Locator;

  constructor(page: Page) {
    super(page);
    this.usernameFld = page.locator("#Username");
    this.passwordFld = page.locator("#Password");
    this.loginBtn = page.locator("//button[@data-automation-id='login-button']");
    this.loginAsEmployeeBtn = page.locator("//button[@data-automation-id='login-as-employee-button']");
  }

  async navigate(): Promise<void> {
    await this.page.goto('/');
  }

  async loginToMarketingOps(user: User): Promise<MarketingOpsLandingPage> {
    await this.usernameFld.fill(user.username);
    await this.passwordFld.fill(user.password);
    await this.loginBtn.click();
  
    return new MarketingOpsLandingPage(this.page);
  }

  async loginToDamSpaces(user: User): Promise<DamSpacesLandingPage> {
    await this.usernameFld.fill(user.username);
    await this.passwordFld.fill(user.password);
    await this.loginBtn.click();
  
    return new DamSpacesLandingPage(this.page);
  }
}

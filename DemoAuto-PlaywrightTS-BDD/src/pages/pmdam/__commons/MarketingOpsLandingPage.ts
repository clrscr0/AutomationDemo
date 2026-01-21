import { Page } from "@playwright/test";
import { UserPageTemplate } from "../../pmdam/__templates/UserPageTemplate";

export class MarketingOpsLandingPage extends UserPageTemplate
{
    constructor(page: Page)
    {
        super(page);
    }

    async waitForPageLoaded(): Promise<void> {
        await this.page.waitForURL(/my-aprimo/);
        await this.page.waitForFunction(() => document.title.includes('My Aprimo'));
    }
}
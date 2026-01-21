import { Page } from "@playwright/test";
import { UserPageTemplate } from "../../pmdam/__templates/UserPageTemplate";

export class DamSpacesLandingPage extends UserPageTemplate {
    async waitForPageLoaded(): Promise<void> {
        await this.page.waitForURL(/dam/);
        await this.page.waitForFunction(() => document.title.includes('Spaces - Aprimo'));
    }
}
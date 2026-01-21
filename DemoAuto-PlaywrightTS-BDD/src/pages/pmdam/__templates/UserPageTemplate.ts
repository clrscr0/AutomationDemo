import { Locator, Page } from "@playwright/test";
import { BasePageTemplate } from "./BasePageTemplate";
import { LoginPage } from "../__commons/LoginPage";
import { MyProfile } from "../../pmdam/__components/MyProfile";
import { DamMyProfile } from "../__components/DamMyProfile";

export class UserPageTemplate extends BasePageTemplate
{
    private readonly myProfile : MyProfile;
    private readonly damMyProfile : DamMyProfile;

    constructor(page: Page)
    {
        super(page);
        this.myProfile = new MyProfile(page);
        this.damMyProfile = new DamMyProfile(page);
    }
    
    async waitForPageLoaded(): Promise<void> {
        throw new Error("Method not implemented.");
    }

    async logoutFromMarketingOps() : Promise<LoginPage>
    {
        await this.myProfile.myProfileBtn.click();
        await this.myProfile.logoutBtn.click();

        return new LoginPage(this.page);
    }

    async logoutFromDam() : Promise<LoginPage>
    {
        await this.damMyProfile.myProfileBtn.click();
        await this.damMyProfile.logoutBtn.click();
    
        return new LoginPage(this.page);
    }
}
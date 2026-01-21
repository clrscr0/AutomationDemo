import { Locator, Page } from "@playwright/test";

export class DamMyProfile
{
    private page : Page;    
    readonly myProfileBtn : Locator;
    readonly logoutBtn : Locator;
    
    constructor(page: Page)
    {
        this.page = page;
        this.myProfileBtn = page.locator('//button[@data-id="apptoolbar-profile-button"]');
        this.logoutBtn = page.locator('//li[@role="menuitem" and .="Log out"]');
    }
}
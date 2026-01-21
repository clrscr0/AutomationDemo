import { Locator, Page } from "@playwright/test";

export class MyProfile
{
    private page : Page;    
    readonly myProfileBtn : Locator;
    readonly logoutBtn : Locator;
    
    constructor(page: Page)
    {
        this.page = page;
        this.myProfileBtn = page.locator('//button[@data-automation-id="my-profile-header-button"]');
        this.logoutBtn = page.locator('//button[@data-automation-id="logout-button"]');
    }
}
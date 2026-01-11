import { Page } from "@playwright/test";
import { LandingPage } from "./pages/LandingPage";

export class PageObjectManager 
{
    private _landingPage?: LandingPage;

    constructor(private readonly page: Page) {}

    public get landingPage(): LandingPage {
        return this._landingPage ??= new LandingPage(this.page);
    }
}
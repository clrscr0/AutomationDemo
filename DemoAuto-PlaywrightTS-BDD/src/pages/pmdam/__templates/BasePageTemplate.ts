import { Page } from '@playwright/test';
import { BasePageInterface } from './BasePageInterface';

export class BasePageTemplate implements BasePageInterface {
    protected readonly page: Page;

    constructor(page: Page) {
        this.page = page;
    }

    async waitForPageLoaded(): Promise<void> {
        throw new Error("Method not implemented.");
    }

    async waitForContentLoaded() {
        await this.page.waitForLoadState('domcontentloaded');
        await this.page.waitForLoadState('load');
        await this.page.waitForLoadState('networkidle');
    }

    async getTitle(): Promise<string> {
        return await this.page.title();
    }

}
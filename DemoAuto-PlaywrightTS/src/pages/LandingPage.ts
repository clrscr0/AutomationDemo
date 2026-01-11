import {Page} from '@playwright/test';
import { SearchFlightFragment } from '../fragments/SearchFlightFragment';

export class LandingPage
{
    public page: Page;
    public searchFlightFragment: SearchFlightFragment;

    constructor(page: Page)
    {
        this.page = page;
        this.searchFlightFragment = new SearchFlightFragment(page);
    }

    async navigate()
    {
        await this.page.goto('/');
    }
}
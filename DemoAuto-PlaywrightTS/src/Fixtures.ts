import { PageObjectManager } from "./PageObjectManager";
import { test as base } from '@playwright/test';

type Fixture = {
    pom: PageObjectManager;
}

export const test = base.extend<Fixture>(
    {
        pom: async ({page}, use) => {
            const pom = new PageObjectManager(page);
            await use(pom);
        }
    } 
);

export { expect } from '@playwright/test';
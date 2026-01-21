import { Page, FrameLocator, Locator, expect, Frame } from '@playwright/test';

export class HelperUtil {
    private constructor() {}
    
    static async isElementExist(locator: Locator, timeoutMs = 60000, intervalMs = 200): Promise<boolean> {
        const start = Date.now();
        while (Date.now() - start < timeoutMs) {
            const count = await locator.count();
            if (count >= 1) return true;
            await new Promise(res => setTimeout(res, intervalMs));
        }
        return false;
    }
}
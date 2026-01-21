import { expect } from '@playwright/test';
import { test } from '../../../test-options';
import type { User } from '../../../src/data-models/user';
import { LoginPage } from '../../../src/pages/pmdam/__commons/LoginPage';
import { AnnotationViewer } from '../../../src/pages/pmdam/AnnotationViewer/AnnotationViewer';



test.describe('Annotation Viewer Basic Tests', () => {
    let loginPage: LoginPage;
    let testData: any;

    test.beforeEach(async ({ page, testDataDir }) => {
        testData = require(`${testDataDir}/AVTestData.json`);
        loginPage = new LoginPage(page);
        await loginPage.navigate();
    });

    test('[1] Annotation_Viewer_BasicTest_Attachment_PDF', async ({ page }) => {
        const user = testData.toMarketingOps as User;
        console.log("Logging in with user: ", user);
        const marketingOpsLandingPage = await loginPage.loginToMarketingOps(user);
        await marketingOpsLandingPage.waitForPageLoaded();
        const title = await marketingOpsLandingPage.getTitle();
        expect(title).toBe('My Aprimo');
        
        let annotationViewer = new AnnotationViewer(page);

        await page.goto('/MarketingOps/#/annotation-viewer-ng/asset/2101363/version/2105184?contextObject=review-task&contextObjectId=2213867');
        await annotationViewer.waitForCanvasLoaded();

        await marketingOpsLandingPage.logoutFromMarketingOps();
    });
});
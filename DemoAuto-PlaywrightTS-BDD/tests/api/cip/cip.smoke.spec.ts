import { expect } from '@playwright/test';
import { test } from '../../../test-options';
import { User } from '../../../src/data-models/user';

test.describe('CIP API Smoke Tests', () => {
    let authToken: string;
    let testData: any;  

    //Runs only once and stores authToken for reuse in tests
    test.beforeAll(async ({ request, clientId, clientKey, audience, testDataDir }) => {
        testData = require(`${testDataDir}/cip.smoke.json`);
        const user = testData.user as User;  

        const postData = new URLSearchParams();
        postData.append('grant_type', 'client_credentials');
        postData.append('client_id', clientId);
        postData.append('client_secret', clientKey);
        postData.append('audience', audience);

        const authResponse = await request.post('https://personifyxp.eu.auth0.com/oauth/token', {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
              },
            data: postData.toString()
        });

        console.log("authResponse", authResponse);
        expect(authResponse.ok()).toBeTruthy(); // Ensure login was successful

        const authResponseBody = await authResponse.json();
        authToken = authResponseBody.access_token; // Extract the token from the response, adjust based on your API response structure.
        expect(authToken).toBeTruthy(); //Ensure token is not empty.
    });

    test('GetAssetsList: Get List of Assets', async ({ request }) => {
        const page = 0
        const size = 10
        
        const response = await request.get(`/ci/data/locations?page=${page}&size=${size}`, {
            headers: {
                Authorization: `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            },
        });
        
        expect(response.ok()).toBeTruthy()
        const responseBody = await response.json()
        expect(responseBody).toHaveProperty('data')
        const assetsData = responseBody.data

        expect(Array.isArray(assetsData)).toBe(true)
      
        console.log('Number of assets:', assetsData.length)
        expect(assetsData.length).toBeLessThan(size)
      
        const assetNames = assetsData.map((asset: { name: string; }) => asset.name)
        console.log('Asset Names:', assetNames)
    });

    test('GetBehaviorsList: Get List of Behaviors', async ({ request }) => {
        const page = 0
        const size = 10
        
        const response = await request.get(`/ci/data/behaviors?page=${page}&size=${size}`, {
            headers: {
                Authorization: `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        });
        
        expect(response.ok()).toBeTruthy()
        const responseBody = await response.json()
        expect(responseBody).toHaveProperty('data')
        const behaviorsData = responseBody.data

        expect(Array.isArray(behaviorsData)).toBe(true)
      
        console.log('Number of behaviors:', behaviorsData.length)
        expect(behaviorsData.length).toBeLessThan(size)

        behaviorsData.forEach((behavior: { code: any; tags: any; }) => {
            console.log("Behavior Code: ", behavior.code)
            const tags = behavior.tags
            const tagNames = tags.map((tag: { name: string; }) => tag.name)

            console.log("Tag Names: ", tagNames)
        });
    });
});
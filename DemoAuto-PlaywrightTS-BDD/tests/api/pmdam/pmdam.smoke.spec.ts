import { expect } from '@playwright/test';
import { test } from '../../../test-options';
import { User } from '../../../src/data-models/user';

test.describe('PMDAM API Smoke Tests', () => {
    let authToken: string;
    let testData: any;

    test.beforeAll(async ({ request, clientId, testDataDir }) => {
        testData = require(`${testDataDir}/pmdam.smoke.json`);
        const user = testData.user as User;
        const authResponse = await request.post('/api/oauth/create-native-token', {
            headers: {
                'Authorization': `Basic ${Buffer.from(`${user.username}:${user.password}`).toString('base64')}`,
                'content-type': 'application/json',
                'client-id': clientId
              },
        });

        console.log("authResponse", authResponse);
        expect(authResponse.ok()).toBeTruthy(); // Ensure login was successful

        const authResponseBody = await authResponse.json();
        authToken = authResponseBody.accessToken; // Extract the token from the response, adjust based on your API response structure.
        expect(authToken).toBeTruthy(); //Ensure token is not empty.
    });

    test('API View Search', async ({ request }) => {
        // Example: Fetch user profile data
        const response = await request.get('/api/ui/init', {
            headers: {
                Authorization: `Bearer ${authToken}`, // Include the token in the Authorization header
            },
        });

        expect(response.ok()).toBeTruthy();
        const responseBody = await response.json();
        expect(responseBody).toHaveProperty('userId'); // Example assertion: Check for a specific property in the response
        //Add more assertions as needed.
        console.log('Response', responseBody);
    });
});
import test, { expect, request } from "@playwright/test";
import { UserController } from "../controller/usercontroller";

test.describe("Demo API Test", async () => {

    test.beforeEach("", async({request}) => {
        //set auth
    });

    test("Test get", async({request}) => {
        const response = await request.get(`/get`);
        console.log(`status: ${response.status()}`)
        expect(response.status()).toBe(200);

        const body = await response.json();     
        console.log(`body: ${body.headers.host}`)
        expect(body.headers.host).toBe('postman-echo.com');
    })

    test("Test post", async({request}) => {
        const userController = new UserController(request);
        const response = await userController.createUser({ name: 'Jane', job: 'Dev' });
        
        expect(response.status()).toBe(200);
    })
});
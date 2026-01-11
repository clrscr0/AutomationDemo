import { APIRequestContext } from '@playwright/test';

export class UserController {
  constructor(private request: APIRequestContext) {}

  async createUser(userData: object) {
    return await this.request.post('/post', { data: userData });
  }
}
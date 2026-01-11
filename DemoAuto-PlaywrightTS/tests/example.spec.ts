import { test, expect } from '@playwright/test';

test.describe("Demo", async () => {

  test.beforeEach(async ({page}) => {
    await page.goto('/');
  });

  test('test flight', async ({page}) => {
    const acceptCookiesBtn = page.getByRole('button', { name: 'Accept' });
    await acceptCookiesBtn.waitFor({'state': 'visible'});
    await acceptCookiesBtn.click();

    const departureField = page.getByRole('textbox', { name: 'Departure airport' });
    await departureField.fill('MNL');
    await departureField.press('Enter');
    const depCodeDrp = page.getByText('MNL');
    await depCodeDrp.waitFor({'state':'visible'});
    await depCodeDrp.click();
    await page.waitForTimeout(1000);

    const arrivalField = page.getByRole('textbox', { name: 'Arrival airport' });
    await arrivalField.fill('DXB');
    await arrivalField.press('Enter');
    const arrCodeDrp = page.getByText('DXB');
    await arrCodeDrp.waitFor({'state':'visible'});
    await arrCodeDrp.click();

    const depDate = page.getByRole('button', { name: 'Saturday, 24 January' });
    await depDate.click();
    const arrDate = page.getByRole('button', { name: 'Saturday, 31 January' });
    await arrDate.click();

    const paxCount = 1;
    for(let i = 0; i <= paxCount; i++){
      const currPaxCount = await (await page.getByRole('combobox', { name: 'Passengers'}).inputValue()).replace(/\D/g, '');
      if(parseInt(currPaxCount) == paxCount) break;
      await page.getByRole('button', { name: 'Increase number of Adult' }).click();
    }
    
    await page.locator('.js-dropdown.dropdown-container.mobile-bubble-dropdown > div > .dropdown__input-container > .field > .clear-x-mobile').first().click();
    await page.getByRole('option', { name: 'Premium Economy' }).click();
    await page.getByRole('button', { name: 'Search flights' }).click();
  });

});

import { Locator, Page } from "@playwright/test";

export class SearchFlightFragment 
{
    private readonly acceptCookiesBtn: Locator;
    private readonly departureField: Locator;
    
    constructor(private readonly page:Page){
        this.acceptCookiesBtn = page.getByRole('button', { name: 'Accept' });
        this.departureField = page.getByRole('textbox', { name: 'Departure airport' });
    }

    async acceptCookiesIfPresent()
    {
        //if(await this.acceptCookiesBtn.isVisible())
            await this.acceptCookiesBtn.click();
    }

    async setDepartureField(depCode: string)
    {
        await this.departureField.fill(depCode);
        await this.departureField.press('Enter');
        const depCodeDrp = this.page.getByText(depCode);
        await depCodeDrp.waitFor({'state':'visible'});
        await depCodeDrp.click();
        await this.page.waitForTimeout(1000);
    }
}
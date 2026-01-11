import { Trip } from "../src/data-models/Trip";
import { test } from "../src/Fixtures";

test.describe("Demo", async () => {

    // 2. Add 'async' to the hook and 'await' to the action
    test.beforeEach(async ({ pom }) => {
        await pom.landingPage.navigate();
    });

    test("Demo", async ({ pom }) => {
        const trip: Trip = {
            flights: [{
                'airportCode':'MNL',
                date: new Date()
            }]
        };

        await pom.landingPage.searchFlightFragment.acceptCookiesIfPresent();
        await pom.landingPage.searchFlightFragment.setDepartureField(trip.flights[0].airportCode);
    });
});
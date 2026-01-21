export interface BasePageInterface {
    // You can add custom wait implemented in child methods
    waitForPageLoaded(): Promise<void>;
}
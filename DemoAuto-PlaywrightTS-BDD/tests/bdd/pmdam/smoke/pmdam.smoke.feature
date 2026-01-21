Feature: Smoke Tests

  Scenario: User logs in to Marketing Ops
    Given the user is on the login page
    When the user logs in with valid credentials "cortiaga" and "aprimo123456"
    Then the user should be redirected to the MarketingOps page
    Then the user logs out of the MarketingOps page
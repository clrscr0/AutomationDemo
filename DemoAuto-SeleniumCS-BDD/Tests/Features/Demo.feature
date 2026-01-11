Feature: Flight Search
  As a traveler
  I want to search for roundtrip flights
  So that I can see available travel options

@search @demo
Scenario Outline: Search for a roundtrip flight between two cities
    Given I am on the landing page
    When I enter "<Origin>" as the departure city
    And I enter "<Destination>" as the arrival city
    And I select a departure date "2" days from now
    And I select a return date "4" days from now
    And I click on the Search Flight button
    Then I should see a list of available flights for "<Departure>" to "<Arrival>"

    Examples: 
      | Origin   | Destination |
      | DXB      | MNL         |
      | DXB      | SIN         |
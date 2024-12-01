Feature: Banking Project Deposit

  Scenario: Deposit to customer account
    Given the banking page is open
    And the user logs in as "Hermoine Granger"
    And the user selects account "1001"
    When the user deposits "500"
    Then the balance should be updated correctly

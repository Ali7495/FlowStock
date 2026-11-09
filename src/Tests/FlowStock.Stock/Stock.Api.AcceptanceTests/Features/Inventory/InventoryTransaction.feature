Feature: Inventory Transaction

  Background: 
    Given The user has permission to manage inventory

  Rule: Adding stock increases product's available quantity

    Scenario Outline: Add stock to an active product
      Given an active product exists with <initialStock> units in stock
      When the user adds <quantity> units to the inventory
      Then the inventory transaction should be created successfully
      And the product stock should be <expectedStock> units
      And the transaction should have type "Addition" and quantity <quantity>

      Examples:
        | initialStock | quantity | expectedStock |
        | 0            | 100      | 100           |
        | 50           | 100      | 150           |
        | 200          | 30       | 230           |

  Rule: Deducting stock decreases product's available quantity

    Scenario Outline: Deduct stock from a product
      Given an active product exists with <initialStock> units in stock
      When the user deducts <quantity> units from the inventory
      Then the inventory transaction should be created successfully
      And the product stock should be <expectedStock> units
      And the transaction should have type "Deduction" and quantity <quantity>

        Examples:
          | initialStock | quantity | expectedStock |
          | 200          | 30       | 170           |
          | 100          | 50       | 50            |
          | 50           | 10       | 40            |

  Rule: Avoid stock less than 0        
  
    Scenario: Deduct more stock than available
      Given an active product with 100 units in stock
      When the user deducts 120 units from the inventory
      Then the inventory transaction should not be created
      And the product's stock remains with 100 units

  Rule: Can not set invalid numbers for inventory transaction

    Scenario Outline: Reject invalid transaction quantity
      Given an active product exists with 100 units in stock
      When the user adds <quantity> units to the inventory
      Then the inventory transaction should not be created

      Examples:
        | quantity |
        | 0        |
        | -10      |     
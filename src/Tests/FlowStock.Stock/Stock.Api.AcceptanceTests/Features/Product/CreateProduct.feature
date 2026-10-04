Feature: Create product

  Background:
    Given the current user has permission to create products

  Scenario: Create a product with valid information
    Given an active product category exists
    When the user creates a product in that category
    Then the product should be created successfully
    And the created product identifier should be returned

  Scenario: Creating a product for nonexistent category
    Given the selected product category does not exist
    When the user creates a product in that category
    Then product creation should be rejected
    And no product should be stored

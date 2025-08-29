
Feature: RecipesController GetAll

Scenario: RecipesController_GetAll
Given there are recipes
When a request is received to retrieve these recipes
Then the recipes are returned

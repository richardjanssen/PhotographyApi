
Feature: PhotosController Get

Scenario: PhotosController_Get
Given a number of photos in the database
When a request is received to retrieve these photos
Then the photos are returned

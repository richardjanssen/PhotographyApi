
Feature: PhotosController Get

Scenario: PhotosController_Get
Given there are photos in the homepage album
When a request is received to retrieve these photos
Then the photos are returned

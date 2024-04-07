# Independent Reserve Postman Collection

## Intro

This Postman collection is designed to simplify integration and testing process for third party developers. This accelerates development, fosters a better understanding of the API, and encourages more developers to adopt and integrate the API into their projects.

Please refer https://www.independentreserve.com/au/features/api for more detailed description.

## Authentication and Private Methods

In order to access private endpoints, set the `apiKey` and `apiSecret` collection variables.

See the `Pre-request Script` in the `Private` folder to see how requests are signed before being sent to the server. 

This can be adopted in your own project and migrated to any other programming language.

# VentionTask
This is an API built using .NET WebAPI to manage CSV data containing information about users. The API provides two endpoints for uploading CSV data and retrieving user information.

## Getting Started

Before running the API, make sure you have cloned the repository and set up the database. Follow these steps to get started:

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio or your preferred IDE.
3. Open the `appsettings.json` file and set the connection string for your database.
4. Open the Package Manager Console and run the following command to apply the database migration:
    ```
        update-database
   ```
    This will create the necessary tables in the database to store user information.
   
## Example CSV Files

To test the API, two example CSV files are provided:

1. `csv_file_for_checking.csv`: This CSV file contains user data with the following format without header:
   ```username,useridentifier,age,city,phonenumber,email```
2. `csv_file_for_checking_2.csv`: This CSV file also contains user data with the same format as above but some data is changed to check update functionality.

## API Documentation

The API is documented using Swagger/OpenAPI. You can access the API documentation by running the application and navigating to the following URL:
```
        https://localhost:<port>/swagger
   ```
Replace `<port>` with the port number specified in your application configuration.

## Endpoint 1: Upload Users

This endpoint allows users to upload a CSV file containing user information. The structure of the CSV file should follow the format:

The API will parse the CSV file, and for each user record, it will check if the user already exists in the database. If the user does not exist, it will be added to the database. If the user already exists, the API will update the user's record with the new data from the CSV file.

### Request

- **HTTP Method:** POST
- **Route:** /api/users/upload
- **Request Body:** Form file containing the CSV data

### Response

- If the upload is successful, the API will return a 200 OK status code with the message "Users uploaded successfully."
- If there is an error during the upload process, the API will return a 400 Bad Request status code with an error message describing the issue.

## Endpoint 2: Get Users Ascending

This endpoint allows users to retrieve a set of user objects sorted in ascending order based on the "username" property. Users can also specify the maximum number of users (objects) they want to include in the API response.

### Request

- **HTTP Method:** GET
- **Route:** /api/users
- **Query Parameters:**
  - `limit`: Optional. The maximum number of users to include in the response. Default is 10.

### Response

- If the request is successful, the API will return a 200 OK status code with a JSON array containing the user objects.
- If there is an error during the request, the API will return a 400 Bad Request status code with an error message describing the issue.

## Error Handling

The API handles various error scenarios, including invalid file uploads, missing CSV headers, invalid data in the CSV file, and database-related errors. When an error occurs, the API returns an appropriate error message in the response to help users understand the issue.

## Dependencies

The API uses the following dependencies:

- `CsvHelper`: A library for reading and writing CSV files.
- `Microsoft.EntityFrameworkCore`: Entity Framework Core for database operations.

## Data Persistence

The API uses Entity Framework Core to persist user data in a database. The database connection string is provided in the app configuration.

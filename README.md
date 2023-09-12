# Library Management System

This application is designed for library management, allowing you to track books, lend them, and add new books to the library.

## Technologies Used

- .NET Core MVC
- Postgresql Database
- Entity Framework Core ORM
- HTML5, Bootstrap, and jQuery

## Requirements

This application meets the following requirements:

1. The ability to view a list of books in the library.
2. Each book's title, author, image, and current status (in the library or checked out) are displayed.
3. Books are listed in alphabetical order.
4. Books currently in the library can be lent.
5. When a book is lent, the borrower's name and return date are recorded.
6. New books can be added to the library, including their images.
7. Changes to books and deletions can be made directly in the database.
8. User-friendly interface and error handling are provided.
9. Application errors are captured and logged.

## User Guide

1. Run the application.
2. On the main page, you can view books in the library.
3. You can lend a book by clicking the "Lend" button.
4. You can add a new book using the "Add Book" link.

## Installation

1. Clone this project.
2. Update your database connection settings in the `appsettings.json` file.
3. To create the database and add tables, run the following command: `dotnet ef database update`



## Screenshots
### Home Page (Books that are currently unavailable are marked with a red title)
![image](https://github.com/mehmetali10/case-study-library/assets/77547122/58699bbb-4249-4afa-b48f-c1882042e5bb)

### Add Book Modal
![image](https://github.com/mehmetali10/case-study-library/assets/77547122/c123d89d-115c-47f9-b193-24eb241d740f)

### Give Barrow Modal
![image](https://github.com/mehmetali10/case-study-library/assets/77547122/48b38305-f512-46ee-8b8f-32b6ccee699c)

### Book Details

![image](https://github.com/mehmetali10/case-study-library/assets/77547122/36ea672b-7cfd-4c36-91ca-6ed5d0bf3388)

![image](https://github.com/mehmetali10/case-study-library/assets/77547122/e791ece2-93e0-4d41-887b-698ffbe29d9c)


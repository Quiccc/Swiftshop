# Swiftshop

Swiftshop is a shopping list application that is created for Techcareer.net ASP.NET MVC Bootcamp as a final project. Swiftshop is built with ASP.NET and Bootstrap with MVC structure.

# How to Deploy the Swiftshop

To deploy the project: 
- Clone the master branch of this repository. 
- Then establish the database connection by changing the "DbConnection" attribute of appsettings.json with your own database connection string.
- Rebuild the project and start deployment via IIS Express.

Swiftshop features data seeds for an admin user alongside of some example categories, subcategories and products. If these data seeds do not work, add a new migration and update the database by hand.

## Seeded Admin Credentials

    Email: admin@swiftshop.com
    Password: Swiftshop2023_

## Main Features

- Authentication and role based authorization via Identity Framework.
- MS SQL Server database for data management.
- Fully working admin panel for managing the platform.
- Custom front-end design with Bootstrap 5.

## Shopping List Features

- Creating and deleting shopping lists.
- Adding and removing products into each shopping list.
- Attaching notes to each product in a shopping list.
- Adding individual shopping lists to favorites for ease of use.
- A shopping list is created for each completed shopping list if a purchase is made.

## Favorite List Feature
- A favorited shopping list will not be deleted from the main page. Instead it will get hidden to later get activated by the user. 
- Purchasing a product does not remove that product from a favorited shopping list.
- User can remove a list from favorites at the main page or their profile page. Then the list will be able to get deleted completely.

## List History Feature
- After a shopping cycle is completed. If the user made a purchase of one or multiple products, a history of that shopping list will be created.
- User can view their shopping histories at their profile page and delete their history if they wish.

# Passion Project for HTTP5212

Cafe database project built in ASP.Net.

## Functionality

This project allows users to:

1. See a list of cafes
2. See a list of coffees
3. See details on each cafe
4. See details on each coffee
5. Update exisiting cafes
6. Update existing coffees
7. Add a new cafe
8. Add a new coffee
9. See reviews associated with each cafe
10. See coffees associated with each cafe
11. See details of each review
12. Associate a coffee with a cafe
13. Delete a cafe
14. Delete a coffee

## Database structure

The project relies on 3 main tables:

1. cafes
2. coffees
3. reviews

The cafes table holds a M-M relationship with coffees as one cafe can serve many coffees, and one coffee can be offered at multiple cafes. The reviews table holds a 1-M relationship with cafes as one cafe can have many reviews, but a review can only be for one cafe.

## Future development

This project can be improved further by resolving the following errors/issues/shortcomings:

1. Flaws in build result in M-M relationship failing to be represented in updating/adding cafes
2. Errors with bootstrap resulting in basic design and lack of responsive default styling
3. Images not incorporated in database design
4. Representation of reviews to be improved upon (design)


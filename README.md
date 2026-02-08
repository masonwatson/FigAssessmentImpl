# **FIG Assessment Implementation**

> This repository contains my edits for the [FIG Code Review Assessment](https://github.com/andrewbarnettfig/FIG.CodeReview.Assessment2025).
> I have provided quick links below to easily navigate through the project!

---

## **Server Side Code Edits**
- ### **Controllers**
   - [User Controller](./FigAssessmentImpl.Api/Controllers/UserController.cs)
   - [Product Controller](./FigAssessmentImpl.Api/Controllers/ProductController.cs)
- ### **Application Models and Interfaces**
   - Users
      - Create Users
         - [CreateUserRequest Model](./FigAssessmentImpl.Application/Users/CreateUsers)
      - Get Users
         - [GetUserQueryOptions Model](./FigAssessmentImpl.Application/Users/GetUsers/GetUserQueryOptions.cs)
         - [GetUserResponse Model](./FigAssessmentImpl.Application/Users/GetUsers/GetUserResponse.cs)
      - Validate Users
         - [ValidateUserRequest Model](./FigAssessmentImpl.Application/Users/ValidateUsers/ValidateUserRequest.cs)
      - [UserRepository Interface](FigAssessmentImpl.Application/Users/IUserRepository.cs)
      - [UserService Interface](./FigAssessmentImpl.Application/Users/IUserService.cs)
   - Products
      - Create Products
         - [CreateProductRequest Model](./FigAssessmentImpl.Application/Products/CreateProducts)
      - Get Products
         - [GetProductQueryOptions Model](./FigAssessmentImpl.Application/Products/GetUsers/GetProductQueryOptions.cs)
         - [GetProductResponse Model](./FigAssessmentImpl.Application/Products/GetUsers/GetProductResponse.cs)
      - [ProductRepository Interface](FigAssessmentImpl.Application/Products/IProductRepository.cs)
      - [ProductService Interface](./FigAssessmentImpl.Application/Products/IProductService.cs)
- ### **Domain Models**
   - [User Model](./FigAssessmentImpl.Domain/Users/User.cs)
   - [Product Model](./FigAssessmentImpl.Domain/Products/Product.cs)
- ### **Application Services**
   - [UserService Service](./FigAssessmentImpl.Application/Users/UserService.cs)
   - [Product Service](./FigAssessmentImpl.Application/Products/ProductService.cs)
- ### **SQL Repositories**
   - [User SQL Repository](./FigAssessmentImpl.Infrastructure/Repositories/SqlUserRepository.cs)
   - [Product SQL Repository](./FigAssessmentImpl.Infrastructure/Repositories/SqlProductRepository.cs)

# **FIG Assessment Example 1 and 2 Edits**

> This repository contains my edits for Example 1 and 2 of the [FIG Code Review Assessment](https://github.com/andrewbarnettfig/FIG.CodeReview.Assessment2025).
> I have provided quick links below to easily navigate through the project!

---

## **Server-side Code Edits (Example 1 and 2)**
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
         - [GetProductQueryOptions Model](./FigAssessmentImpl.Application/Products/GetProducts/GetProductQueryOptions.cs)
         - [GetProductResponse Model](./FigAssessmentImpl.Application/Products/GetProducts/GetProductResponse.cs)
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

## **Client-side Code Edits (Example 3)**
- ### **Component**
   - [User Profile Component TypeScript](https://github.com/masonwatson/FigAssessmentExample3/blob/master/Example3CodeEdits/user-profile.component.ts)
- ### **Store**
   - [User Profile Store](https://github.com/masonwatson/FigAssessmentExample3/blob/master/Example3CodeEdits/user-profile.store.ts)
- ### **Service**
   - [User Profile Service](https://github.com/masonwatson/FigAssessmentExample3/blob/master/Example3CodeEdits/users.service.ts)
- ### **Models**
   - [User Profile Model](https://github.com/masonwatson/FigAssessmentExample3/blob/master/Example3CodeEdits/user-profile.model.ts)
   - [User Profile Form Model](https://github.com/masonwatson/FigAssessmentExample3/blob/master/Example3CodeEdits/user-profile.form.model.ts)
   - [User Profile API Models](https://github.com/masonwatson/FigAssessmentExample3/blob/master/Example3CodeEdits/user-profile.api.models.ts)

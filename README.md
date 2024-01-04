# Fullstack Project

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)

This project involves a Fullstack project with React and Redux in the frontend and ASP.NET Core 7 in the backend as a finnal projectr for Integrfy Academy.

- Frontend: SASS, TypeScript, React, Redux Toolkit
- Backend: ASP.NET Core, Entity Framework Core, PostgreSQL

You can follow the same topics as your backend project or choose the alternative one, between E-commerce and Library. You can reuse the previous frontend project, with necessary modification to fit your backend server.

## Table of Contents

1. [About the repository](#about-the-repositpry)
2. [Features](#features)
3. [Entity Relationship Diagram](#entity-relationship-diagram)
4. [Implemented](#implemented)

## About the repository

This repository is used only for backend server. The frontend server is in a separate repository [here](https://github.com/ericpastor/practice-frontend-e-commerce).

### Backend

Generate a solution file inside this repository. All the project layers of backend server should be added into this solution.

## Features

#### User Functionalities

1. User Management: Users should be able to register for an user account and log in. Users cannot register themselves as admin.
2. Browse Products: Users should be able to view all available products and single product, search and sort products.
3. Add to Cart: Users should be able to add products to a shopping cart, and manage cart.
4. Checkout: Users should be able to place orders.

#### Admin Functionalities

1. User Management: Admins should be able to view and delete users.
2. Product Management: Admins should be able to view, edit, delete and add new products.
3. Order Management: Admins should be able to view all orders.

## Entity Relationship Diagram

![image](https://github.com/ericpastor/practice-frontend-e-commerce/assets/110885492/dfb9d1aa-3efb-43eb-bf4b-bb17f8d4ae3f)

## Implemented

1. Apply CLEAN architecture in your backend.

   - Comm.WebAPI (Framework - Configuration and data access)

     - Repositories
     - Database
     - Program

   - Comm.Controller

     - Request/Response
     - Autorization

   - Comm.Business

     - Services & Services Intefaces
     - DTO (Data Transfer Objects)
     - Custom Exeption
     - Mapper Profile
     - Password Service

   - Comm.Core

     - Entities
     - Repositories Interfaces
     - Parameters

2. Unit testing (xunit) for Service(Use case) layer.

3. Document with Swagger.

Visit Comm.WebAPI [here](https://comm2024.azurewebsites.net/index.html)

- A few screenshot examples (registering, getting token, authorizing, seeing profile (needs authorization))

  - Register

  ![image](https://github.com/ericpastor/practice-frontend-e-commerce/assets/110885492/dc0c8823-ad33-49be-a33d-8c17ddbef244)

  - Get the Token

  ![image](https://github.com/ericpastor/practice-frontend-e-commerce/assets/110885492/b1a7f05e-71cc-4b5f-8fa7-b9fbf65c98cc)
  ![image](https://github.com/ericpastor/practice-frontend-e-commerce/assets/110885492/a4867a13-f76a-4065-bfd6-fb1733b54e2f)

  - Auzorize (Write: Bearer followed by the token obtained)

  ![image](https://github.com/ericpastor/practice-frontend-e-commerce/assets/110885492/510d22a6-be91-400d-b744-7162e25fd6d0)

  - Now profile, that needs authorization is available:

  ![image](https://github.com/ericpastor/practice-frontend-e-commerce/assets/110885492/e0901226-fe8c-4df5-9d73-cb916521d7dd)

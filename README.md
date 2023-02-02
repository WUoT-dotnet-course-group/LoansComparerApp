# Loans Comparer Application

Web application with backend written in C# with .NET 6 and Angular used on frontend. Realized as a team project for .NET classes at Faculty of Mathematics and Information Science of the Warsaw University of Technology. Project was realized in Scrum.

## Description

Loans Comparer is an application for simulating taking a loan process. User can enter inquiry details and will see ranking of loan offers generated by multiple bank APIs (team has provided one here: <https://github.com/WUoT-dotnet-course-group/LoaningBankAPI> and also connected to lecturer's and other team API). After decision made by a debtor, offer submission form is displayed. User to complete the process needs to download an agreement and upload signed copy. After that debtor can see offer details under link that was sent on debtor's email address. If debtor was a logged user he can also see the history of made inquiries and see their details from there.

Application provides access for employee of a bank (which API was created by the team). Bank employee can see all inquiries made for his bank and accept/reject submitted offers related to them.

## Technologies

- Backend:
  - .NET 6

  - ASP.NET Core 6.0

  - Data storage
    - relational SQL database

    - Entity Framework Core (Code First approach)

  - File storage (Loaning Bank API)
    - Azure Blob Storage

  - Email sending
    - Azure Communication Services

  - Authentication & Authorization
    - Google OAuth

    - JWT Auth

  - Tests (in the future)

  - Mapster (<https://github.com/MapsterMapper/Mapster>)

  - SonarLint

- Frontend:
  - Angular CLI 14.2.7

  - TypeScript

  - Angular Material

- Version control:
  - Git (simplified Git flow)

  - Github (repository hosting)

- CI/CD:
  - GitHub (GitHub Actions)

- Orchestration/Containerization
  - Docker (in the future)

- Project Management / Scrum / Agile:
  - Azure DevOps

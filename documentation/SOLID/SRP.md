# Single Responsibility Principle (SRP)

## 1. Description

The Single Responsibility Principle (SRP) states that a class should have only one reason to change, meaning it should be responsible for only one part of the functionality provided by the software. This helps make the code more modular, easier to maintain, and reduces the risk of introducing bugs when making changes.

## 2. Objective

The goal of SRP is to ensure that each class or module in the system is focused on a single responsibility, making the system more robust, flexible, and easier to test. By isolating responsibilities, we avoid making a class too complex or overloaded with multiple purposes.

## 3. How SRP is applied in this project

Below are several instances where SRP has been applied in different parts of the project, divided by application layers, along with examples and diagrams to illustrate each case.

---

## Application Layer

### Example 1: User Management

In this example, SRP is applied by separating user management operations from validation logic.

- **Where it is used:** `UserManager` and `UserValidator`.
- **What it does:**
  - The `UserManager` is responsible for creating and updating users, ensuring the separation of core user management logic.
  - The `UserValidator` is solely responsible for validating user input, such as checking the format of emails or password strength.

#### Code Example

```csharp
public class UserManager
{
    public void CreateUser(UserData userData)
    {
        // Logic for creating a user
    }

    public void UpdateUser(int userId, UserData userData)
    {
        // Logic for updating a user
    }
}

public class UserValidator
{
    public bool ValidateEmail(string email)
    {
        // Logic for validating email format
        return true; // Placeholder
    }

    public bool ValidatePassword(string password)
    {
        // Logic for validating password strength
        return true; // Placeholder
    }
}
```

The clear separation of these responsibilities ensures that if validation logic changes (e.g., stricter password rules), only the `UserValidator` class will be affected.

#### Diagram
[Insert Diagram: User Management SRP]

---

### Example 2: User Notifications

This example demonstrates SRP by separating user-related notification logic from user account management.

- **Where it is used:** `NotificationService` and `UserManager`.
- **What it does:**
    - The `UserManager` focuses solely on managing user accounts (creation, updates, etc.).
    - The `NotificationService` handles all notification-related functionality, such as sending welcome emails or account updates.

#### Code Example

```csharp
public class UserManager
{
    public void CreateUser(UserData userData)
    {
        // Logic for creating a user
    }

    public void UpdateUser(int userId, UserData userData)
    {
        // Logic for updating a user
    }
}

public class NotificationService
{
    public void SendWelcomeEmail(string userEmail)
    {
        // Logic for sending welcome email
    }

    public void SendAccountUpdateNotification(string userEmail, string updateType)
    {
        // Logic for sending account update notifications
    }
}
```

By applying SRP, changes in how notifications are sent (e.g., switching from email to SMS) only affect the `NotificationService` without impacting user account management logic.

#### Diagram

[Insert Diagram: User Notifications SRP]

---

## Infrastructure Layer

This layer handles aspects like logging, error management, and persistence, ensuring separation of concerns from core business logic.

### Example 3: Logging

In this example, SRP is applied by separating logging from the actual business logic.

- **Where it is used:** `LoggerService` and `OrderProcessor`.
- **What it does:**
    - `OrderProcessor` manages the logic for processing orders.
    - `LoggerService` takes care of logging important information or errors during the process.

#### Code Example

```csharp
public class OrderProcessor
{
    private readonly ILoggerService _loggerService;

    public OrderProcessor(ILoggerService loggerService)
    {
        _loggerService = loggerService;
    }

    public void ProcessOrder(Order order)
    {
        try
        {
            // Logic for processing an order
            _loggerService.LogInfo($"Order {order.Id} processed successfully");
        }
        catch (Exception e)
        {
            _loggerService.LogError($"Error processing order {order.Id}: {e.Message}");
        }
    }
}

public interface ILoggerService
{
    void LogInfo(string message);
    void LogError(string message);
}

public class LoggerService : ILoggerService
{
    public void LogInfo(string message)
    {
        // Logic for logging info messages
    }

    public void LogError(string message)
    {
        // Logic for logging error messages
    }
}
```

If the logging mechanism changes (e.g., switching from console logging to file logging), only the `LoggerService` is affected, keeping the order processing logic untouched.

#### Diagram

[Insert Diagram: Logging SRP]

---

## Conclusion

By applying the SRP across different layers of the project, we ensure that each class is focused on a single responsibility, improving the overall maintainability, modularity, and testability of the system. Each responsibility is isolated to prevent complex classes that are harder to maintain and debug.


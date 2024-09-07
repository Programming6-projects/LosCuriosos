# Git Flow

Our Git Flow strategy ensures a smooth and organized development process,
facilitating collaboration and efficient feature integration. Here is a detailed
overview of our Git Flow:

## Develop Branch

- The `develop` branch is the primary branch where all developers collaborate.
- All new features, fixes, and improvements are developed in this branch.

## Branch Creation

- Developers create new branches from the `develop` branch following our branch
  naming strategy.
- Each branch must adhere to the naming conventions using the specified prefixes
  and format.

## Branch Strategy

- Format: `prefix/name/description`
  - `prefix`: Indicates the type of work (e.g., feature, fix, chore).
  - `name`: The last name of the developer in charge of the ticket.
  - `description`: Brief description of the branch's purpose.
- Allowed prefixes include: feature, stable, fix, chore, docs, feat, build, ci,
  test, refactor, perf.

## Pull Requests and Merging

1. **Feature Development:**

   - Developers work on their branches and, upon completion, create a pull
     request (PR) to merge their branch into the `develop` branch.

2. **Sprint Completion:**
   - At the end of each sprint, the `develop` branch is merged into the `main`
     branch.
   - This merge signifies the completion of the sprint and the release of a new
     version of the application.

### Release Process

- The `main` branch is updated weekly at the end of each sprint.
- A new release is created from the `main` branch, incorporating all the tested
  and approved features and fixes.

By following this Git Flow, we ensure that our development process is
well-structured and efficient. This strategy helps us maintain a clean and
stable codebase, enabling regular releases and continuous improvement.

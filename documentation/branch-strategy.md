# Branch Strategy

To ensure a consistent and organized workflow, we have established a branch
naming convention and strategy for our project. This strategy leverages GitHub
Actions to enforce branch naming rules and ensure all branches follow a
specified pattern.

## Conventional Branch Name

We use a GitHub Action to enforce branch naming conventions. The action checks
branch names against a predefined regex pattern and validates the branch name
based on the allowed prefixes, ignoring specific branch names, and ensuring
branch name length constraints. Here are the details of our branch naming
convention

### Prefixes and Conventions

Our branch naming strategy follows a specific format and uses prefixes to
indicate the purpose of the branch. The naming convention is structured as
follows:

- **Format**: `prefix/name/description`
  - **prefix**: Indicates the type of work being done.
  - **name**: Provides the last name of the developer in charge of the ticket.
  - **description**: A brief description of the branch's purpose.

### Allowed Prefixes

The following prefixes are allowed and must be used at the beginning of branch
names:

- **feature**: Used for new features or enhancements.
- **stable**: For stable branches ready for release.
- **fix**: For bug fixes.
- **chore**: For routine tasks or maintenance.
- **docs**: For documentation updates.
- **feat**: An alternative for feature branches.
- **build**: For changes that affect the build system or external dependencies.
- **ci**: For continuous integration changes.
- **test**: For testing changes.
- **refactor**: For code refactoring.
- **perf**: For performance improvements.

### Ignored Branches

The following branch names are ignored by the convention check and do not need
to follow the naming rules:

- **main**
- **develop**

### Length Constraints

Branch names must adhere to the following length constraints:

- **Minimum Length**: 4 characters
- **Maximum Length**: 120 characters

By following this branch naming strategy, we ensure clarity and consistency in
our workflow, making it easier to understand the purpose and context of each
branch. This strategy also integrates seamlessly with our Continuous Integration
(CI) model, enhancing our overall development process.

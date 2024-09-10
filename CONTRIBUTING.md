# Contributing to Distribution Center built

Thank you for your interest in contributing to this API! Here are some
guidelines to help you get started.

## How to Report Bugs

If you find a bug, please open an issue on GitHub and include:

- A clear and descriptive title.
- Steps to reproduce the bug.
- Expected and actual behavior.
- Any relevant screenshots or log files.

## Getting Started

### Prerequisites

- [Dotnet SDK](https://dotnet.microsoft.com/download)
- [node.js](https://nodejs.org/en/)
- [pnpm](https://pnpm.io/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Installation

1. Clone the repository to your local machine.

   ```bash
   git clone https://github.com/Programming6-projects/LosCuriosos.git
   cd LosCuriosos
   ```

2. Install the project dependencies.

   ```bash
   pnpm install
   ```

3. Enable Husky hooks.

   ```bash
   pnpm prepare
   ```

With these steps, you have the project set up and ready to start development.

## Commands for Development

### Building all the Modules

Build all the modules with the following command:

```bash
dotnet build
```

### Running the Database

```bash
pnpm start:db
```

### Reset the Database

```bash
pnpm db:restore
```

### Running the API

Run the API do it with the following command:

```bash
pnpm start:dev
```

### Testing

Run the tests suite with the following command:

```bash
dotnet test
```

Add new tests to cover the changes you make.

### Open Coverage Report (Web)

First, you need to run the tests with the previous command and then you can open
the coverage report with the following command:

```bash
pnpm report
```

To see the html report you need to open the `coverage/index.html` file.

## Test Coverage

We aim to maintain a high level of test coverage to ensure the reliability and
stability of our codebase. When adding new features or making changes, please
ensure that you include the necessary tests to cover the changes. We want to
achieve a test coverage of at least 80% for our codebase.

## Docker Compose Services

Docker Compose is used to manage the services required for the project. The main
services are:

### PostgreSQL

PostgreSQL database is used to store the data for the API. The database is
created with the following configuration:

```yaml
db:
  image: postgres:16.0
  container_name: distribution-center-db
  restart: always
  environment:
    POSTGRES_DB: distribution_center
    POSTGRES_USER: ${POSTGRES_USER}
    POSTGRES_PASSWORD: ${POSTGRES_PASSWORD}
    TZ: 'UTC-4'
  ports:
    - 3030:3030
  command: -p 3030
  volumes:
    - postgres_data:/var/lib/postgresql/data
    - ./scripts/init.sql:/docker-entrypoint-initdb.d/1.sql
  healthcheck:
    test:
      ['CMD-SHELL', 'pg_isready -h localhost -p 3030 -d distribution_center']
    interval: 5s
    timeout: 5s
    retries: 5
```

The initialization script is in the `scripts/init.sql` file.

You can connect to the database with the following url:
`postgresql://localhost:3030/distribution_center` and the credentials are in the
`.env` file.

### PgAdmin

PgAdmin is a web-based interface for managing the PostgreSQL database. PGAdmin
is created with the following configuration:

```yaml
admin:
  image: dpage/pgadmin4:7.8
  container_name: distribution-center-admin
  restart: always
  environment:
    PGADMIN_DEFAULT_EMAIL: ${PGADMIN_EMAIL}
    PGADMIN_DEFAULT_PASSWORD: ${PGADMIN_PASSWORD}
    PGADMIN_LISTEN_PORT: 4040
  ports:
    - 4040:4040
  volumes:
    - pgadmin_data:/var/lib/pgadmin
  depends_on:
    db:
      condition: service_healthy
```

You can access the PgAdmin interface at `http://localhost:4040` and the
credentials are in the `.env` file.

## Development Guidelines

- Follow the coding style and conventions outlined in the
  [conventions](documentation/conventions.md).
- Write clear and concise commit messages following the
  [commit convention](documentation/commit-convention.md).
- Make sure your code is well-documented.

## Git Workflow

- Follow the branch naming conventions outlined in the
  [branch strategy](documentation/branch-strategy.md).
- Follow the git flow outlined in the
  [git workflow](documentation/git-workflow.md).

## Pull Request Process

When you are ready to submit your changes, follow these steps:

- You need to commit your changes using the `git commit` command - without a
  message the husky hooks will be in-charge of that.
- You need to create a new branch with a semantic prefix (e.g., `feature/`,
  `fix/`, `docs/`) and a descriptive name.
- Push your changes to the branch and open a pull request on GitHub.
- Wait for the maintainers to review your pull request.

## Pull Request Review Guidelines

All pull requests will be evaluated based on the following criteria:

1. **Semantic Commits:**

   - Ensure that commit messages follow semantic commit conventions as agreed
     upon by the team.

2. **Code Conventions:**

   - Adhere to all code conventions enforced by Dotnet Analyzers.
   - Follow additional coding rules and standards discussed and agreed upon by
     the team.

3. **Test Execution:**

   - Run all tests and ensure they pass successfully.
   - Use `dotnet test` for running tests.
   - The test coverage should be at least 80%.

4. **CI Model and Pipelines:**

   - The pull request must pass all stages of the Continuous Integration (CI)
     model and associated pipelines.
   - This includes using GitHub Actions for branch name enforcement
     (`deepakputhraya/action-branch-name@master`), semantic pull request checks
     (`amannn/action-semantic-pull-request@v5`), and commit message checking
     (`gsactions/commit-message-checker@v2`).

5. **Task or Feature Completion:**
   - The pull request should successfully complete the task or feature it is
     intended to address.

By adhering to these guidelines, we ensure that our codebase remains clean,
maintainable, and aligned with our project goals and standards.

## More Information

- If you have any questions about the UML diagrams, please visit the next link:
  [UML](https://app.diagrams.net/#G1awGmFTObv4UfOzXyf1iRkqM6QIfm03qK#%7B%22pageId%22%3A%22xvrsX0oJFwpDuwu-0w1k%22%7D)

- If you have any questions about the ER diagrams, please visit the next link:
  [ER](https://app.diagrams.net/#G1w7jpeUgPEAQOYKz1nO29NhScbfLFuASE#%7B%22pageId%22%3A%22dGa4_oXlvo0_BvP2rMHZ%22%7D)

- If you need more information about the tasks of the project, please visit the
  next link: [ClickUp](https://app.clickup.com/9011230438/v/o/s/90111008515)

## License

By contributing to this API, you agree that your contributions will be licensed
under its [MIT License](LICENSE).

Thank you for your interest in contributing to this API! We look forward to your
contributions.

```

```

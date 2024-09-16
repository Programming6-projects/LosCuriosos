# Mapbox API Integration, Email Configuration, and Database Setup

## Using Mapbox API

The Mapbox API is essential for our order management system as it allows us to
calculate the distance between two points on a route. This is crucial for
optimizing delivery routes and providing accurate information on arrival times
and total distance. Without the Mapbox API, we would need to develop our own
distance calculation algorithm, which would be complex and potentially less
accurate and efficient. You need to set the following environment variable:

- `MAPBOX_TOKEN` - Your Mapbox API token.

## Database Configuration

We use PostgreSQL for our database. You need to set the following environment
variables:

- `PGUSER` - The username for your PostgreSQL database.
- `PGPASSWORD` - The password for your PostgreSQL database.

## pgAdmin Configuration

pgAdmin is a web-based interface for managing PostgreSQL databases. It requires
the following environment variables:

- `PGADMIN_DEFAULT_EMAIL` - The default email to log into pgAdmin.
- `PGADMIN_DEFAULT_PASSWORD` - The default password to log into pgAdmin.

## Email Credentials Configuration

To send emails from our application, we need to configure the credentials of a
Gmail account that will act as our sender. We use application-specific passwords
generated in the Gmail account to ensure that the email can be used without
compromising the security of our primary account.

- `EMAIL_USERNAME` - Email username (e.g., `loscuriosos63@gmail.com`).
- `EMAIL_PASSWORD` - Application password generated for the email account.

**Note:** Replace `your_app_password` with the actual application password
generated for the Gmail account. Do not share these credentials as they are
sensitive information.

## Environment Variable Configuration

Instead of setting environment variables in your shell's configuration file, we
recommend creating a `.env` file in the root of your project. This file should
not be committed to your version control system. It's already included in the
`.gitignore` file.

Create a `.env` file and add the following variables:

```bash
# Postgres variables
export POSTGRES_USER="Your postgres user"
export POSTGRES_PASSWORD="Your postgres password"
# PgAdmin variables
export PGADMIN_EMAIL="Your pgAdmin email"
export PGADMIN_PASSWORD="Your pgAdmin password"
# Mapbox variables
export MAPBOX_TOKEN="Your mapbox token"
# Gmail variables
export GMAIL_EMAIL="Your gmail email"
export GMAIL_APP_PASSWORD="Your gmail app password"
```

Replace the placeholders with your actual values.

**Note:** Be sure not to share these tokens and credentials as they are
sensitive information.

# Mapbox API Integration and Email Configuration

## Using Mapbox API

The Mapbox API is essential for our order management system as it allows us to calculate the distance between two points on a route. This is crucial for optimizing delivery routes and providing accurate information on arrival times and total distance. Without the Mapbox API, we would need to develop our own distance calculation algorithm, which would be complex and potentially less accurate and efficient.

## Environment Variable Configuration for API Tokens

To use the Mapbox API, you need to configure environment variables in your development environment to securely store your API token. Follow these steps in a Linux environment:

1. **Open your terminal**.
2. **Set environment variables** using the `export` command. Replace `your_token` with your actual Mapbox token.

    ```bash
    export MAPBOX_TOKEN=your_token
    ```

3. **Make these environment variables persistent** by adding the export commands to your shell's configuration file (e.g., `~/.bashrc` or `~/.bash_profile` for bash, `~/.zshrc` for zsh).

    ```bash
    echo 'export MAPBOX_TOKEN=your_token' >> ~/.bashrc
    ```

4. **Apply the changes** to the current session by running the `source` command.

    ```bash
    source ~/.bashrc
    ```

5. **Verify that the environment variable is set correctly**.

    ```bash
    echo $MAPBOX_TOKEN
    ```

**Note:** Replace `your_token` with your actual Mapbox token. Be sure not to share these tokens as they are sensitive information.

## Email Credentials Configuration

To send emails from our application, we need to configure the credentials of a Gmail account that will act as our sender. We use application-specific passwords generated in the Gmail account to ensure that the email can be used without compromising the security of our primary account.

1. **Environment Variables for Email**:
    - `EMAIL_USERNAME` - Email username (e.g., `loscuriosos63@gmail.com`).
    - `EMAIL_PASSWORD` - Application password generated for the email account.

2. **Configuration in the Terminal**:

    ```bash
    export EMAIL_USERNAME=loscuriosos63@gmail.com
    export EMAIL_PASSWORD=your_app_password
    ```

3. **Persisting Variables**:

   Add the commands to your shell's configuration file so that these variables persist between sessions:

    ```bash
    echo 'export EMAIL_USERNAME=loscuriosos63@gmail.com' >> ~/.bashrc
    echo 'export EMAIL_PASSWORD=your_app_password' >> ~/.bashrc
    ```

4. **Apply the Changes**:

    ```bash
    source ~/.bashrc
    ```

5. **Verify Variables**:

    ```bash
    echo $EMAIL_USERNAME
    echo $EMAIL_PASSWORD
    ```

**Note:** Replace `your_app_password` with the actual application password generated for the Gmail account. Do not share these credentials as they are sensitive information.

---

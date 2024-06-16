# Account Database System

This repository contains a simple account database system implemented in C#. It allows for registering accounts, saving them to a JSON file, loading them back, and retrieving accounts by credentials. It also supports generating XML representation of accounts.

## Features

- **Registration:** Register new accounts with unique usernames and hashed passwords.
- **Persistence:** Save accounts to a JSON file (`users.json`) and load them back.
- **Credential Verification:** Verify account credentials (username and password).
- **XML Export:** Generate XML representation of registered accounts.

## Usage

To use the `AccountsStorage` class:

1. Create an instance of `AccountsStorage` with a storage file name.
2. Register new accounts using the `Register` method.
3. Save registered accounts to file using `SaveToFile`.
4. Load accounts from file using `LoadFromFile`.
5. Retrieve account details by credentials using `GetAccountByCredentials`.
6. Generate XML representation of accounts using `GetAsXml`.

Example usage is provided in `Program.cs`.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

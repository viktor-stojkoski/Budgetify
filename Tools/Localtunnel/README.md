# Localtunnel

Localtunnel allows you to easily share a web service on your local development machine without messing with DNS and firewall settings.
Read more about localtunnel [here](https://theboroer.github.io/localtunnel-www/).

# Localtunnel in Budgetify

Localtunnel in Budgetify is used for the Azure ADB2C API Connector.
This serves the Functions project in a publicly accessible website.
Then the B2C Client uses this URL to:

- Create Users in Budgetify database for the `CreateUserFunction`.
- Add custom claims to token for the `UpdateUserClaimsFunction`.

# Bookings

This web application is designed as a simple resource booking calendar system, designed to be used for meeting rooms, company vehicles, laptop carts, or anything else you might book in a calendar.

It does not support some advanced calendar features such as recurring events, or multi-day events. This is by design, as this system is intended to be extremely simple and straightforward for users.

## Running

This web application is designed to run in a container.

This web application requires a number of configuration variables to exist for it to work. These must be passed in in such a way that dotnet's [Configuration](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration) system can read them, such as an `appconfig.json` file, or via environment variables in a container.

Please keep in mind that the use of environment variables outside of a container is not safe.

This web application is designed to run with OAuth / OpenID Connect as an authentication mechanism. It will not run without the corresponding settings, and does not support other authentication mechanisms. It was written specifically to be used with Azure Active Directory, but should work with any OAuth / OpenID Connect provider, such as Google Workspaces or Okta.

| Configuration Variable | Description | Example |
|------------------------|-------------|---------|
| `ConnectionStrings:Internal` | Connection string to a MongoDB server and database. | `mongodb://username:password@mongoservername/databasename` |
| `OIDC:AdminGroupID` | The objectID of a group that site administrators are a member of. These users have access to the configuration page, and can see and edit all resource calendars. The app will search the user's `groups` claims for this value. IT does not have to be guid, but does have to exist in a users' `groups` claims. | `00000000-0000-0000-0000-000000000000` |
| `OIDC:Authority` | The issuing authority for the OpenID Connect connection. | `https://login.microsoftonline.com/00000000-0000-0000-0000-000000000000` |
| `OIDC:ClientID` | The ClientID for the OpenID Connect connection. If using AzureAD, this is the __Application ID__ of your _Enterprise Application_ you've set up for the OIDC connection. | `00000000-0000-0000-0000-000000000000` |
| `OIDC:ClientSecret` | A valid client secret from your OIDC server. | _A random string_ |
| `Settings:TimeZone` | The name of the time zone to display dates and times in. Must be appropriate for the operating system hosting the application.| `America/Regina` or `Canada Central Standard Time` |


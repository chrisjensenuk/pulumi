# Pulumi and APIM

Using Pulumi to automate the creation of API Manager (APIM) and create an API from a known swagger.  API is secured by OAuth 2.0 with v2 endpoints.

## Setup
- Install Pulumi https://www.pulumi.com/docs/get-started/azure/


In Azure AD create 2 App Registrations (one to represent the back-end API and the other for the front-end).
As per this: https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-protect-backend-with-aad

### Server AAD App Registration
- Leave Redirect URI empty
- Expose an API > Set Application ID URI
- Expose an API > Add a scope

In order for Applications to authenticate with the backend API and not via delegated permissions then update the **manifest** to
```
"appRoles": [
		{
			"allowedMemberTypes": [
				"Application"
			],
			"description": "Apps that have this role have the ability to invoke my API",
			"displayName": "Can invoke my API",
			"id": "{{guid}}",
			"isEnabled": true,
			"value": "myTestRole"
		}
	],
```
> The id guid needs to be a unique guid  
> The value is the name of the role. It cannot contain spaces
(resources: https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-daemon-overview)

### Client AAD App Registration
- Supported account types : Accounts in any organizational directory (Any Azure AD directory - Multitenant)
- Redirect URI : select Web and leave the URL field empty
- Certificates & secrets > New client secret
- API permissions > Add a Permission > My APIs > select backend app scope

## Deploying environment
- Populate `/pulumi/Pulumi.dev.yaml` with AAD App Registration information
- Navigate terminal to `/pulumi`
- `az login` login to subscription (pulumi will use this login)
 and run `pulumi up`
- Deploy the environment - APIM takes a while to set up
- From VS publish the .NET web api (`/SampleWebApi`) to the created App Service

Use the client app `/OauthClient` to test app authorization.  
Use Postman to test Authorization header





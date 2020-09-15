using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;
using Pulumi.Azure.ApiManagement;
using Pulumi.Azure.ApiManagement.Inputs;

class ApimStack : Stack
{
    public ApimStack()
    {
        var config = new Pulumi.Config();

        var tenantId = config.Require("tenantId");
        var authorizationEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/authorize";
        var tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
        var defaultScope = config.Require("scope");
        var clientId = config.Require("clientId");
        var clientSecret = config.Require("clientSecret");

        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("rg-chris-apim");

        //Service Plan
        var appServicePlan = new Plan("plan-api", new PlanArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Kind = "App",
            Sku = new PlanSkuArgs
            {
                Tier = "Basic",
                Size = "B1"
            }
        });

        var app = new AppService("app-api", new AppServiceArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AppServicePlanId = appServicePlan.Id
        });

        var apim = new Service("apim-api", new ServiceArgs{
            ResourceGroupName = resourceGroup.Name,
            PublisherName = "chrisjensenuk",
            PublisherEmail = "chrisjensenuk@gmail.com",
            SkuName = "Developer_1"
        });

        var apimAuth = new AuthorizationServer("apim-oauth", new AuthorizationServerArgs
        {
            ApiManagementName = apim.Name,
            ResourceGroupName = resourceGroup.Name,
            DisplayName = "apim-oauth",
            ClientRegistrationEndpoint = "http://localhost",
            GrantTypes = { "authorizationCode" },
            AuthorizationEndpoint = authorizationEndpoint,
            AuthorizationMethods = {"GET", "POST"},
            TokenEndpoint = tokenEndpoint,
            ClientAuthenticationMethods = "Body",
            BearerTokenSendingMethods = "authorizationHeader",
            DefaultScope = defaultScope,
            ClientId = clientId,
            ClientSecret = clientSecret
        });

        var api = new Api("example-api", new ApiArgs
        {
            ResourceGroupName = resourceGroup.Name,
            ApiManagementName = apim.Name,
            Revision = "1",
            DisplayName = "Example API",
            Path = "example",
            Protocols = 
            {
                "https"
            },
            Oauth2Authorization = new ApiOauth2AuthorizationArgs
            {
                 AuthorizationServerName = apimAuth.Name
            },
            Import = new ApiImportArgs
            {
                ContentFormat = "openapi+json",
                ContentValue = Constants.SwaggerJson
            },
            ServiceUrl = app.Name.Apply(n => $"http://{n}.azurewebsites.net")
        });

        var policy = new ApiPolicy("example-api-policy", new ApiPolicyArgs
        {
            ResourceGroupName = resourceGroup.Name,
            ApiManagementName = apim.Name,
            ApiName = api.Name,
            XmlContent = Constants.ApiPolicyXml
        });
    }
}
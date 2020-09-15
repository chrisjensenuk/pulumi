public static class Constants
{
    public const string ApiPolicyXml = @"<policies>
    <inbound>
        <base />
        <validate-jwt header-name='Authorization' failed-validation-httpcode='401' failed-validation-error-message='Unauthorized. Access token is missing or invalid.'>
            <openid-config url='https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration' />
            <required-claims>
                <claim name='aud'>
                    <value>e71ebe2d-cf06-436b-9573-6d3f86ea3b91</value>
                </claim>
            </required-claims>
        </validate-jwt>
    </inbound>
    <backend>
        <base />
    </backend>
    <outbound>
        <base />
    </outbound>
    <on-error>
        <base />
    </on-error>
</policies>";

    public const string SwaggerJson = @"{
  ""openapi"": ""3.0.1"",
  ""info"": {
    ""title"": ""SampleWebApi"",
    ""version"": ""1.0""
  },
  ""paths"": {
    ""/"": {
      ""get"": {
        ""tags"": [
          ""WeatherForecast""
        ],
        ""responses"": {
          ""200"": {
            ""description"": ""Success"",
            ""content"": {
              ""text/plain"": {
                ""schema"": {
                  ""type"": ""array"",
                  ""items"": {
                    ""$ref"": ""#/components/schemas/WeatherForecast""
                  }
                }
              },
              ""application/json"": {
                ""schema"": {
                  ""type"": ""array"",
                  ""items"": {
                    ""$ref"": ""#/components/schemas/WeatherForecast""
                  }
                }
              },
              ""text/json"": {
                ""schema"": {
                  ""type"": ""array"",
                  ""items"": {
                    ""$ref"": ""#/components/schemas/WeatherForecast""
                  }
                }
              }
            }
          }
        }
      }
    }
  },
  ""components"": {
    ""schemas"": {
      ""WeatherForecast"": {
        ""type"": ""object"",
        ""properties"": {
          ""date"": {
            ""type"": ""string"",
            ""format"": ""date-time""
          },
          ""temperatureC"": {
            ""type"": ""integer"",
            ""format"": ""int32""
          },
          ""temperatureF"": {
            ""type"": ""integer"",
            ""format"": ""int32"",
            ""readOnly"": true
          },
          ""summary"": {
            ""type"": ""string"",
            ""nullable"": true
          }
        },
        ""additionalProperties"": false
      }
    }
  }
}";
}
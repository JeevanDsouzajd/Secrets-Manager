using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace SecretManager.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet("GetSecret")]
        public async Task<string> GetSecret()
        {
            try
            {
                string secretKey = Environment.GetEnvironmentVariable("AWS_SECRETACESSKEY");
                string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESSKEY");
                string region = Environment.GetEnvironmentVariable("AWS_REGION");
                var credentials = new BasicAWSCredentials(accessKey, secretKey);
                IAmazonSecretsManager secretManagerClient = new AmazonSecretsManagerClient(credentials, RegionEndpoint.GetBySystemName(region));
                string secretName = Environment.GetEnvironmentVariable("Secret_Name");

                GetSecretValueRequest request = new GetSecretValueRequest
                {
                    SecretId = secretName,
                };

                GetSecretValueResponse response = await secretManagerClient.GetSecretValueAsync(request);
                string configJson = response.SecretString;
                return configJson;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

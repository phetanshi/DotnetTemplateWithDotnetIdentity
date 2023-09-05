using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace DotnetTemplateWithDotnetIdentity.Api.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly IIdentityServerInteractionService identity;
        private readonly ILogger<OidcConfigurationController> _logger;


        public OidcConfigurationController(
            IClientRequestParametersProvider clientRequestParametersProvider,
            IIdentityServerInteractionService identity,
            ILogger<OidcConfigurationController> logger)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
            this.identity = identity;
            _logger = logger;
        }

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}

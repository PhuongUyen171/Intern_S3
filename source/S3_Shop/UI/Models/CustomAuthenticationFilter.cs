using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
//using System.Web.Mvc;
//using System.Web.Mvc.Filters;

namespace UI.Models
{
    public class CustomAuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string auth = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            string[] TokenAndUser = null;
            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Authorization Header",request);
                return;
            }
            if(authorization.Scheme!="Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Authorization Schema",request);
                return;
            }
            TokenAndUser = authorization.Parameter.Split(':');
            string token = TokenAndUser[0];
            string user = TokenAndUser[1];
            if (string.IsNullOrEmpty(token))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Token", request);
                return;
            }
            string validUsername = TokenManager.ValidateToken(token);
            if (validUsername != user)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token for user",request);
                return;
            }
            context.Principal = TokenManager.GetPrincipal(authorization.Parameter);
        }

        public async Task AuthenticationFailureResult(HttpAuthenticationContext context)
        {

        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=localhost"));
            context.Result = new ResponseMessageResult(result);
        }
    }
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase;
        public HttpRequestMessage Request { get; set; }
        public AuthenticationFailureResult(string reasonPhrase,HttpRequestMessage request)
        {
            this.ReasonPhrase = reasonPhrase;
            this.Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
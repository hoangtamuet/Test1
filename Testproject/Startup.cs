using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Testproject;
using Testproject.Provider;

[assembly: OwinStartup(typeof(AngularJSAuthentication.API.Startup))]
namespace AngularJSAuthentication.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);

			ConfigureOAuth(app);
		}
		public void ConfigureOAuth(IAppBuilder app)
		{
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

		}

		//public void ConfigureOAuth(IAppBuilder app)
		//{
		//	OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
		//	OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
		//	{
		//		//For Dev enviroment only (on production should be AllowInsecureHttp = false)
		//		AllowInsecureHttp = true,
		//		//TokenEndpointPath = new PathString("/oauth2/token"),
		//		TokenEndpointPath = new PathString("/token"),
		//		AccessTokenExpireTimeSpan = TimeSpan.FromHours(5),
		//		Provider = new CustomOAuthProvider(),
		//		RefreshTokenProvider = new SimpleRefreshTokenProvider(),
		//		AccessTokenFormat = new CustomJwtFormat(URL_AS_API)
		//	};

		//	// OAuth 2.0 Bearer Access Token Generation
		//	app.UseOAuthAuthorizationServer(OAuthServerOptions);
		//	app.UseOAuthBearerAuthentication(OAuthBearerOptions);

		//	//use a cookie to temporarily store information about a user logging in with a third party login provider            
		//	app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
		//}
		////AS dong thoi la RS thi cai dat them: Install-Package Microsoft.Owin.Security.Jwt
		//public void ConfigureOAuthRS(IAppBuilder app)
		//{
		//	var issuer = URL_AS_API;//dia chi cua AS
		//	var secret = TextEncodings.Base64Url.Decode("1234567890123456789012345678901234567890123");//sceret key

		//	// Api controllers with an [Authorize] attribute will be validated with JWT
		//	app.UseJwtBearerAuthentication(
		//		new JwtBearerAuthenticationOptions
		//		{
		//			AuthenticationMode = AuthenticationMode.Active,
		//			AllowedAudiences = new[] { ClientId },
		//			IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
		//			{
		//				new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
		//			},
		//			Provider = new OAuthBearerAuthenticationProvider
		//			{
		//				OnValidateIdentity = context =>
		//				{
		//					context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim("newCustomClaim", "newValue"));
		//					return Task.FromResult<object>(null);
		//				}
		//			}
		//		});
		//}
	}
}
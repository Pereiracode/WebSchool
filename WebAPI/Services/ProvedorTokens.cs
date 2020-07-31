using DAO.Dao;
using DAO.Utils;
using Microsoft.Owin.Security.OAuth;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI.Services
{
    public class ProvedorTokens : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UsuarioDAO daoUsuario = new UsuarioDAO();
            Usuario usuario = daoUsuario.BuscarPorLogin(context.UserName);
            string criptada = CriptoHash.GerarHash(context.Password);
            if (usuario != null)
            {
                if (usuario.Senha.Equals(criptada))
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));
                    context.Validated(identity);

                }
                else
                {
                    context.SetError("Não Autorizado", "Usuario/Senha incorretos");
                }
            }
            else
            {
                context.SetError("Não Autorizado", "Usuario/Senha incorretos");
            }

        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Primeiro_Projeto.Models;
using Primeiro_Projeto.Repositories;
using Primeiro_Projeto.Services;

namespace Primeiro_Projeto.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]

        public LoginResult Login(LoginRequest request)
        {
            var result = new LoginResult();

            if (request == null)
            {
                result.Sucesso = false;
                result.Mensagem = "Parâmetro veio nulo";

            }
            else if (request.Email == "")
            {
                result.Sucesso = false;
                result.Mensagem = "E-mail obrigatório";
            }

            else if (request.Senha == "")
            {
                result.Sucesso = false;
                result.Mensagem = "Senha obrigatória";
            }

            else
            {
                var connectionString = _configuration.GetConnectionString("primeiroprojetodb");

                var usuarioService = new UsuarioService(connectionString);

                result = usuarioService.Login(request.Email, request.Senha);
            }

            return result;
        }



        [Route("cadastro")]
        [HttpPost]

        public CadastroResult Cadastro(CadastroRequest request)
        {
            var result = new CadastroResult();           

            if (request == null ||
                string.IsNullOrEmpty(request.Nome) ||
                string.IsNullOrEmpty(request.Sobrenome) ||
                string.IsNullOrEmpty(request.Telefone) ||
                string.IsNullOrEmpty(request.Genero) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Senha))

            {
                result.Sucesso = false;
                result.Mensagem = "Todos os campos obrigatórios";

                return (result);
            }

            

            else
            {
                var connectionString = _configuration.GetConnectionString("primeiroprojetodb");
                var usuarioService = new UsuarioService(connectionString);

                result = usuarioService.Cadastro(request.Nome, request.Sobrenome, request.Telefone, request.Genero, request.Email, request.Senha);

                

            }

            return result;

        }

        [Route("esqueceuSenha")]
        [HttpPost]

        public EsqueceuSenhaResult EsqueceuSenha(EsqueceuSenhaRequest request) 
        {
            var result = new EsqueceuSenhaResult();

            if (request == null ||
                string.IsNullOrEmpty(request.Email))
            {
                result.Sucesso = false;
                result.Mensagem = "Email obrigátorio";

                return result;
            }

           
                var connectionString = _configuration.GetConnectionString("primeiroprojetodb");

                var mensagem = new UsuarioService(connectionString).EsqueceuSenha(request.Email);

            if (!string.IsNullOrEmpty(mensagem)){

                result.Sucesso = false;
                result.Mensagem = "Erro ao enviar email";
            }

            else
            {
                result.Sucesso = true;  
            }
            

            return result;

        }

        [HttpGet]
        [Route("obterUsuario")]
        

        public ObterUsuarioResult ObterUsuarioGuid(Guid usuarioGuid)
        {

            var result = new ObterUsuarioResult();

            if (usuarioGuid == null)
            {
                result.Mensagem = "Guid Vazio";
               
            }

            else
            {
                var connectionString = _configuration.GetConnectionString("primeiroprojetodb");

                var usuario = new UsuarioService(connectionString).ObterUsuarioGuid(usuarioGuid);

               if (usuario == null)
                {
                    result.Mensagem = "Usuario não existe";
                }

               else
                {
                    result.Sucesso = true;
                    result.nome = usuario.Nome;
                }


            }

            return result;

        }





    }



}

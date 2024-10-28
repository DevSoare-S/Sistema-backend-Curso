using Primeiro_Projeto.Common;
using Primeiro_Projeto.Entities;
using Primeiro_Projeto.Models;
using Primeiro_Projeto.Repositories;

namespace Primeiro_Projeto.Services
{
    public class UsuarioService
    {

        private string _connectionString;
        public UsuarioService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LoginResult Login(string email, string senha)
        {
            var result = new LoginResult();

            var usuarioRepository = new UsuarioRepository(_connectionString);

            var usuario = usuarioRepository.ObterUsuario(email);

            if (usuario != null)
            {
                if (usuario.Senha == senha)
                {
                    result.Sucesso = true;
                    result.UsuarioGuid = usuario.UsuarioGuid;
                }

                else
                {
                    result.Sucesso = false;
                    result.Mensagem = "Usuario ou senha inválidos";
                }
            }

            else
            {
                result.Sucesso = false;
                result.Mensagem = "Usuário ou senha inválidos";
            }

            return result;
        }

        public CadastroResult Cadastro(string nome, string sobrenome, string telefone, string genero, string email, string senha)
        {
            var result = new CadastroResult();

            var usuarioRepository = new UsuarioRepository(_connectionString);

            var usuario = usuarioRepository.ObterUsuario(email);

            if (usuario != null)
            {
                result.Sucesso = false;
                result.Mensagem = "Usuário já existe no sistema";

            }

            else
            {
                usuario = new Usuario();

                usuario.Nome = nome;
                usuario.Sobrenome = sobrenome;
                usuario.Telefone = telefone;
                usuario.Genero = genero;
                usuario.Email = email;
                usuario.Senha = senha;
                usuario.UsuarioGuid = Guid.NewGuid();

                var insertResult = usuarioRepository.Inserir(usuario);

                if (insertResult > 0)
                {
                    result.Sucesso = true;
                    result.Mensagem = "Usuario Cadastrado com sucesso";
                    result.UsuarioGuid = usuario.UsuarioGuid;                   

                }

                else
                {
                    result.Sucesso = false;
                    result.Mensagem = "Erro ao inserir usuário. Tente novamente";
                }


                usuarioRepository.Inserir(usuario);

            }

            return result;
        }

        public string EsqueceuSenha(string email)
        {
            
            var mensagem = string.Empty;

            var usuario = new UsuarioRepository(_connectionString).ObterUsuario(email);

            if (usuario == null)
            {
                mensagem = "Usuário não existe";
            }

            else
            {

                var assunto = "Recuperação de senha";
                
                var corpo = "Sua senha é" + usuario.Senha;
                
                var emailSender = new EmailSender();

                emailSender.Enviar(assunto, corpo, usuario.Email);
            }

            return mensagem;
        }

        public Usuario ObterUsuarioGuid(Guid usuarioGuid)
        {
            var usuario = new UsuarioRepository(_connectionString).ObterPorGuid(usuarioGuid);

            return usuario;
        }
    }
}

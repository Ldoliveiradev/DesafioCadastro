using AutoMapper;
using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Cadastro.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cadastro.Mvc.Controllers
{
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService,
            IMapper mapper, INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodos()));
        }

        [Route("novo-usuario")]
        public IActionResult Criar()
        {
            return View();
        }

        [Route("novo-usuario")]
        [HttpPost]
        public async Task<IActionResult> Criar(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid) return View(usuarioViewModel);

            await _usuarioService.Adicionar(_mapper.Map<Usuario>(usuarioViewModel));

            if (!OperacaoValida()) return View(usuarioViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-usuario/{id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var usuarioViewModel = await ObterUsuario(id);

            if (usuarioViewModel == null)
            {
                return NotFound();
            }

            return View(usuarioViewModel);
        }

        [Route("editar-usuario/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, UsuarioViewModel usuarioViewModel)
        {
            if (id != usuarioViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(usuarioViewModel);

            var usuarioAtualizacao = await ObterUsuario(id);
            usuarioAtualizacao.Nome = usuarioViewModel.Nome;
            usuarioAtualizacao.Email = usuarioViewModel.Email;
            usuarioAtualizacao.Telefone = usuarioViewModel.Telefone;
            usuarioAtualizacao.Senha = usuarioViewModel.Senha;

            await _usuarioService.Atualizar(_mapper.Map<Usuario>(usuarioAtualizacao));

            if (!OperacaoValida()) return View(usuarioViewModel);

            return RedirectToAction("Index");
        }

        [Route("excluir-usuario/{id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var usuario = await ObterUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [Route("excluir-usuario/{id:guid}")]
        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ConfirmarExclusao(Guid id)
        {
            var usuario = await ObterUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioService.Remover(id);

            if (!OperacaoValida()) return View(usuario);

            TempData["Sucesso"] = "Usuário excluido com sucesso!";

            return RedirectToAction("Index");
        }

        private async Task<UsuarioViewModel> ObterUsuario(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            usuario.DescriptografarSenha();

            return _mapper.Map<UsuarioViewModel>(usuario);

        }
    }
}

using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myfinance_web_netcore.Models;
using myfinance_web_netcore.Services;

namespace myfinance_web_netcore.Controllers;

[Route("[controller]")]
public class TransacaoController : Controller  //! como copiamos a classe PlanoConta, devemos alterar todas as referencias dela para a classe nova ! 
{
    private readonly ILogger<TransacaoController> _logger;
    private readonly IMapper _mapper;
    private readonly ITransacaoService _transacaoService;

    private readonly IPlanoContaService _planoContaService;
    
    public TransacaoController(ILogger<TransacaoController> logger, IMapper mapper, ITransacaoService transacaoService, IPlanoContaService planoContaService)
    {
        _logger = logger;
        _mapper = mapper;
        _transacaoService = transacaoService;
        _planoContaService =  planoContaService;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index() //! devemos colocar o mesmo nome do arquivo dentro da pasta View
    {
        var listaTransacao = _transacaoService.ListarTransacoes(); //* quando usamos o mapeamento, fica dessa forma a passagem dos dados
        ViewBag.ListaTransacao = listaTransacao;

        return View();
    }

    [HttpGet]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]

    public IActionResult Cadastro(int? id) //@ nesse caso, passando ou não o ID a operação é realizada
    {

        var transacaoModel = new TransacaoModel();

        if (id != null) //@ se eu receber um valor de Id diferente de null o cód busca as info para serem tratadas
        {
            transacaoModel = _transacaoService.RetornarRegistro((int)id); //$ (int)id) é uma conversão para garantir que chegue numeros 

        }

        var listaPlanoConta = _planoContaService.ListarPlanoContas(); //* já está passando uma lista de plano conta model
        var planoContaSelectItens = new SelectList(listaPlanoConta, "Id" , "Descricao");
        transacaoModel.ListaPlanoConta = listaPlanoConta;
        transacaoModel.PlanoContas = planoContaSelectItens;

        return View(transacaoModel);
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]

    public IActionResult Cadastro(TransacaoModel model)
    {
        _transacaoService.Salvar(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Excluir/{id}")]

    public IActionResult Excluir(int id)
    {
        _transacaoService.Excluir(id);
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

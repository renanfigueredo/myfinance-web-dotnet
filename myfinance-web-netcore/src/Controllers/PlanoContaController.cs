using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using myfinance_web_netcore.Models;
using myfinance_web_netcore.Services;

namespace myfinance_web_netcore.Controllers;

[Route("[controller]")]
public class PlanoContaController : Controller
{
    private readonly ILogger<PlanoContaController> _logger;

    private readonly IMapper _mapper;

    private readonly IPlanoContaService _planoContaService;

    public PlanoContaController(ILogger<PlanoContaController> logger, IMapper mapper, IPlanoContaService planoContaService)
    {
        _logger = logger;
        _mapper = mapper;
        _planoContaService = planoContaService;
    }

    [HttpGet]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(int? id)
    {
        if (id != null)
        {
            var registro = _planoContaService.RetornarRegistro((int)id); //$ (int)id) é uma conversão para garantir que chegue numeros 
            return View(registro);
        }

        return View();
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(PlanoContaModel model)
    {
        _planoContaService.Salvar(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Excluir/{id}")]
    public IActionResult Excluir(int id)
    {
        _planoContaService.Excluir(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
        var listaPlanoConta = _planoContaService.ListarPlanoContas();
        ViewBag.ListarPlanoConta = listaPlanoConta;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

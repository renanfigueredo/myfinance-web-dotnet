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

    public IActionResult Cadastro(int? id) //@ nesse caso, passando ou não o ID a operação é realizada
    {
        if (id != null) //@ se eu receber um valor de Id diferente de null o cód busca as info para serem tratadas
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
    public IActionResult Index() //! devemos colocar o mesmo nome do arquivo dentro da pasta View
    {
        //! NO COD REAL, TIRAR ESSSA PARTE ! { .....
        //* Quando não usamos AutoMapper temos que fazer tudo isso aqui: 
        //    List<PlanoContaModel> listaPlanoContaModel = new();

        //     foreach ( var item in _planoContaService.ListarPlanoContas()){

        //         var planoConta = new PlanoContaModel(){  //@ cria a tabela com os dados pegos no DB, essa parte é chamada de mapeamento, no caso, esse mapeamento foi feito ná mão ( o que não é muito recomendado ...)
        //             Id = item.Id,
        //             Descricao = item.Descricao,
        //             Tipo = item.Tipo
        //         };
        //         listaPlanoContaModel.Add(planoConta);
        //     }

        // ViewBag.ListarPlanoConta =  listaPlanoContaModel; //@ essa view guarda todos os dados da lista plano contas e apresenta na tela depois, somente ela, não manda os dados para a tela (por isso temos usar o loop acima para completar a tabela)
        //! ......}

        var listaPlanoConta = _planoContaService.ListarPlanoContas(); //* quando usamos o mapeamento, fica dessa forma a passagem dos dados
        ViewBag.ListarPlanoConta = listaPlanoConta;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

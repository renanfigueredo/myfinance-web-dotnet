using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain;
using myfinance_web_netcore.Infrastructure;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Services
{
    public class PlanoContaService : IPlanoContaService
    {
        private readonly MyFinanceDbContext _myFinanceDbContext;
        private readonly IMapper _mapper;
        public PlanoContaService(MyFinanceDbContext myFinanceDbContext, IMapper mapper)
        {   //@ Construtor da classe, pega a info e repassa
            _myFinanceDbContext = myFinanceDbContext;
            _mapper = mapper;
        }

        public void Excluir(int id)
        {
            var item = _myFinanceDbContext.PlanoConta.Where(x => x.Id == id ).First(); //@ vai lá no banco de dados buscar o elemento que tem o Id requerido "Where(x => x.Id == id )" e volta o valor do elemento que aparece primeiro ".First()"
            _myFinanceDbContext.PlanoConta.Attach(item);  //@ Para realizar alterações nos dados
            _myFinanceDbContext.PlanoConta.Remove(item);

            _myFinanceDbContext.SaveChanges(); //@ toda alteração dos dados devem finalizar com o SaveChanges" para garantir que vai ser salvo. !! Por garantia, deixa ele sempre no final da função!!

        }

        public IEnumerable<PlanoContaModel> ListarPlanoContas()
        {
            var listaPlanoConta = _myFinanceDbContext.PlanoConta.ToList(); //@ vai lá no banco de dados buscar a lista, por isso o tipo é uma entidade de dominio 
            var lista = _mapper.Map<IEnumerable<PlanoContaModel>>(listaPlanoConta); //@ Mapeia e retorna para o DOM : "IEnumerable<PlanoContaModel"
            return lista;
        }

        public PlanoContaModel RetornarRegistro(int id)
        {
            var item = _myFinanceDbContext.PlanoConta.Where(x => x.Id == id ).First(); //@ vai lá no banco de dados buscar o elemento que tem o Id requerido "Where(x => x.Id == id )" e volta o valor do elemento que aparece primeiro ".First()"
            var lista = _mapper.Map<PlanoContaModel>(item); //@ Mapeia e retorna para o DOM : "IEnumerable<PlanoContaModel"
            return lista;
        }

        public void Salvar(PlanoContaModel model)
        {
            //* Mapeamento Manual: { ......
            var instancia = new PlanoConta()
            {
                Id = model.Id,
                Descricao = model.Descricao,
                Tipo = model.Tipo
            };
            //*  ...}

            if (instancia.Id == null)
            {
                _myFinanceDbContext.PlanoConta.Add(instancia);//* aqui precisa receber uma entidade de DOM, para isso devemos realizar o mapeamento dela (Comandos de cima). Só passar a var não garante que vai ser salvo por isso rodamos o comando "SaveChanges()" 
            }
            else
            {
                _myFinanceDbContext.PlanoConta.Attach(instancia); //@ Para realizar alterações nos dados
                _myFinanceDbContext.PlanoConta.Entry(instancia).State = EntityState.Modified; //! Toda vez que vamos realizar uma alteração nos dados precisamos "avisar" o cód
            }

            _myFinanceDbContext.SaveChanges(); //@ toda alteração dos dados devem finalizar com o SaveChanges" para garantir que vai ser salvo. !! Por garantia, deixa ele sempre no final da função!!
        }
    }
}
//* _myFinanceDbContext é um objeto do tipo dbSet por isso ele consegue acessar todas as propriedades do db
//* antes de refatorar, a lista era 

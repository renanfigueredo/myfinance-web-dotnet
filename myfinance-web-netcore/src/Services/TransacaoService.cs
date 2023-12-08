using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain;
using myfinance_web_netcore.Infrastructure;
using myfinance_web_netcore.Models;




namespace myfinance_web_netcore.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly MyFinanceDbContext _myFinanceDbContext;

        private readonly IMapper _mapper;
        public TransacaoService(MyFinanceDbContext myFinanceDbContext, IMapper mapper )
        {   //@ Construtor da classe, pega a info e repassa
            _myFinanceDbContext = myFinanceDbContext;
            _mapper = mapper;
        }

        public void Excluir(int id)
        {
            var item = _myFinanceDbContext.Transacao.Where(x => x.Id == id).First(); //@ vai lá no banco de dados buscar o elemento que tem o Id requerido "Where(x => x.Id == id )" e volta o valor do elemento que aparece primeiro ".First()"
            _myFinanceDbContext.Transacao.Attach(item);  //@ Para realizar alterações nos dados
            _myFinanceDbContext.Transacao.Remove(item);
            _myFinanceDbContext.SaveChanges(); //@ toda alteração dos dados devem finalizar com o SaveChanges" para garantir que vai ser salvo. !! Por garantia, deixa ele sempre no final da função!!
        }

        public IEnumerable<TransacaoModel> ListarTransacoes()
        {
            var listaTransacao = _myFinanceDbContext.Transacao.ToList(); //@ vai lá no banco de dados buscar a lista, por isso o tipo é uma entidade de dominio 
            var lista = _mapper.Map<IEnumerable<TransacaoModel>>(listaTransacao); //@ Mapeia e retorna para o DOM : "IEnumerable<TransacaoModel"
            return lista;
        }

        public TransacaoModel RetornarRegistro(int id)
        {
            var item = _myFinanceDbContext.Transacao.Where(x => x.Id == id).First(); //@ vai lá no banco de dados buscar o elemento que tem o Id requerido "Where(x => x.Id == id )" e volta o valor do elemento que aparece primeiro ".First()"
            var lista = _mapper.Map<TransacaoModel>(item); //@ Mapeia e retorna para o DOM : "IEnumerable<TransacaoModel"
            return lista;
        }

        public void Salvar(TransacaoModel model)
        {
            //* Mapeamento Manual: { ......
            var instancia = new Transacao()
            {
                Id = model.Id,
                Historico = model.Historico,
                Data = model.Data,
                Valor = model.Valor,
                PlanoContaId = model.PlanoContaId,
                Tipo = model.Tipo
            };
            //*  ...}

            if (instancia.Id == null)
            {
                _myFinanceDbContext.Transacao.Add(instancia);//* aqui precisa receber uma entidade de DOM, para isso devemos realizar o mapeamento dela (Comandos de cima). Só passar a var não garante que vai ser salvo por isso rodamos o comando "SaveChanges()" 
            }
            else
            {
                _myFinanceDbContext.Transacao.Attach(instancia); //@ Para realizar alterações nos dados
                _myFinanceDbContext.Entry(instancia).State = EntityState.Modified; //! Toda vez que vamos realizar uma alteração nos dados precisamos "avisar" o cód
            }
            _myFinanceDbContext.SaveChanges(); //@ toda alteração dos dados devem finalizar com o SaveChanges" para garantir que vai ser salvo. !! Por garantia, deixa ele sempre no final da função!!
        }
    }
}
//* _myFinanceDbContext é um objeto do tipo dbSet por isso ele consegue acessar todas as propriedades do db
//* antes de refatorar, a lista era 

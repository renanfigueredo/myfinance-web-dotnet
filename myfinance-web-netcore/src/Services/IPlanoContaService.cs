using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Services
{
    public interface IPlanoContaService //! atenção que aqui é interface !
    {
        IEnumerable<PlanoContaModel> ListarPlanoContas();

        void Salvar(PlanoContaModel model);  //@ o salvar vai ser um modelo sem retorno. idependente da ação (se é salvar ou modificar algo) essa função é capaz disso graças ao imput do tipo hidden que tem na view do cadastro, ele passa o Id da operação automaticamente )

        PlanoContaModel RetornarRegistro(int id);

        void Excluir(int id);
    }
}

//$ por boas práticas as interfaces sempre começão com "I". É um 'contrato' que fala o que deve ser implementado quando for usa-la

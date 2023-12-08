namespace myfinance_web_netcore.Domain
{
    public class Transacao
    {
        public int? Id { get; set; }
        public string Historico { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public int PlanoContaId { get; set; } //! Como no DB essa info é uma chave estrangeira, precisamos lincar com a fonte dela tbm ...
        public DateTime Data { get; set; }

        public PlanoConta PlanoConta { get; set; } //! ... Com isso, criamos um objeto para o PlanoConta

    }
}
//* Podemos pegar os mesmos nomes das colunas que estão no DB
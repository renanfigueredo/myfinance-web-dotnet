namespace myfinance_web_netcore.Domain
{
    public class PlanoConta
    {
        public int? Id {get;set;} //@ para sinalizar que uma var pode receber valor "null" colocamos o "?" apos o tipo dela
        public string Descricao {get;set;}
        public string Tipo {get;set;}
    }
}

//* Podemos pegar os mesmos nomes das colunas que estão no DB
namespace BankWebAPI.Domain
{
    namespace BankAPI.Domain
    {
        public class Bank
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Endereco { get; set; }
            public string Telefone { get; set; }

            public Bank() { }

            public Bank(int id, string nome, string endereco, string telefone)
            {
                Id = id;
                Nome = nome;
                Endereco = endereco;
                Telefone = telefone;
            }
        }
    }
}

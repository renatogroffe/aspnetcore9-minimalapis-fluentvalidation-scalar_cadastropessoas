using APIPessoas.Models;

namespace APIPessoas.Data;

public class PessoasRepository
{
    private List<Pessoa> _pessoas { get; init; }
    
    public PessoasRepository() => _pessoas = new ();

    public void Incluir(DadosPessoa pessoa) => _pessoas.Add(
        new ()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = pessoa.Nome,
            Sobrenome = pessoa.Sobrenome,
            Empresa = pessoa.Empresa
        });

    public IEnumerable<Pessoa> ListarTodos() => _pessoas;
}
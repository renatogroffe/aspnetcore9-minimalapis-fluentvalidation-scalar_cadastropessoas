using FluentValidation;
using APIPessoas.Models;

namespace APIPessoas.Validators;

public class DadosPessoaValidator : AbstractValidator<DadosPessoa>
{
    public DadosPessoaValidator()
    {
        RuleFor(c => c.Nome).NotEmpty().WithMessage($"Preencha o campo 'nome'");
        RuleFor(c => c.Sobrenome).NotEmpty().WithMessage($"Preencha o campo 'sobrenome'");
        RuleFor(c => c.Empresa).NotEmpty().WithMessage($"Preencha o campo 'empresa'");
    }                
}
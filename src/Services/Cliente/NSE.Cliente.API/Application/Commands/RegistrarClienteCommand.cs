using FluentValidation;
using NSE.Core.Messages;

namespace NSE.Cliente.API.Application.Commands;

public class RegistrarClienteCommand : Command
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public RegistrarClienteCommand(Guid id, string nome, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Nome = nome;
        Email = email;
        Cpf = cpf;
    }

    public override bool EhValido()
    {
        ValidationResult = new RegistrarClienteValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RegistrarClienteValidation : AbstractValidator<RegistrarClienteCommand>
{
    public RegistrarClienteValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("O Id do cliente deve ser informado");

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome do cliente deve ser informado")
            .Length(2, 150).WithMessage("O nome do cliente deve ter entre 2 e 150 caracteres");

        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("O email do cliente é inválido");

        RuleFor(c => c.Cpf)
            .Must(TerCpfValido).WithMessage("O CPF do cliente é inválido");
    }

    protected static bool TerCpfValido(string cpf)
    {
        return Core.DomainObjects.Cpf.Validar(cpf);
    }

    protected static bool TerEmailValido(string email)
    {
        return Core.DomainObjects.Email.Validar(email);
    }
}

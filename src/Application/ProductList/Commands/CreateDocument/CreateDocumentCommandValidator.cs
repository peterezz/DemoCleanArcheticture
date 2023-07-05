using FluentValidation;

namespace Clean_architecture.Application.ProductList.Commands.CreateDocument;
public sealed class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {
        RuleFor(rule => rule.product.ProductName)
                .NotEmpty()
                .WithMessage("product name not valid");

        RuleFor(rule => rule.product.ProductReference)
        .NotEmpty()
        .WithMessage("product name not valid");

        RuleFor(rule => rule.product.Price)
        .NotEmpty()
        .GreaterThan(50)
        .WithMessage("product name not valid");
    }
}

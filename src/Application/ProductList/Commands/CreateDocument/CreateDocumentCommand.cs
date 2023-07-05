using AutoMapper;
using Clean_architecture.Application.Common.Repository;
using Clean_architecture.Domain.Entities;
using MediatR;

namespace Clean_architecture.Application.ProductList.Commands.CreateDocument;
public class CreateDocumentCommand : IRequest<demoindex>
{
    public demoindex product { get; set; }
}
public class CreateDocumentCommandHandeler : IRequestHandler<CreateDocumentCommand, demoindex>
{
    private readonly IndexRepository<demoindex> _indexRepository;
    private readonly IMapper _mapper;
    private readonly CreateDocumentCommandValidator _validationRules;

    public CreateDocumentCommandHandeler(IndexRepository<demoindex> indexRepository, IMapper mapper)
    {
        _indexRepository = indexRepository;
        _mapper = mapper;
        _validationRules = new CreateDocumentCommandValidator();
    }
    public async Task<demoindex> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validationRules.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new System.ComponentModel.DataAnnotations.ValidationException(validationResult.Errors.Select(error => error.ErrorMessage).First());
        //var product = _mapper.Map<demoindex>(request.product);

        var response = await _indexRepository.IndexDocument(request.product);
        if (!response.IsValid)
            throw new Exception("not valid response");
        return request.product;

    }
}

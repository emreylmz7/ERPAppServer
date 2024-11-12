using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the product by its ID
        var product = await _productRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
        if (product == null)
        {
            return Result<string>.Failure("Product not found.");
        }

        // Map the updated fields to the existing product object
        _mapper.Map(request, product);
        product.UpdatedDate = DateTime.Now;

        // Update the product in the repository
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Product updated successfully.";
    }
}

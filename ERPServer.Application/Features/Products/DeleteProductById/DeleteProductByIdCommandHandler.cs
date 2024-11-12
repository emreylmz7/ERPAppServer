using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.DeleteProductById;

internal sealed class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Result<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductByIdCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        // Get the product by Id
        Product product = await _productRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            return Result<string>.Failure("Product not found");
        }

        // Delete the product
        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Product deleted successfully";
    }
}

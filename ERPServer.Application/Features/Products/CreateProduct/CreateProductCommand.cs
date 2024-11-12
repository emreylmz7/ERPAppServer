using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.CreateProduct;

public sealed record class CreateProductCommand(
    string Name,              // Product name
    string Description,       // Product description
    decimal Price,            // Product price
    decimal Cost,             // Product cost
    int StockQuantity,        // Stock quantity
    Guid CategoryId,          // Category ID to which the product belongs
    Guid SupplierId           // Supplier ID for the product
) : IRequest<Result<string>>;

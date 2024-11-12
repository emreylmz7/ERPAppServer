using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.UpdateProduct;

public sealed record class UpdateProductCommand(
    Guid Id,               // Unique identifier of the product
    string? SKU,           // Optional: SKU of the product
    string? Name,          // Optional: Name of the product
    string? Description,   // Optional: Description of the product
    decimal? Price,        // Optional: Price of the product
    decimal? Cost,         // Optional: Cost of the product
    int? StockQuantity,    // Optional: Stock quantity of the product
    Guid? CategoryId,      // Optional: Category ID
    Guid? SupplierId       // Optional: Supplier ID
) : IRequest<Result<string>>;

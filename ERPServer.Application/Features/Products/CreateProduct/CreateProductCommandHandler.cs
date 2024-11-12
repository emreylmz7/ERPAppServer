using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;
using System.Text.RegularExpressions;

namespace ERPServer.Application.Features.Products.CreateProduct;

internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<string>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetCurrentUserId();
        if (userId == null)
        {
            return Result<string>.Failure("User is not authenticated.");
        }

        // Ürün için benzersiz SKU oluştur
        string uniqueSKU = await GenerateUniqueSKU(request.Name, cancellationToken);

        // İsteği Product nesnesine dönüştür ve benzersiz SKU'yu ata
        Product product = _mapper.Map<Product>(request);
        product.SKU = uniqueSKU;
        product.CreatedBy = userId.Value;

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Product created successfully.";
    }

    private async Task<string> GenerateUniqueSKU(string productName, CancellationToken cancellationToken)
    {
        // Ürün adının ilk üç harfini al ve harf dışı karakterleri çıkar
        string nameCode = Regex.Replace(productName.Substring(0, Math.Min(3, productName.Length)).ToUpper(), @"[^A-Z]", "");

        // Zaman damgası ile benzersiz hale getir
        string timeCode = DateTime.Now.ToString("yyyyMMddHHmmss");
        string sku = $"{nameCode}-{timeCode}";

        // Benzersizliği doğrula
        bool isSkuExist = await _productRepository.AnyAsync(p => p.SKU == sku, cancellationToken);
        if (isSkuExist)
        {
            // Aynı SKU mevcutsa bir saniye ekleyerek yeni bir SKU oluştur
            timeCode = DateTime.Now.AddSeconds(1).ToString("yyyyMMddHHmmss");
            sku = $"{nameCode}-{timeCode}";
        }

        return sku;
    }
}

using AutoMapper;
using ERPServer.Application.Features.Categories.CreateCategory;
using ERPServer.Application.Features.Categories.UpdateCategory;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;
using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.Products.UpdateProduct;
using ERPServer.Application.Features.StockMovements.CreateStockMovement;
using ERPServer.Application.Features.StockMovements.UpdateStockMovement;
using ERPServer.Application.Features.Suppliers.CreateSupplier;
using ERPServer.Application.Features.Suppliers.UpdateSupplier;
using ERPServer.Application.Features.Warehouses.CreateWarehouse;
using ERPServer.Application.Features.Warehouses.UpdateWarehouse;
using ERPServer.Domain.Dtos.ProductDtos;
using ERPServer.Domain.Dtos.StockMovementDtos;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;

namespace ERPServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();

            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<UpdateProductCommand, Product>();
            CreateMap<CreateProductCommand, Product>();

            CreateMap<UpdateStockMovementCommand, StockMovement>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<MovementType>(src.Type)))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => Enum.Parse<StockMovementReason>(src.Reason)));

            // CreateStockMovementCommand to StockMovement mapping
            CreateMap<CreateStockMovementCommand, StockMovement>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<MovementType>(src.Type)))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => Enum.Parse<StockMovementReason>(src.Reason)));

            CreateMap<UpdateSupplierCommand, Supplier>();
            CreateMap<CreateSupplierCommand, Supplier>();

            CreateMap<UpdateWarehouseCommand, Warehouse>();
            CreateMap<CreateWarehouseCommand, Warehouse>();

            CreateMap<Product, ResultProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier!.CompanyName));

            CreateMap<StockMovement, ResultStockMovementDto>()
                .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse!.WarehouseName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name));
                //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))  
                //.ForMember(dest => dest.Reason, opt => opt.MapFrom(src => (int)src.Reason));
        }
    }
}

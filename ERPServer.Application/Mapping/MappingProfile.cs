using AutoMapper;
using ERPServer.Application.Features.Categories.CreateCategory;
using ERPServer.Application.Features.Categories.UpdateCategory;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;
using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.Products.GetAllProduct;
using ERPServer.Application.Features.Products.UpdateProduct;
using ERPServer.Application.Features.StockMovements.CreateStockMovement;
using ERPServer.Application.Features.StockMovements.GetAllStockMovement;
using ERPServer.Application.Features.StockMovements.UpdateStockMovement;
using ERPServer.Application.Features.Suppliers.CreateSupplier;
using ERPServer.Application.Features.Suppliers.UpdateSupplier;
using ERPServer.Application.Features.Warehouses.CreateWarehouse;
using ERPServer.Application.Features.Warehouses.UpdateWarehouse;
using ERPServer.Application.Features.Orders.CreateOrder;
using ERPServer.Application.Features.Orders.UpdateOrder;
using ERPServer.Application.Features.OrderItems.CreateOrderItem;
using ERPServer.Application.Features.OrderItems.UpdateOrderItem;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Application.Features.Order.GetAllOrder;
using ERPServer.Application.Features.OrderItems.GetAllOrderItem;
using ERPServer.Application.Features.Addresses.CreateAddress;
using ERPServer.Application.Features.Addresses.GetAllAddress;
using ERPServer.Application.Features.Addresses.UpdateAddress;
using ERPServer.Application.Features.Invoices.CreateInvoice;
using ERPServer.Application.Features.Invoices.UpdateInvoice;
using ERPServer.Application.Features.Invoices.GetAllInvoices;
using ERPServer.Application.Features.Addresses.GetAddressesByCustomerId;


namespace ERPServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();

            // Category
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<CreateCategoryCommand, Category>();

            // Supplier
            CreateMap<UpdateSupplierCommand, Supplier>();
            CreateMap<CreateSupplierCommand, Supplier>();

            // Warehouse
            CreateMap<UpdateWarehouseCommand, Warehouse>();
            CreateMap<CreateWarehouseCommand, Warehouse>();

            // Product
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<CreateProductCommand, Product>();

            CreateMap<Product, GetAllProductQueryResult>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier!.CompanyName));

            // StockMovement
            CreateMap<UpdateStockMovementCommand, StockMovement>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<MovementType>(src.Type)))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => Enum.Parse<StockMovementReason>(src.Reason)));

            CreateMap<CreateStockMovementCommand, StockMovement>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<MovementType>(src.Type)))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => Enum.Parse<StockMovementReason>(src.Reason)));

            CreateMap<StockMovement, GetAllStockMovementQueryResult>()
                .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse!.WarehouseName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name));

            // Order
            CreateMap<CreateOrderCommand, Order>().ReverseMap();
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)))
                //.ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => Enum.Parse<PaymentMethod>(src.PaymentMethod)));

            CreateMap<UpdateOrderCommand, Order>().ReverseMap();
            //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)))
            //.ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => Enum.Parse<PaymentMethod>(src.PaymentMethod)));

            CreateMap<Order, GetAllOrderQueryResult>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer!.FirstName + " " + src.Customer!.LastName))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                src.ShippingAddress!.AddressLine1 + " " +
                src.ShippingAddress.AddressLine2 + ", " +
                src.ShippingAddress.City + ", " +
                src.ShippingAddress.State + " " +
                src.ShippingAddress.ZipCode + ", " +
                src.ShippingAddress.Country
            ));


            // OrderItem
            CreateMap<CreateOrderItemCommand, OrderItem>();
            CreateMap<UpdateOrderItemCommand, OrderItem>();

            CreateMap<OrderItem, GetAllOrderItemQueryResult>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
                .ForMember(dest => dest.LineTotal, opt => opt.MapFrom(src => src.LineTotal));

            // Address
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<UpdateAddressCommand, Address>();
            CreateMap<Address, GetAllAddressQueryResult>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer!.FirstName + " " + src.Customer.LastName));
            CreateMap<Address, GetAddressesByCustomerIdQueryResult>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer!.FirstName + " " + src.Customer.LastName));

            // Invoice
            CreateMap<CreateInvoiceCommand, Invoice>();
            CreateMap<UpdateInvoiceCommand, Invoice>();
            CreateMap<Invoice, GetAllInvoiceQueryResult>();
        }
    }
}



//public decimal TotalAmount => Items.Sum(item => item.LineTotal) + TaxAmount + ShippingFee;
//public decimal SubTotal => Items.Sum(item => item.LineTotal);
//public decimal TaxAmount => SubTotal * (TaxRate / 100);
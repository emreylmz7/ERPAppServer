using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.OrderItems.DeleteOrderItemById;

public sealed record class DeleteOrderItemByIdCommand(Guid OrderItemId) : IRequest<Result<string>>;

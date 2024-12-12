using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.DeleteOrderById;

public sealed record class DeleteOrderByIdCommand(Guid OrderId) : IRequest<Result<string>>;

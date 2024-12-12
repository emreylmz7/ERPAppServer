using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.GetAllInvoices;

public sealed record class GetAllInvoicesQuery() : IRequest<Result<List<GetAllInvoiceQueryResult>>>;

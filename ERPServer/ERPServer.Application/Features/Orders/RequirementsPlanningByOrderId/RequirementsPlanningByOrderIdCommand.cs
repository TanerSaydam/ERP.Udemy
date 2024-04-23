using ERPServer.Domain.Dtos;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.RequirementsPlanningByOrderId;
public sealed record RequirementsPlanningByOrderIdCommand(
    Guid Id): IRequest<Result<RequirementsPlanningByOrderIdCommandResponse>>;


public sealed record RequirementsPlanningByOrderIdCommandResponse(
    DateOnly Date,
    string Title,
    List<ProductDto> Products);

internal sealed class RequirementsPlanningByOrderIdCommandHandler(
    IOrderRepository orderRepository) : IRequestHandler<RequirementsPlanningByOrderIdCommand, Result<RequirementsPlanningByOrderIdCommandResponse>>
{
    public async Task<Result<RequirementsPlanningByOrderIdCommandResponse>> Handle(RequirementsPlanningByOrderIdCommand request, CancellationToken cancellationToken)
    {
        Order? order = 
            await orderRepository
            .Where(p => p.Id == request.Id)
            .Include(p=> p.Details)
            .FirstOrDefaultAsync(cancellationToken);

        if(order is null)
        {
            return Result<RequirementsPlanningByOrderIdCommandResponse>.Failure("Sipariş bulunamadı");
        }

        //siparişteki ürünlerin tüm depolardaki adetlerine bakacağım
        //eğer yetersiz ise kaç tane üretilmesi gerektiğini tespit edeceğim
        //her bir ürün için gereken ürün reçetesini alacağım ve o ürünlerin tek tek stoklarına bakacağım
        //üretilmesi için gereken ürünlerden kaç tanesine ihtiyacımız olduğunu tespit edip liste olarak geri dönereceğiz.

        return new RequirementsPlanningByOrderIdCommandResponse(
            DateOnly.FromDateTime(DateTime.Now), 
            order.Number + 
            " Nolu Siparişin İhtiyaç Planlaması", new());

    }
}

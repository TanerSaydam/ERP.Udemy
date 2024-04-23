using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder;

internal sealed class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderDetailRepository orderDetailRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = 
            await orderRepository
            .Where(p => p.Id == request.Id)
            .Include(p=> p.Details)
            .FirstOrDefaultAsync();

        if (order is null)
        {
            return Result<string>.Failure("Sipariş bulunamadı");
        }

        orderDetailRepository.DeleteRange(order.Details);

        List<OrderDetail> newDetails = request.Details.Select(s => new OrderDetail
        {
            OrderId = order.Id,
            Price = s.Price,
            ProductId = s.ProductId,
            Quantity = s.Quantity
        }).ToList();

        await orderDetailRepository.AddRangeAsync(newDetails, cancellationToken);

        mapper.Map(request, order);

        orderRepository.Update(order);

        await unitOfWork.SaveChangesAsync();

        return "Sipariş başarıyla güncellendi";
    }
}

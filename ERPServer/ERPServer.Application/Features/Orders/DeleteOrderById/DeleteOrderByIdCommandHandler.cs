using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.DeleteOrderById;

internal sealed class DeleteOrderByIdCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
    {
        Order order = await orderRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if(order is null)
        {
            return Result<string>.Failure("Sipariş bulunamadı");
        }

        orderRepository.Delete(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Sipariş başarıyla silindi";
    }
}

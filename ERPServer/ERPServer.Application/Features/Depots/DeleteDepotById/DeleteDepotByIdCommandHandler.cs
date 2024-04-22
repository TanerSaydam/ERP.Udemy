using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.DeleteDepotById;

internal sealed class DeleteDepotByIdCommandHandler(
    IDepotRepository depotRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepotByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDepotByIdCommand request, CancellationToken cancellationToken)
    {
        Depot depot = await depotRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if(depot is null)
        {
            return Result<string>.Failure("Depo bulunamadı");
        }

        depotRepository.Delete(depot);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Depo başarıyla silindi";
    }
}

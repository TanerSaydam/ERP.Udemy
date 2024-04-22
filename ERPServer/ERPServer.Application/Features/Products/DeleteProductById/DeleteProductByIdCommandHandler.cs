using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.DeleteProductById;

internal sealed class DeleteProductByIdCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if(product is null)
        {
            return Result<string>.Failure("Ürün bulunamadı");
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ürün başarıyla silindi";
    }
}

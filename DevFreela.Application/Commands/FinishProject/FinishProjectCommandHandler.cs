using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IPaymentService _paymentService;

    public FinishProjectCommandHandler(IUnityOfWork unityOfWork, IPaymentService paymentService)
    {
        _unityOfWork = unityOfWork;
        _paymentService = paymentService;
    }

    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unityOfWork.Projects.GetByIdAsync(request.Id);
        var paymentInfoDto = new PaymentInfoDto(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project!.TotalCost);

        _paymentService.ProcessPayment(paymentInfoDto);
        
        await _unityOfWork.BeginTransactionAsync();
        project.SetPaymentPending();
        await _unityOfWork.SaveChangesAsync();
        await _unityOfWork.CommitTransactionAsync();
        return Unit.Value;
    }
}
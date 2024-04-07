using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService) : IRequestHandler<FinishProjectCommand, Unit>
{
    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        var paymentInfoDto = new PaymentInfoDto(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project!.TotalCost);

        paymentService.ProcessPayment(paymentInfoDto);
        project.SetPaymentPending();

        await projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
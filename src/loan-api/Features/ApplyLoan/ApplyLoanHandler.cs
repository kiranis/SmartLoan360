using MediatR;

namespace Features.ApplyLoan
{
    public class ApplyLoanHandler : IRequestHandler<ApplyLoanCommand, ApplyLoanResult>
    {
        public Task<ApplyLoanResult> Handle(ApplyLoanCommand request, CancellationToken cancellationToken)
        {
            // Placeholder logic for approval
            var approved = request.Amount * 0.9m;
            return Task.FromResult(new ApplyLoanResult("Loan approved", approved));
        }
    }
}
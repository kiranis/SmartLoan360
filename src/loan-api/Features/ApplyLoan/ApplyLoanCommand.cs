using MediatR;

namespace Features.ApplyLoan
{
    public record ApplyLoanCommand(string FullName, decimal Amount, int TermMonths) : IRequest<ApplyLoanResult>;

    public record ApplyLoanResult(string Message, decimal ApprovedAmount);
}
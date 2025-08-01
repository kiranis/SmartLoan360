using MediatR;

namespace Features.ScoreLoan
{
    public record ScoreLoanCommand(string FullName, decimal Amount, int TermMonths) : IRequest<ScoreLoanResult>;

    public record ScoreLoanResult(float Score, string Risk);
}
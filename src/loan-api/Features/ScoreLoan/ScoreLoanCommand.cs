using MediatR;

public record ScoreLoanCommand(string FullName, decimal Amount, int TermMonths) : IRequest<ScoreLoanResult>;

public record ScoreLoanResult(float Score, string Risk);

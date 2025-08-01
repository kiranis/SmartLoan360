using Features.ScoreLoan;
using MediatR;

public class ScoreLoanHandler : IRequestHandler<ScoreLoanCommand, ScoreLoanResult>
{
    private readonly IHttpClientFactory _clientFactory;

    public ScoreLoanHandler(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<ScoreLoanResult> Handle(ScoreLoanCommand request, CancellationToken cancellationToken)
    {
        var client = _clientFactory.CreateClient("ScoringApi");
        var response = await client.PostAsJsonAsync("/score", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ScoreLoanResult>(cancellationToken: cancellationToken);
        return result!;
    }
}

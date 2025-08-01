using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Moq.Protected;
using MediatR;
using System.Net.Http.Headers;

public class ScoreLoanHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsScoreLoanResult()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>("SendAsync",
               ItExpr.IsAny<HttpRequestMessage>(),
               ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.OK,
               Content = JsonContent.Create(new { score = 0.6f, risk = "medium" })
           });

        var client = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new System.Uri("http://test/")
        };

        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

        var handler = new ScoreLoanHandler(factory.Object);
        var command = new ScoreLoanCommand("Test User", 10000, 24);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0.6f, result.Score);
        Assert.Equal("medium", result.Risk);
    }
}

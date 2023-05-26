using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Housemate.Helpers.Tests.Models;

public sealed class TestWebApplicationFactory<TEntity> : WebApplicationFactory<TEntity>, IAsyncLifetime
    where TEntity : ControllerBase
{

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}

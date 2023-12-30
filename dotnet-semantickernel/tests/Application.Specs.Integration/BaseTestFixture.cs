using NUnit.Framework;

namespace dotnet_semantickernel.Specs.Application.Integration;

using static TestBase;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
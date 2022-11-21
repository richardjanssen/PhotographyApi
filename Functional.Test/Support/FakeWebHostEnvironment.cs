using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Test.Helpers;

public class FakeWebHostEnvironment : IWebHostEnvironment
{
    public string WebRootPath { get => "fakeWebRootPath"; set => throw new NotImplementedException(); }
    public IFileProvider WebRootFileProvider { get => new NullFileProvider(); set => throw new NotImplementedException(); }
    public string ApplicationName { get => "fakeApplicationName"; set => throw new NotImplementedException(); }
    public IFileProvider ContentRootFileProvider { get => new NullFileProvider(); set => throw new NotImplementedException(); }
    public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string EnvironmentName { get => "fakeEnvironmentName"; set => throw new NotImplementedException(); }
}

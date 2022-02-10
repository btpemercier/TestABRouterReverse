using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TestABRouterReverse;
using Yarp.ReverseProxy.LoadBalancing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILoadBalancingPolicy, TestABLoadBalancingPolicy>();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();

app.MapReverseProxy();

app.Run();

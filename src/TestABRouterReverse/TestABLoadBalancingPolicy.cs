using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Yarp.ReverseProxy.LoadBalancing;
using Yarp.ReverseProxy.Model;

namespace TestABRouterReverse;

public class TestABLoadBalancingPolicy : ILoadBalancingPolicy
{
    private readonly IConfiguration _configuration;
    public string Name => "TestAB";

    public TestABLoadBalancingPolicy(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DestinationState? PickDestination(HttpContext context, ClusterState cluster, IReadOnlyList<DestinationState> availableDestinations)
    {
        if (_configuration.GetValue<bool>("TestAB"))
        {
            return availableDestinations.First(m => m.DestinationId == "aws");
        }
        
        return availableDestinations.First(m => m.DestinationId == "legacy"); 
    }
}
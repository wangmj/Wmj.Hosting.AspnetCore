using Grpc.Core;
using Wmj.Hosting.Grpc.Protos;

namespace Wmj.Hosting.Grpc.Services
{
    public class OrderServices:Order.OrderBase
    {
        public override Task<AddOrderRes> AddOrder(AddOrderReq request, ServerCallContext context)
        {
            string name=request.Name;
            Console.WriteLine($"Received request from client, Name is {name}");
            Console.WriteLine(name);
            AddOrderRes res=new AddOrderRes();
            res.OrderId = 111;
            res.Success = true;
            return Task.FromResult(res);
            //return base.AddOrder(request, context);
        }
    }
}

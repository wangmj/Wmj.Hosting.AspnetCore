// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using Wmj.Hosting.Grpc.Protos;


string grpcServer = "http://localhost:5236";
try
{
    var channel = GrpcChannel.ForAddress(grpcServer);
    var orderClient = new Order.OrderClient(channel);
    AddOrderReq req = new AddOrderReq();
    req.Name = "tttt";
    req.Num = 1;
    var addOrderRes = await orderClient.AddOrderAsync(req);
    Console.WriteLine($"Success:{addOrderRes.Success}");
    Console.WriteLine($"OrderId:{addOrderRes.OrderId}");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
Console.WriteLine("Hello, World!");
Console.Read();
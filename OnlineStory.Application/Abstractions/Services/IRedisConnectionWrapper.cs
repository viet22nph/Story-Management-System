
using OnlineStory.Contract.Share.Enums;
using  StackExchange.Redis;

namespace OnlineStory.Application.Abstractions.Services;

public interface IRedisConnectionWrapper : IDisposable
{
    IDatabase GetDatabase(int db);

    IServer GetServer(System.Net.EndPoint endPoint);

    System.Net.EndPoint[] GetEndPoints();

    void FlushDatabase(RedisDatabaseNumber db);

}

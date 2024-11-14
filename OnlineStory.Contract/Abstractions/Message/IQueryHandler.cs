
using MediatR;
using OnlineStory.Contract.Share;

namespace OnlineStory.Contract.Abstractions.Message;

public interface IQueryHandler<TRequest, TResponse>: IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : IQuery<TResponse>
{
}

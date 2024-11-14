
using MediatR;
using OnlineStory.Contract.Share;
namespace OnlineStory.Contract.Abstractions.Message
{
    public interface ICommandHandler<TRequest,TResponse>: IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : ICommand<TResponse>
    {
    }
}

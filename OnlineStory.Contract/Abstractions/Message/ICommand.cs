
using MediatR;
using OnlineStory.Contract.Share;
namespace OnlineStory.Contract.Abstractions.Message;

public interface ICommand<TResponse>: IRequest<Result<TResponse>>
{
}

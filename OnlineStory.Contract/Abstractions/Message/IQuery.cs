
using MediatR;
using OnlineStory.Contract.Share;

namespace OnlineStory.Contract.Abstractions.Message;

public interface IQuery<TRespone>: IRequest<Result<TRespone>>
{
}



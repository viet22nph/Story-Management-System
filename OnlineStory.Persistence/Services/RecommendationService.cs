
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Domain.Entities;

namespace OnlineStory.Persistence.Services;

public class RecommendationService
{
    private readonly IUnitOfWork _unitOfWork;
    public RecommendationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
}

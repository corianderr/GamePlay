namespace GamePlay.Domain.Contracts.Services; 

public interface IRecalculationService {
    Task RecalculateUserRelationsAsync();
    Task RecalculateAverageRatingAsync();
}
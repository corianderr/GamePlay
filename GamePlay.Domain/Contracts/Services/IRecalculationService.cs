namespace GamePlay.Domain.Contracts.Services; 

public interface IRecalculationService {
    Task RecalculateUserRelations();
    Task RecalculateAverageRating();
}
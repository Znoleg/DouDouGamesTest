namespace Assets.Scripts.Infrastructure.Services
{
    public interface IRandomService
    {
        float GetRandomFloat(float min, float max);
        int GetRandomInt(int min, int max);
    }
}
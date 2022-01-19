using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class RandomService
    {
        public RandomService() { }

        public float GetRandomFloat(float min, float max) => Random.Range(min, max);

        public int GetRandomInt(int min, int max) => Random.Range(min, max);
    }
}

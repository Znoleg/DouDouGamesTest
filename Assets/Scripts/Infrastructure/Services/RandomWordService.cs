using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Infrastructure.Services
{
    public class RandomWordService
    {
        private readonly RandomService _random;
        private readonly List<char> _defaultCharset;

        public RandomWordService(RandomService randomService)
        {
            _random = randomService;
            _defaultCharset = new List<char>();
            FillDefaultCharset();
        }

        public string GetRandomWord(int length) =>
            GetRandomWord(length, _defaultCharset);

        public string GetRandomWord(int length, IEnumerable<char> charset)
        {
            string word = "";
            for (int i = 0; i < length; i++) // better to use string builder but nvm for now
            {
                word += (char)(_random.GetRandomInt(charset.First(), charset.Last()));
            }

            return word;
        }

        private void FillDefaultCharset()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                _defaultCharset.Add(c);
            }
        }
    }
}

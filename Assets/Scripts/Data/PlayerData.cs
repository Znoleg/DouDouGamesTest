using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public struct PlayerData
    {
        public string Nickname;
        public int Score;

        public PlayerData(string nickname, int score)
        {
            Nickname = nickname;
            Score = score;
        }

        public override string ToString() =>
            $"Nickname: {Nickname}, Score: {Score}";
    }
}
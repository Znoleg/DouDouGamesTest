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

        //public override bool Equals(object other)
        //{
        //    if (other is PlayerData otherData)
        //        return Nickname.Equals(otherData.Nickname) && Score.Equals(otherData.Score);

        //    return base.Equals(other);
        //}

        //public override int GetHashCode()
        //{
        //    return (Nickname, Score).GetHashCode();
        //}

        public override string ToString()
        {
            return $"Nickname: {Nickname}, Score: {Score}";
        }
    }
}
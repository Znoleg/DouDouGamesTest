using Assets.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class PlayerInputWindow : WindowBase
    {
        [SerializeField]
        private TMP_InputField _nameInput;

        [SerializeField]
        private TMP_InputField _scoreInput;

        protected TMP_InputField NameInput => _nameInput;
        protected TMP_InputField ScoreInput => _scoreInput;

        protected PlayerData GetInputData()
        {
            if (!int.TryParse(_scoreInput.text, out int score))
                throw new UnityException($"Bad score text {_scoreInput.text} written at scoreInput");

            return new PlayerData(_nameInput.text, score);
        }
    }
}

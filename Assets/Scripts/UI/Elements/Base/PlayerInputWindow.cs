using Assets.Scripts.Data;
using TMPro;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.UI.Elements.Base
{
    public abstract class PlayerInputWindow : WindowBase
    {
        [SerializeField]
        private TMP_InputField _nameInput;

        [SerializeField]
        private TMP_InputField _scoreInput;

        [SerializeField]
        private TextMeshProUGUI _warningLabel;

        [SerializeField]
        private bool _useUniqueNaming;

        protected TMP_InputField NameInput => _nameInput;
        protected TMP_InputField ScoreInput => _scoreInput;
        protected bool UseUniqueNaming => _useUniqueNaming;

        protected override void Awake()
        {
            base.Awake();
            AssignInputHandler();
        }

        protected bool IsUniqueName(IEnumerable<PlayerData> sequence, PlayerData playerData)
        {
            if (sequence.Any(data => data.Nickname == playerData.Nickname))
            {
                SetWarningText($"There is a player with nickname {playerData.Nickname} already!");
                return false;
            }
            return true;
        }

        protected PlayerData GetInputData()
        {
            if (!int.TryParse(_scoreInput.text, out int score))
                throw new UnityException($"Can't cast score text {_scoreInput.text} to int");

            return new PlayerData(_nameInput.text, score);
        }

        protected void SetWarningText(string text) =>
            _warningLabel.text = text;

        protected void ClearWarning() =>
            SetWarningText("");

        private void AssignInputHandler() =>
            _nameInput.onValueChanged.AddListener((string s) => ClearWarning());
    }
}

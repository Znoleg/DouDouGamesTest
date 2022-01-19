using Assets.Scripts.Data;
using Assets.Scripts.UI.Elements.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class EditWindow : PlayerInputWindow
    {
        [SerializeField]
        private LeaderboardWindow _leaderBoardWindow;

        [SerializeField]
        private PlayersData _playersData;

        [SerializeField]
        private Button _editButton;

        [SerializeField]
        private bool _setEditText = true;

        private int _index;
        private PlayerData _editData;

        protected override void Awake()
        {
            base.Awake();
            AssignEditHandler();
        }

        public void Initialize(PlayerData editData, int index)
        {
            _index = index;
            _editData = editData;

            if (_setEditText)
                SetEditText();
        }

        private void SetEditText()
        {
            NameInput.text = _editData.Nickname;
            ScoreInput.text = $"{_editData.Score}";
        }

        private void AssignEditHandler() =>
            _editButton.onClick.AddListener(Edit);

        private void Edit()
        {
            PlayerData inputData = GetInputData();
            if (UseUniqueNaming)
            {
                IEnumerable<PlayerData> datasWithoutEdited = _playersData.PlayerDatas
                    .Where((data, i) => i != _index);

                if (!IsUniqueName(datasWithoutEdited, inputData)) return;
            }

            _playersData.Edit(_index, inputData);
            Close();
            _leaderBoardWindow.Open();
        }
    }
}

using Assets.Scripts.Data;
using Assets.Scripts.UI.Elements.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class AddPlayerWindow : PlayerInputWindow
    {
        [SerializeField]
        private LeaderboardWindow _leaderBoardWindow;
        
        [SerializeField]
        private PlayersData _playersData;

        [SerializeField]
        private Button _addButton;

        [SerializeField]
        private bool _refreshInputOnOpen = true;

        protected override void Awake()
        {
            base.Awake();
            AssignAddHandler();
        }

        public override void Open()
        {
            base.Open();
            if (_refreshInputOnOpen)
                RefreshInputs();
        }

        private void RefreshInputs()
        {
            NameInput.text = "";
            ScoreInput.text = "";
        }

        private void AssignAddHandler() =>
            _addButton.onClick.AddListener(CreateData);

        private void CreateData()
        {
            PlayerData inputData = GetInputData();
            if (UseUniqueNaming)
            {
                if (!IsUniqueName(_playersData.PlayerDatas, inputData)) return;
            }

            _playersData.Add(inputData);
            Close();
            _leaderBoardWindow.Open();
        }
    }
}
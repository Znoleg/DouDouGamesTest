using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EditWindow : PlayerInputWindow
    {
        [SerializeField]
        private PlayersData _playersData;

        [SerializeField]
        private Button _editButton;
        
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
            NameInput.text = _editData.Nickname;
            ScoreInput.text = $"{_editData.Score}";
        }

        private void AssignEditHandler() =>
            _editButton.onClick.AddListener(Edit);

        private void Edit() =>
            _playersData.Edit(_index, GetInputData());
    }
}

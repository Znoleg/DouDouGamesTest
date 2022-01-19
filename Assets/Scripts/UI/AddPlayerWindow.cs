using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class AddPlayerWindow : PlayerInputWindow
    {
        [SerializeField]
        private PlayersData _playersData;

        [SerializeField]
        private Button _addButton;

        protected override void Awake()
        {
            base.Awake();
            AssignAddHandler();
        }

        private void AssignAddHandler() =>
            _addButton.onClick.AddListener(CreateData);

        private void CreateData() =>
            _playersData.Add(GetInputData());
    }
}
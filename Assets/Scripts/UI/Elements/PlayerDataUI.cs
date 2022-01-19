using Assets.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class PlayerDataUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI _numberText;

        [SerializeField]
        private TextMeshProUGUI _nicknameText;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        private Button _selectButton;

        private GameObject _containter;
        private EditWindow _editWindow;
        private PlayerData _ownData;
        private int _index;

        public TextMeshProUGUI NumberText => _numberText;
        public Button SelectButton => _selectButton;
        public PlayerData OwnData => _ownData;
        public int Index => _index;

        public void Construct(GameObject container, EditWindow editWindow)
        {
            _containter = container;
            _editWindow = editWindow;
        }

        public void SetOwnData(PlayerData ownData) =>
            _ownData = ownData;

        public void SetNumber(int number)
        {
            _index = number - 1;
            _numberText.text = $"{number}";
        }

        public void SetNickname(string nickname) =>
            _nicknameText.text = $"{nickname}";

        public void SetScore(int score) =>
            _scoreText.text = $"{score}";

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OpenEditWindow();
            }
        }

        private void OpenEditWindow()
        {
            _editWindow.Initialize(editData: OwnData, _index);
            _editWindow.Open();
            _containter.SetActive(false);
        }
    }
}

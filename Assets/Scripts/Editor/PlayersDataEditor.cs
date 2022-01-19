using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.UI.Elements;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(PlayersData))]
    public class PlayersDataEditor : UnityEditor.Editor
    {
        private const string DataContentTag = "PlayerDataContent";
        private const string DataUIPrefabPath = "UI/Prefabs/Player Data";
        private const string PlayersDataPath = "Data/PlayersData";
        private PlayerDataUI _prefab;
        private GameObject _container;
        private bool _isVisualized;
        
        private int _wordCount = 0;
        private int _wordLengthMin = 0;
        private int _wordLengthMax = 0;
        private int _scoreMin = 0;
        private int _scoreMax = 0;

        private RandomService _randomService;
        private RandomWordService _randomWordService;

        private void OnEnable()
        {
            _randomService = new RandomService();
            _randomWordService = new RandomWordService(_randomService);

            _prefab = Resources.Load<PlayerDataUI>(DataUIPrefabPath);
            if (_prefab == null)
                Debug.LogWarning($"{nameof(PlayerDataUI)} can't be found on {DataUIPrefabPath} path");

            _container = GameObject.FindGameObjectWithTag(DataContentTag);
            if (_container == null)
                Debug.LogWarning($"DataContainer with tag {DataContentTag} can't be found");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (_prefab == null || _container == null) return;

            PlayersData playersData = (PlayersData)target;

            HandleReorderButton(playersData);
            HandleVisualiseButton(playersData);
            HandleRemoveVisualizeButton();
            HandleClearButton(playersData);
            HandleGenerateWordsSequence(playersData);

            static void HandleReorderButton(PlayersData playersData)
            {
                if (GUILayout.Button("Reorder"))
                {
                    playersData.Reorder();
                }
            }

            void HandleVisualiseButton(PlayersData playersData)
            {
                if (!_isVisualized && GUILayout.Button("Visualize"))
                {
                    _isVisualized = true;
                    CreateAllUIData(playersData);
                }
            }

            void HandleRemoveVisualizeButton()
            {
                if (_isVisualized && GUILayout.Button("Remove visualize"))
                {
                    _isVisualized = false;
                    DestroyUIData();
                }
            }

            static void HandleClearButton(PlayersData playersData)
            {
                if (GUILayout.Button("Clear"))
                {
                    while (playersData.PlayerDatas.Count != 0)
                        playersData.RemoveAt(0);
                }
            }

            void HandleGenerateWordsSequence(PlayersData playersData)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Generate random words", EditorStyles.boldLabel);

                _wordCount = EditorGUILayout.IntField("Count: ", _wordCount);
                _wordLengthMin = EditorGUILayout.IntField("Min length: ", _wordLengthMin);
                _wordLengthMax = EditorGUILayout.IntField("Max length: ", _wordLengthMax);
                _scoreMin = EditorGUILayout.IntField("Score min: ", _scoreMin);
                _scoreMax = EditorGUILayout.IntField("Score max: ", _scoreMax);

                if (GUILayout.Button("Generate"))
                {
                    CreateRandomWords(playersData);
                }
            }
        }

        private void CreateRandomWords(PlayersData playersData)
        {
            for (int i = 0; i < _wordCount; i++)
            {
                int wordLength = _randomService.GetRandomInt(_wordLengthMin, _wordLengthMax);
                string nickname = _randomWordService.GetRandomWord(wordLength);
                int score = _randomService.GetRandomInt(_scoreMin, _scoreMax);
                playersData.Add(new PlayerData(nickname, score));
            }
        }

        private void DestroyUIData()
        {
            int childCount = _container.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(_container.transform.GetChild(0).gameObject);
            }
        }

        private void CreateAllUIData(PlayersData playersData)
        {
            for (int index = 0; index < playersData.PlayerDatas.Count; index++)
            {
                PlayerData playerData = playersData.PlayerDatas[index];
                CreateUIData(playerData, index + 1);
            }
        }

        private void CreateUIData(PlayerData playerData, int number)
        {
            PlayerDataUI dataUIInstance = Instantiate(_prefab, _container.transform);
            InitializeUIData(playerData, dataUIInstance, number);
        }

        private void InitializeUIData(PlayerData playerData, PlayerDataUI uiInstance, int number)
        {
            uiInstance.SetScore(playerData.Score);
            uiInstance.SetNickname(playerData.Nickname);
            uiInstance.SetNumber(number);
        }
    }
}
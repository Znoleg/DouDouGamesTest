﻿using Assets.Scripts.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LeaderboardWindow : WindowBase
    {
        [SerializeField]
        private PlayersData _playersData;

        [SerializeField]
        private PlayerDataUI _playerDataUIPrefab;

        [SerializeField]
        private Transform _playerListHolder;

        [SerializeField]
        private Button _removeButton;

        [SerializeField]
        private EditWindow _editWindow;

        private PlayerDataUI _selectedData;
        private List<PlayerDataUI> _createdData
            = new List<PlayerDataUI>();
        private readonly Dictionary<PlayerData, int> _playerDataIndex
            = new Dictionary<PlayerData, int>();

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();
            _playersData.Reorder();
            AssignRemoveHandler();
            StartUpdatingData();
        }

        private void Start() => CreateStartingUIData();

        private void OnDestroy() => StopUpdatingData();

        #endregion

        private void TryRemoveSelectedData()
        {
            bool dataIsSelected = _selectedData != null;
            if (!dataIsSelected) return;
            
            _playersData.RemoveAt(_selectedData.Index);

            _createdData.Remove(_selectedData);
            Destroy(_selectedData.gameObject);
            _selectedData = null;

            RefreshAllNumberText();
        }

        private void AddNewData(PlayerData playerData, int index)
        {
            PlayerDataUI uiInstance = CreateUIData(playerData, number: index + 1);

            bool dataIsLast = index == _playersData.PlayerDatas.Count;
            if (!dataIsLast)
                ReorderAddedUI(uiInstance, index);
        }

        private void EditData(int oldIndex, PlayerData newData, int newIndex)
        {
            PlayerDataUI uiData = _createdData[oldIndex];

            Debug.Log($"OldData: {uiData.OwnData}, NewData: {newData}");
            InitializeUIData(newData, uiData, newIndex + 1);

            ReorderAddedUI(uiData, newIndex);
        }

        private void ReorderAddedUI(PlayerDataUI lastCreated, int index)
        {
            Debug.Log($"Add data: {lastCreated.OwnData}");
            InitPerfomanceDictionary();
            ReorderDataList();
            SetTransfromIndex(lastCreated, index);
            RefreshAllNumberText();

            void InitPerfomanceDictionary()
            {
                _playerDataIndex.Clear();
                for (int i = 0; i < _playersData.PlayerDatas.Count; i++)
                {
                    _playerDataIndex[_playersData.PlayerDatas[i]] = i;
                }
            }

            void ReorderDataList()
            {
                try
                {
                    _createdData = _createdData
                                    .OrderBy((PlayerDataUI dataUi) => _playerDataIndex[dataUi.OwnData])
                                    .ToList();
                }
                catch (KeyNotFoundException exception)
                {
                    Debug.LogError($"Not found: ");
                }
            }

            static void SetTransfromIndex(PlayerDataUI lastCreated, int index)
            {
                lastCreated.transform.SetSiblingIndex(index);
            }
        }

        void RefreshAllNumberText()
        {
            for (int i = 0; i < _createdData.Count; i++)
            {
                _createdData[i].SetNumber(i + 1);
            }
        }

        private void CreateStartingUIData()
        {
            for (int index = 0; index < _playersData.PlayerDatas.Count; index++)
            {
                PlayerData playerData = _playersData.PlayerDatas[index];
                CreateUIData(playerData, index + 1);
            }
        }

        private PlayerDataUI CreateUIData(PlayerData playerData, int number)
        {
            PlayerDataUI dataUIInstance = Instantiate(_playerDataUIPrefab, _playerListHolder);
            dataUIInstance.SelectButton.onClick.AddListener(() => SetSelectedData(dataUIInstance));
            
            InitializeUIData(playerData, dataUIInstance, number);
            _createdData.Add(dataUIInstance);
            return dataUIInstance;
        }

        private void InitializeUIData(PlayerData playerData, PlayerDataUI uiInstance, int number)
        {
            uiInstance.Initialize(playerData, gameObject, _editWindow);
            uiInstance.SetScore(playerData.Score);
            uiInstance.SetNickname(playerData.Nickname);
            uiInstance.SetNumber(number);
        }

        private void SetSelectedData(PlayerDataUI selected) =>
            _selectedData = selected;

        private void AssignRemoveHandler() =>
            _removeButton.onClick.AddListener(TryRemoveSelectedData);

        private void StartUpdatingData()
        {
            _playersData.OnAdd += AddNewData;
            _playersData.OnEdit += EditData;
        }

        private void StopUpdatingData()
        {
            _playersData.OnAdd -= AddNewData;
            _playersData.OnEdit -= EditData;
        }
    }
}

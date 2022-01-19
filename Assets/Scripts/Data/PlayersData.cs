using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "PlayersData", menuName = "Data/PlayersData")]
    public class PlayersData : ScriptableObject, IEnumerable
    {
        [SerializeField]
        private List<PlayerData> _playerDatas;

        public IReadOnlyList<PlayerData> PlayerDatas => _playerDatas;

        public event Action<PlayerData, int> OnAdd;
        public event Action<int, PlayerData, int> OnEdit;
        public event Action<int> OnRemove;

        public IEnumerator GetEnumerator() =>
            _playerDatas.GetEnumerator();

        public void Reorder() =>
            _playerDatas = _playerDatas.OrderBy(data => data.Score).ToList();

        public void Add(PlayerData playerData)
        {
            int newIndex = FindAddIndex(playerData.Score);
            _playerDatas.Insert(newIndex, playerData);

            OnAdd?.Invoke(playerData, newIndex);
        }

        public void Edit(int index, PlayerData newData)
        {
            int oldIndex = index;
            PlayerData oldData = _playerDatas[oldIndex];
            _playerDatas.RemoveAt(oldIndex);
            
            int newIndex = FindAddIndex(newData.Score);
            _playerDatas.Insert(newIndex, newData);

            OnEdit?.Invoke(oldIndex, newData, newIndex);
        }

        public void RemoveAt(int index)
        {
            _playerDatas.RemoveAt(index);
            OnRemove?.Invoke(index);
        }

        private int FindAddIndex(int dataScore)
        {
            if (_playerDatas.Count == 0) return 0;

            int addIndex = 0;
            int currentScore = _playerDatas[addIndex].Score;
            while (dataScore > currentScore)
            {
                addIndex++;
                if (addIndex == _playerDatas.Count) break;
                currentScore = _playerDatas[addIndex].Score;
            }
            
            return addIndex;
        }
    }
}
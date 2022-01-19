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
        public List<PlayerData> PlayerDatas;

        public event Action<PlayerData, int> OnAdd;
        public event Action<int, PlayerData, int> OnEdit;
        public event Action<int> OnRemove;

        public IEnumerator GetEnumerator() =>
            PlayerDatas.GetEnumerator();

        public void Reorder() =>
            PlayerDatas = PlayerDatas.OrderBy(data => data.Score).ToList();

        public void Add(PlayerData playerData)
        {
            PlayerDatas.Add(playerData);
            Reorder();
            OnAdd?.Invoke(playerData, GetIndex(playerData));
        }

        public void Edit(int index, PlayerData newData)
        {
            int oldIndex = index;
            PlayerData oldData = PlayerDatas[index];
            PlayerDatas[index] = newData;
            Reorder();
            int newIndex = PlayerDatas.FindIndex(data => data.Equals(newData));

            OnEdit?.Invoke(oldIndex, newData, newIndex);
        }

        public void RemoveAt(int index)
        {
            PlayerDatas.RemoveAt(index);
            Reorder();
            OnRemove?.Invoke(index);
        }

        private int GetIndex(PlayerData playerData) =>
            PlayerDatas.FindIndex(data => data.Equals(playerData));
    }
}
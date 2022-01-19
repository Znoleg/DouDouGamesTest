using Assets.Scripts.UI.Elements.Base;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Infrastructure
{
    [Serializable]
    public class CloseWindowButton
    {
        public Button Button;
        public WindowBase NextOpenWindow;
    }
}
using System;
using System.Linq;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Infrastructure.InputConstraints
{
    [Serializable]
    public class AcceptInputConstraint
    {
        public Button AcceptInputButton;
        public InputConstraint[] InputConstraints;

        public bool Satisfies => InputConstraints.All(cons => cons.Satisfies);
    }
}

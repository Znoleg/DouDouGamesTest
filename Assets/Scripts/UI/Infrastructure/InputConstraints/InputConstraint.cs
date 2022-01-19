using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Infrastructure.InputConstraints
{
    [Serializable]
    public class InputConstraint
    {
        public TMP_InputField InputField;
        public InputType InputType = InputType.Any;

        [Min(0)]
        public int MinCharacters = 0;

        [Min(0)]
        public int MaxCharacters = 999;

        public bool Satisfies => InputField.text.Length >= MinCharacters &&
            InputField.text.Length <= MaxCharacters && CheckType();

        private bool CheckType()
        {
            switch (InputType)
            {
                case InputType.Any:
                    return true;

                case InputType.NumbersOnly:
                    return CheckNumbersOnly();

                default:
                    throw new NotImplementedException($"Not Implemented {InputType} " +
                        $"of {nameof(InputType)} enum");
            }
        }

        private bool CheckNumbersOnly() => int.TryParse(InputField.text, out int _);
    }
}

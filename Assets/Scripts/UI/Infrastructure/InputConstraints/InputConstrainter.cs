using UnityEngine;

namespace Assets.Scripts.UI.Infrastructure.InputConstraints
{
    public class InputConstrainter : MonoBehaviour
    {
        [SerializeField]
        private AcceptInputConstraint[] Constraints;

        private void Awake() => CheckAndAssignHandlers();

        private void CheckAndAssignHandlers()
        {
            foreach (AcceptInputConstraint acceptConstraint in Constraints)
            {
                foreach (InputConstraint inputConstraint in acceptConstraint.InputConstraints)
                {
                    CheckSatisfy(acceptConstraint);

                    inputConstraint.InputField.onValueChanged.AddListener(
                        (str) => CheckSatisfy(acceptConstraint));
                }
            }
        }

        private void CheckSatisfy(AcceptInputConstraint acceptConstraint)
        {
            if (acceptConstraint.Satisfies)
                EnableButton(acceptConstraint);
            else
                DisableButton(acceptConstraint);
        }

        private void EnableButton(AcceptInputConstraint constraint) =>
            constraint.AcceptInputButton.interactable = true;

        private void DisableButton(AcceptInputConstraint constraint) =>
            constraint.AcceptInputButton.interactable = false;
    }
}

using com.game.misc;
using UnityEngine;
using UnityEngine.InputSystem;
using RebindingOperation = UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation;

namespace com.game.extensions
{
    public static class InputExtensions
    {
        public static InputAction FindActionByRef(this PlayerInputActions inputActions, InputActionReference inputActionRef)
        {
            foreach (var map in inputActions.asset.actionMaps)
                foreach (var action in map.actions)
                    if (action.id == inputActionRef.action.id) return action;

            Debug.LogError("Matching action not found in inputActions");
            return null;
        }

        public static RebindingOperation WithDefaultControlsExcluded(this RebindingOperation operation)
        {
            input.InputSettings.Rebinding.RebindExcludedControls.ForEach(excludedControlPath =>
            {
                operation = operation.WithControlsExcluding(excludedControlPath);
            });

            return operation;
        }
    }
}

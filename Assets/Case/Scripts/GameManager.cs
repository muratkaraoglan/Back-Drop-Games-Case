using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-7)]
public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public string ChestInteractPrompt { get; private set; }
    [field: SerializeField] public string CollectibleItemInteractPrompt { get; private set; }

    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private ThirdPersonController _thirdPersonController;

    private void OnEnable()
    {
        InputReader.Instance.OnPerspectiveButtonPressed += OnPerspectiveButtonPressed;
    }
    private void OnDisable()
    {
        InputReader.Instance.OnPerspectiveButtonPressed -= OnPerspectiveButtonPressed;
    }

    private void OnPerspectiveButtonPressed()
    {
        _firstPersonController.enabled = !_firstPersonController.enabled;
        _thirdPersonController.enabled = !_thirdPersonController.enabled;
    }
}

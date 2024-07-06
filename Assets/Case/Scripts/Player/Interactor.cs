using UnityEngine;


public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _intractionRadius;
    [SerializeField] private LayerMask _interactionLayer;

    private readonly Collider[] _interactedColliders = new Collider[1];
    private int _numberOfFound;
    private IInteractable _interactable;
    private bool _canInteract = true;
    private void OnEnable()
    {
        InputReader.Instance.OnPressInteractButtonEvent += OnPressInteractButtonEvent;
    }

    private void Update()
    {
        if (!_canInteract) return;
        _numberOfFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _intractionRadius, _interactedColliders, _interactionLayer);

        if (_numberOfFound > 0)
        {
            if (_interactable == null)
            {
                _interactable = _interactedColliders[0].GetComponent<IInteractable>();
                _interactable.PromptTextOn();
            }
        }
        else
        {
            if (_interactable != null)
            {
                _interactable.PrompTextOff();
                _interactable = null;
            }
        }
    }

    private void OnDisable()
    {
        InputReader.Instance.OnPressInteractButtonEvent -= OnPressInteractButtonEvent;
    }

    private void OnPressInteractButtonEvent()
    {
        if (_canInteract && _interactable != null)
        {
            _canInteract = false;
            _interactable.Interact();
            _interactable.PrompTextOff();
        }
        else
        {
            _interactable = null;
            _canInteract = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _intractionRadius);
    }
}

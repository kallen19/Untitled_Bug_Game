using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;

public class InteractionDetector : MonoBehaviour
{
private IInteractable interactableInRange = null;
public GameObject InteractionIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractionIcon.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
                        interactableInRange?.Interact();

        }
    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            InteractionIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            InteractionIcon.SetActive(false);
        }
    }
}

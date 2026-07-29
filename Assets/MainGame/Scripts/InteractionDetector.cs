using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    private KeyCode interactKeycode;
    public GameObject InteractionIcon;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactKeycode = GameObject.Find("Keycodes").GetComponent<Keycodes>().interactKey;

        InteractionIcon.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(interactKeycode) && interactableInRange != null)
        {
            interactableInRange.Interact();
            if(!interactableInRange.CanInteract())
            {
                interactableInRange = null;
                InteractionIcon.SetActive(false);
            }
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

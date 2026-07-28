using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickMeUp : MonoBehaviour, IInteractable
{
    [SerializeField] Item itemType;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] HealthManager healthManager; 
    [SerializeField] InventoryUI inventoryUI;
    Color[] colors;

    public bool stocked {get; private set;}
    int currentColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentColor = 0;
        colors = new Color[] {Color.magenta, Color.coral, Color.teal, Color.indigo};
        sr.color = colors[currentColor];
        stocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        currentColor = currentColor == colors.Length - 1 ? 0 : currentColor + 1;
        sr.color = colors[currentColor];

        //healthManager.BeHealed(1, true);

        inventoryUI.PickUp(itemType);
        
        // if nmber of uses, decrease
        stocked = false;
    }

    public bool CanInteract()
    {
        return stocked;
    }

}

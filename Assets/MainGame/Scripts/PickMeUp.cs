using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickMeUp : MonoBehaviour, IInteractable
{
    [SerializeField] Item itemType;
    [SerializeField] SpriteRenderer sr;
    HealthManager healthManager; 
    InventoryUI inventoryUI;

    public int stock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
                healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();
                inventoryUI = GameObject.Find("ItemContainer").GetComponent<InventoryUI>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {

        //healthManager.BeHealed(1, true);

        inventoryUI.PickUp(itemType);
        
        // if nmber of uses, decrease
        stock--;
        
        // if 0, disable thyself
        if(stock <= 0)
        {
            sr.color = new Color(0, 0, 0, 0);
        }
    }

    public bool CanInteract()
    {
        return stock > 0;
    }

}

using UnityEngine;

public class CampfirePuzzle : MonoBehaviour, IInteractable
{
    private InventoryUI inventory;
    private SpriteRenderer sr;
    public Sprite litSprite;
    private bool lit;
    public int sticksRequired;

    public HealthManager healthManager;
    
    public void Start()
    {
        inventory = GameObject.Find("ItemContainer").GetComponent<InventoryUI>();
        sr = GetComponent<SpriteRenderer>();
        lit = false;
        
        healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();
    }
    
    public void Interact()
    {
        if (inventory.HasItems(Item.Stick, sticksRequired))
        {
            sr.sprite = litSprite;
            for (int i = 0; i < sticksRequired; i++)
            {
                inventory.UseItem(Item.Stick);
            }
            lit = true;

            healthManager.BeHealed(2, true);
        } else if (inventory.HasItems(Item.Stick, 1))
        {
            // more sticks!

        } else
        {
            // get sticks!
        }
    }

    public bool CanInteract()
    {
        return !lit;
    }
}

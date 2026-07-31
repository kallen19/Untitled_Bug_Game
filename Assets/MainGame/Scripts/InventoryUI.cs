using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum Item {SapPotion, HoneyJar, Stick, DamageBoost}

public class InventoryUI : MonoBehaviour
{
    // gets the IMAGE given the TYPE OF ITEM
    public Image SapBottlePrefab;
    public Image HoneyJarPrefab;
    public Image StickPrefab;
    
    private Dictionary<Item, Image> itemPrefabLookup = new Dictionary<Item, Image>();

    private List<Image> images = new List<Image>(); // keeps track of current list of images shown
    private List<Item> items = new List<Item>(); // keeps track of current items

    [SerializeField] private HealthManager healthManager;
    private KeyCode usePotionKeycode;

    //private Player_Controller pc;
    
    void Start()
    {
        usePotionKeycode = GameObject.Find("Keycodes").GetComponent<Keycodes>().usePotionKey;
        
        itemPrefabLookup.Add(Item.SapPotion, SapBottlePrefab);
        itemPrefabLookup.Add(Item.HoneyJar, HoneyJarPrefab);
        itemPrefabLookup.Add(Item.Stick, StickPrefab);
        healthManager.died.AddListener(clearPotions);
        
        //pc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(usePotionKeycode))
        {
            UseItem(new Item[] {Item.HoneyJar, Item.SapPotion});
        }
    }

    public void UpdatePotionImages()
    {
        SetPotionImages(items);
    }

    public void clearPotions()
    {
        SetPotionImages(new List<Item>());
    }

    public void SetPotionImages(List<Item> itemList)
    {
        // clear current images
        foreach(Image image in images)
        {
            Destroy(image.gameObject);
        }   

        // clear list of current images
        images.Clear();
        
        // instantiate new image and add to images from item list 
        for(int i = 0; i < itemList.Count; i++)
        {
            Image newImage = Instantiate(itemPrefabLookup[itemList[i]], transform);
            images.Add(newImage);
        }

        items = itemList;
    }

    public void PickUp(Item item)
    {
        if (item == Item.DamageBoost)
        {
            healthManager.increaseDamage();
            return;
        }
        
        items.Add(item);
        UpdatePotionImages();
    }

    // no prams: use first object in list
    public void UseItem()
    {
        if(items.Count > 0)
        {
            UseItem(items[0]);
        }
    }

    // use object of a type
    public void UseItem(Item itemType)
    {
        UseItem(new [] {itemType});
    }
    
    public void UseItem(Item[] itemTypes) 
    {
        for(int i = 0; i < items.Count; i++)
        {
            foreach (Item itemType in itemTypes)
            {
                if(items[i] == itemType)
                {
                    // do the thing the item does
                    ActivateItem(itemType);

                    // remove from list
                    items.RemoveAt(i);
                
                    // update
                    UpdatePotionImages();

                    return;
                }
            }
            
        }
    }

    public void ActivateItem(Item itemType)
    {
        Debug.Log("itemType is " + itemType.ToString());
        switch(itemType)
        {
            case Item.SapPotion:
                healthManager.BeHealed(1);
            break;
            case Item.HoneyJar:
                healthManager.BeHealed(1, true);
            break;
        }
    }

    // int: use nth object in list
    public void UseItem(int index)
    {
        if(items.Count > index)
        {
            UseItem(items[index]);
        }
    }

    public int CountItems(Item itemType)
    {
        int itemCount = 0;
        foreach (Item itemInInv in items)
        {
            if (itemInInv == itemType)
            {
                itemCount++;
            }
        }

        return itemCount;
    }

    public bool HasItems(Item itemType, int count)
    {
        return CountItems(itemType) >= count;
    }
    
}

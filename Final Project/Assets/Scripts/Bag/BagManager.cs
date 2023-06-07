using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    private BagDataLoader dataLoader;
    public GameObject Prefab; // for test case usage

    private void Awake()
    {
        dataLoader = new BagDataLoader();

        dataLoader.itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        dataLoader.weaponSlots = GameObject.FindGameObjectsWithTag("WeaponSlot");


        // GetData(); // for test case usage
    }
    
    public void useItem(Transform parent)
    {
        var childCount = parent.childCount;
        if (childCount > 0)
        {
            var child = parent.GetChild(0).gameObject;

            Item item = new Item()
            {
                Id = 1,
                prefab = child
            };

            Debug.Log(dataLoader == null);

            if(parent.CompareTag("ItemSlot"))
            {
                dataLoader.RemoveBagItems(item, false);
            }

            if(parent.CompareTag("WeaponSlot"))
            {
                dataLoader.RemoveWeapon(item);
            }
        }
    }

    /// <summary>
    /// Retrive data from storage, save to bagItems
    /// (Prefab requires having Rect Transform, Canvas Renderer, Image components)
    /// </summary>
    public void GetData(Item item)
    {
        dataLoader.AddBagItems(item);
        // dataLoader.AddBagItems(new Item() { Id = 1, prefab = Instantiate(Prefab) });
        // dataLoader.AddBagItems(new Item() { Id = 1, prefab = Instantiate(Prefab) });
        // dataLoader.AddBagItems(new Item() { Id = 1, prefab = Instantiate(Prefab) });
        // dataLoader.AddBagItems(new Item() { Id = 1, prefab = Instantiate(Prefab) });
        
        // dataLoader.AddWeapon(new Item() { Id = 1, prefab = Instantiate(Prefab) });
    }
}

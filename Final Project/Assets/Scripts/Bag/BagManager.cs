using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    private BagDataLoader dataLoader;

    private void Awake()
    {
        dataLoader = new BagDataLoader();

        dataLoader.itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        dataLoader.weaponSlots = GameObject.FindGameObjectsWithTag("WeaponSlot");
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

    public GameObject LaunchWeapon(int index)
    {
        if (dataLoader.weapons.Count > index)
            return dataLoader.weapons[index].prefab;
        return null;
    }

    /// <summary>
    /// Retrive data from storage, save to bagItems
    /// (Prefab requires having Rect Transform, Canvas Renderer, Image components)
    /// </summary>
    public void AddItem(Item item)
    {
        dataLoader.AddBagItems(item);
    }
}

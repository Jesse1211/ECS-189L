using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;
using System.Reflection;


namespace Project
{
    public class BagDataLoader : MonoBehaviour
    {
        public BagDataLoader dataLoader;
        public List<Item> bagItems = new List<Item>();
        public List<Item> weapons = new List<Item>();
        public GameObject[] itemSlots;
        public GameObject[] weaponSlots;
        public GameObject player;

        public void AddBagItems(Item item)
        {
            bagItems.Add(item);

            // update loaction
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.transform.childCount > 0)
                {
                    continue;
                }
                else
                {
                    GameObject gameObject = bagItems.Last().prefab;
                    bagItems.Last().parent = itemSlot.transform; 

                    gameObject.transform.SetParent(itemSlot.transform);
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    gameObject.GetComponent<RectTransform>().localScale = new Vector2(90, 40);
                    break;
                }
            }
        }

        // todo: 把这个物品后面的东西都向前移动一个格子
        public void RemoveBagItems(Item item, bool isUsed)
        {

            bagItems.Remove(bagItems.Where(x => x.Id == item.Id).First());

            if (isUsed)
            {
                // funtion 可以加血的
                if (item.prefab.name == "GreenApplePickup")
                    player.GetComponent<PlayerControllerAnimator>().HP.GetComponent<Slider>().value += 10;
                if (item.prefab.name == "ApplePickup")
                    player.GetComponent<PlayerControllerAnimator>().HP.GetComponent<Slider>().value += 1;
                Destroy(item.prefab);
            }
            else
            {
                AddWeapon(item);
            }
        }

        // todo: 把这个物品后面的东西都向前移动一个格子
        public void RemoveWeapon(Item item)
        {
            if (weapons.Count() > 0) 
            {
                Debug.Log(weapons.Count());
                weapons.Remove(weapons.Where(x => x.Id == item.Id).First());
                AddBagItems(item);
            }
        }

        public void AddWeapon(Item item)
        {
            weapons.Add(item);

            if (weapons.Count > 4)
            {
                AddBagItems(weapons.First());
                weapons.Remove(weapons.First());
            }
            
            foreach (var itemSlot in weaponSlots)
            {
                if (itemSlot.transform.childCount > 0)
                {
                    continue;
                }
                else
                {
                    GameObject gameObject = weapons.Last().prefab;
                    weapons.Last().parent = itemSlot.transform;

                    gameObject.transform.SetParent(itemSlot.transform);
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    gameObject.GetComponent<RectTransform>().localScale = new Vector2(175, 70);
                    break;
                }
            }
            
        }
    }

}



/*
/// <summary>
/// Update Bag Pannel from bagItems
/// </summary>
public void UpdateBagItem()
{
    if (bagItems.Count > 0)
    {
        int index = 0;
        foreach (var itemSlot in itemSlots)
        {
            if (itemSlot.transform.childCount > 0)
            {
                Destroy(itemSlot.transform.GetChild(0).gameObject);
            }

            if ((bagItems.Count <= index) || (bagItems[index] is null) || (bagItems[index].prefab is null))
            {
                continue;
            }

            GameObject gameObject = Instantiate(bagItems[index].prefab);

            bagItems[index++].parent = itemSlot.transform;

            gameObject.transform.SetParent(itemSlot.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

            gameObject.GetComponent<Image>().enabled = true;
        }
    }
}

public void UpdateWeaponList()
{
    if (weapons.Count > 0)
    {
        int index = 0;

        foreach (var weaponSlot in weaponSlots)
        {
            if (weaponSlot.transform.childCount > 0)
            {
                if (weaponSlot.transform.childCount > 0)
                {
                    Destroy(weaponSlot.transform.GetChild(0).gameObject);
                }
            }

            if ((weapons.Count <= index) || (weapons[index] is null) || (weapons[index].prefab is null) || (weapons[index].prefab.gameObject is null))
            {
                continue;
            }

            weapons[index].parent = weaponSlot.transform;

            GameObject gameObject = Instantiate(weapons[index].prefab);
            gameObject.transform.SetParent(weaponSlot.transform);
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

            gameObject.GetComponent<Image>().enabled = true;

            index++;
        }
    }
}
*/

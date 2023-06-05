using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;


namespace Project
{
    public class BagDataLoader : MonoBehaviour
    {
        public static BagDataLoader dataLoader;
        public static List<Item> bagItems = new List<Item>();

        public static List<Item> weapons = new List<Item>();
        private static int size = 4;

        public GameObject Prefab; // for test case usage

        private GameObject[] itemSlots;
        private GameObject[] weaponSlots;

        // private List<GameObject> weaponSlots = new List<GameObject>();


        //public TextAsset itemData;

        void Awake()
        {
            dataLoader = this;

            GetData();

            itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
            weaponSlots = GameObject.FindGameObjectsWithTag("WeaponSlot");
            // foreach (var index in Enumerable.Range(0, GameObject.FindGameObjectWithTag("WeaponSlot").transform.childCount))
            // {
            //     weaponSlots.Add(GameObject.FindGameObjectWithTag("WeaponSlot").transform.GetChild(index).gameObject);
            // }
        }

        void Update()
        {

        }

        /// <summary>
        /// Retrive data from storage, save to bagItems
        /// (Prefab requires having Rect Transform, Canvas Renderer, Image components)
        /// </summary>
        private void GetData()
        {
            //itemData = Resources.Load("/Resources/data.txt") as TextAsset;
            //items = itemData.text.Split('\n');
            //foreach (var item in items)
            //{
            //    string[] itemData = item.Split(',');
            //    bagItems.Add(new Items() { Id = Convert.ToInt32(itemData[0]), Name = itemData[1] });
            //}
            bagItems.Add(new Item() { prefab = Prefab });
            bagItems.Add(new Item() { prefab = Prefab });
            bagItems.Add(new Item() { prefab = Prefab });
            bagItems.Add(new Item() { prefab = Prefab });
            bagItems.Add(new Item() { prefab = Prefab });

            weapons.Add(new Item() { prefab = Prefab });

        }

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
                }
            }
        }

        public void UpdateWeaponList()
        {
            Debug.Log("!!!!WEAPON counttts:  " + weapons.Count);
            // Debug.Log("weaponSlots COUNT: " + weaponSlots.Length);
            // if (weaponSlots.Length > 0)
            // {
            //     foreach (var weaponSlot in weaponSlots)
            //     {
            //         Debug.Log("weaponSlot:" + weaponSlot);
            //     }
            // }
            if (weapons.Count > 0)
            {
                int index = 0;
                
                // Debug.Log("weaponSlots COUNT: " + weaponSlots.Length.ToString());

                foreach (var weaponSlot in weaponSlots)
                {
                    if (weaponSlot.transform.childCount > 0)
                    {
                        Debug.Log("what is the weaponSlot : " + weaponSlot);
                        Debug.Log("what is the child : " + weaponSlot.transform.GetChild(0));
                        GameObject childObject = weaponSlot.transform.GetChild(0).gameObject;
                        if (childObject != null)
                        {
                            Destroy(childObject);
                        }
                    }

                    if ((weapons.Count <= index) || (weapons[index] is null) || (weapons[index].prefab is null))
                    {
                        continue;
                    }

                    weapons[index].parent = weaponSlot.transform;
                    // Debug.Log("Prefab: " + weapons[index].prefab);

                    GameObject gameObject = Instantiate(weapons[index].prefab);

                    Debug.Log("WHAT IS : gameObject" + gameObject);

                    gameObject.transform.SetParent(weaponSlot.transform);
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    Image imageComponent = gameObject.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        imageComponent.enabled = true;
                    }

                    index++;
                }
            }
        }

        public static void AddBagItems(Item item)
        {
            bagItems.Add(item);
            dataLoader.UpdateBagItem();
        }

        public static void RemoveBagItems(Transform itemSlot)
        {
            bagItems.Remove(bagItems.Where(x => x.parent == itemSlot).First());
            dataLoader.UpdateBagItem();
        }


        public static void RemoveWeapon(GameObject gameObject)
        {
            var weapon = weapons.Where(x => x.prefab == gameObject).First();
            weapons.Remove(weapon);
            BagDataLoader.AddBagItems(weapon);
            dataLoader.UpdateWeaponList();
        }

        public static void AddWeapon(Item item)
        {
            weapons.Add(item);
            Debug.Log("WHAT IS WEAPONS SIZE:  " + weapons.Count);

            if (weapons.Count > size)
            {
                BagDataLoader.AddBagItems(weapons[0]);
                weapons.RemoveAt(0);
                Debug.Log("WEAPONS SIZE after remove:  " + weapons.Count);
            }

            dataLoader.UpdateWeaponList();
        }
    }

}

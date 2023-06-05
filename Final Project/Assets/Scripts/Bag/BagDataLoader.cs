using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;


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

        private List<GameObject> weaponSlots = new List<GameObject>();


        //public TextAsset itemData;

        void Awake()
        {
            dataLoader = this;

            GetData();

            itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");

            foreach (var index in Enumerable.Range(0, GameObject.FindGameObjectWithTag("WeaponSlot").transform.childCount))
            {
                weaponSlots.Add(GameObject.FindGameObjectWithTag("WeaponSlot").transform.GetChild(index).gameObject);
            }
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
            if (weapons.Count > 0)
            {
                int index = 0;
                foreach (var weaponSlot in weaponSlots)
                {
                    if (weaponSlot.transform.childCount > 0)
                    {
                        Destroy(weaponSlot.transform.GetChild(0).gameObject);
                    }

                    if ((weapons.Count <= index) || (weapons[index] is null) || (weapons[index].prefab is null))
                    {
                        continue;
                    }

                    GameObject gameObject = Instantiate(weapons[index].prefab);

                    weapons[index++].parent = weaponSlot.transform;

                    gameObject.transform.SetParent(weaponSlot.transform);
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                }
            }
        }

        public static void AddBagItems(Item item)
        {
            bagItems.Add(item);
        }

        public static void RemoveBagItems(Transform itemSlot)
        {
            bagItems.Remove(bagItems.Where(x => x.parent == itemSlot).First());
            bagItems.Add(new Item { });
        }


        public static void RemoveWeapon(GameObject gameObject)
        {
            var weapon = weapons.Where(x => x.prefab == gameObject).First();
            weapons.Remove(weapon);
            BagDataLoader.AddBagItems(weapon);
        }

        public static void AddWeapon(GameObject gameObject)
        {
            weapons.Add(new Item() { prefab = gameObject });

            if (weapons.Count > size)
            {
                BagDataLoader.AddBagItems(weapons[0]);
                weapons.RemoveAt(0);
            }
        }
    }

}

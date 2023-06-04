using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public List<Items> bagItems = new List<Items>();
    //public List<Items> activeWeapons = new List<Items>();
    public GameObject Prefab; // for test case usage

    private GameObject[] itemSlots;

    //public TextAsset itemData;

    void Start()
    {
        GetData();

        itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
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
        bagItems.Add(new Items() { Id = 1, Name = "name1", prefab = Prefab });
        bagItems.Add(new Items() { Id = 1, Name = "name1", prefab = Prefab });
        bagItems.Add(new Items() { Id = 1, Name = "name1", prefab = Prefab });
        bagItems.Add(new Items() { Id = 1, Name = "name1", prefab = Prefab });
        bagItems.Add(new Items() { Id = 1, Name = "name1", prefab = Prefab });
    }

    /// <summary>
    /// Update Bag Pannel from bagItems
    /// </summary>
    public void UpdateBagItem()
    {
        if (bagItems.Count > 0)
        {
            int index = 0;
            foreach (var item in bagItems)
            {
                if (itemSlots[index].transform.childCount > 0)
                {
                    Destroy(itemSlots[index].transform.GetChild(0).gameObject);
                }
                GameObject gameObject = Instantiate(item.prefab);
                gameObject.transform.SetParent(itemSlots[index++].transform);
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            }
        }
    }

    //private void AddCharacterItems(int id)
    //{
    //    characterItems.Add(bagItems.Where(x => x.Id == id).First());
    //}

    //private void RemoveItems(int id)
    //{
    //    characterItems.Remove(bagItems.Where(x => x.Id == id).First());
    //}
}

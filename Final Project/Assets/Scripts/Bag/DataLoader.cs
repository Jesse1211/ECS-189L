using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader dataLoader;
    public List<Items> bagItems = new List<Items>();
    public List<Items> characterItems = new List<Items>();
    public TextAsset itemData;
    public string[] items;

    void Start()
    {
        dataLoader = this;
        itemData = Resources.Load("/Resources/data.txt") as TextAsset;
        items = itemData.text.Split('\n');
        foreach (var item in items)
        {
            string[] itemData = item.Split(',');
            bagItems.Add(new Items() { Id = Convert.ToInt32(itemData[0]), Name = itemData[1] });
        }
    }

    void Update()
    {
        
    }

    private void AddCharacterItems(int id)
    {
        characterItems.Add(bagItems.Where(x => x.Id == id).First());
    }

    private void RemoveItems(int id)
    {
        characterItems.Remove(bagItems.Where(x => x.Id == id).First());
    }
}

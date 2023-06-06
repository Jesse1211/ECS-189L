using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Id;
    public string? Name;
    public Types? type;
    public Transform parent;
    public GameObject prefab;

    public enum Types
    {
        Medicine,
        Meat,
        Weapon,
        Money
    }
}

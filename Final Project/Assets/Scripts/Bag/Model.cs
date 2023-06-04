using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public int Id;
    public string Name;
    public Types? type;
    public GameObject prefab;

    public enum Types
    {
        Medicine,
        Meat,
        Weapon,
        Money
    }
}

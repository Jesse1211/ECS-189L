using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public int Id;
    public string Name;
    public string? Description;
    public Types? type;

    public enum Types
    {
        Medicine,
        Meat,
        Weapon,
        Money
    }
}

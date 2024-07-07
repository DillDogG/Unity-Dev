using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] Item[] items;
    public Item currentItem { get; private set; }

    void Start()
    {
        currentItem = items[0];
        currentItem.Equip();
    }

    public void Use()
    {
        currentItem?.Use();
    }

    public void StopUse()
    {
        currentItem?.StopUse();
    }

    public void SwapCurrent()
    {
        if (currentItem == items[items.Length - 1])
        {
            currentItem.Unequip();
            currentItem = items[0];
            currentItem.Equip();
            return;
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == currentItem)
            {
                currentItem.Unequip();
                currentItem = items[++i];
                currentItem.Equip();
                return;
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class Inventory : MonoBehaviour
{
    protected Slot[] slotList;

    //public static Inventory Instance;
    
        
    public void Start()
    {
        //把所有的孩子放到这个数组里
        slotList = GetComponentsInChildren<Slot>();
        //Instance = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物品的id不存在");
            return false;
        }
        if (item.Capacity == 1) //容量是1，本身只能找空槽位
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);//把物品存储到这个空的物品槽里面
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 这个方法用来找到一个空的物品槽
    /// </summary>
    /// <returns></returns>
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }


    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            //挂载的孩子有一个（只能有一个） && 孩子item的类型和当前item类型一样&&槽位没有满
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    public void TestInventory()
    {
        Debug.Log("TestInventory()可以");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,IPointerClickHandler
{
    public GameObject itemPrefab;
    public GameObject pickedItem;
    

    /// <summary>
    /// 储存物品
    /// </summary>
    /// <param name="item"></param>
    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            //学习下这块语法
            GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
            //设置父对象
            itemGameObject.transform.SetParent(this.transform);
            //设置大小和位置
            itemGameObject.transform.localScale = Vector3.one;
            itemGameObject.transform.localPosition = Vector3.zero;
            //获得itemui脚本里的方法
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            //默认添加1个
            transform.GetChild(0).GetComponent<ItemUI>().ChangeAmount();
        }
    }

    /// <summary>
    /// 得到当前物品槽存储的物品类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }

    /// <summary>
    /// 得到物品的id
    /// </summary>
    /// <returns></returns>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }
    
    /// <summary>
    /// 槽位是否已满
    /// </summary>
    /// <returns></returns>
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;//当前的数量大于等于容量
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string content = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ToolTipShow(content);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.childCount > 0)
        {
            InventoryManager.Instance.ToolTipHide();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pickedItem = GameObject.Find("PickedItem");

        if(transform.GetChild(0) == null)//Slot为空
        {
            if (InventoryManager.Instance.IsPickedItem)//有东西
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    //只放一个item
                    InventoryManager.Instance.PickedItem_ShowOrHide(true, -1); //pickedItem -1;
                    //Slot生成物体
                    slo
                }
                else
                {
                    //item全放到slot里

                }
            }
        }
        else//Slot不为空
        {

        }



    }
}

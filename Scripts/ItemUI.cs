using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    #region Data
    public Item Item { get; set; }
    public int Amount { get; set; }
    #endregion

    #region UI Component
    private Image itemImage;
    private Text textAmount;

    public Image ItemImage
    {
        get
        {
            if(itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }

    private Text TextAmount
    {
        get
        {
            if(textAmount == null)
            {
                textAmount = GetComponentInChildren<Text>();
            }
            return textAmount;
        }
    }
    #endregion


    public void SetItem(Item item, int amount = 1)
    {
        this.Item = item;
        this.Amount = amount;

        //ItemImage.sprite = item.sprite; 我的天，就不能这么写，记住下面
        //大小写也分清楚
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        TextAmount.text = amount.ToString();
    }

    /// <summary>
    /// 增加默认为1个
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeAmount(int amount = 1)
    {
        //Amount++;
        this.Amount += amount;
        TextAmount.text = Amount.ToString();
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void ShowOrHide(bool showOrHide)
    {
        //不是 this.SetActive
        gameObject.SetActive(showOrHide);
    }


}

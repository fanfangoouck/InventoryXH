using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Item;

public class InventoryManager : MonoBehaviour
{
    #region 单例模式
    private static InventoryManager _instance;

    //引用InventoryManager脚本的实例
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //静态变量 只能引用静态方法
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }

    }
    #endregion


    /// <summary>
    ///  物品信息的列表（集合）
    /// </summary>
    private List<Item> itemList;

    private ToolTip toolTip;
    private bool isToolTip = false;
    private RectTransform rectTransform;
    //private Canvas canvas;
    //针对item最合适的
    //private Vector2 offset = new Vector2(560, 300);
    //对于消耗品、武器类等等，展示格式不同，上面那个太大了
    private Vector2 offset = new Vector2(500, 230);

    //pickeditem相关
    private ItemUI pickedItem;
    private bool isPickedItem = false;//初始值为false

    public bool IsPickedItem //开放权限，为了其他脚本能访问到InventoryManager.Instance.IsPickedItem
    {
        get
        {
            return IsPickedItem;
        }
    }



    void ParseItemJson()
    {
        //文本为在Unity里面是 TextAsset类型
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("item"); //加载json文件
        string itemsJson = itemText.text;//物品信息的Json格式
        JSONObject j = new JSONObject(itemsJson); //传入集成的json方法

        foreach (JSONObject temp in j.list)
        {

            string typeStr = temp["type"].stringValue;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);

            int id = temp["id"].intValue;
            string name = temp["name"].stringValue;
            Item.ItemQuality quality = (Item.ItemQuality)System.Enum.Parse(typeof(Item.ItemQuality), temp["quality"].stringValue);
            string description = temp["description"].stringValue;
            int capacity = temp["capacity"].intValue;
            int buyPrice = temp["buyPrice"].intValue;
            int sellPrice = temp["sellPrice"].intValue;
            string sprite = temp["sprite"].stringValue;

            Item item = null;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = temp["hp"].intValue;
                    int mp = temp["mp"].intValue;
                    item = new Consumable(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;

                case Item.ItemType.Equipment:
                    int strength = temp["strength"].intValue;
                    int intellect = temp["intellect"].intValue;
                    int agility = temp["agility"].intValue;
                    int stamina = temp["stamina"].intValue;
                    Equipment.EquipmentType equipType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipType"].stringValue);
                    item = new Equipment(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, strength, intellect,agility,stamina, equipType);
                    break;

                case Item.ItemType.Weapon:
                    int damage = temp["damage"].intValue;
                    Weapon.WeaponType wpType = (Weapon.WeaponType)System.Enum.Parse
                        (typeof(Weapon.WeaponType),temp["weaponType"].stringValue);
                    item = new Weapon(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, wpType);
                    break;

                case Item.ItemType.Material:
                    //
                    item = new Material(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite);
                    break;
            }
            itemList.Add(item);
            // 改写tostring方法后，debug会自动调用。
            //之前只输出item的类 consumable;
            Debug.Log("item is " + item);



        }

    }
    public Item GetItemById(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }



    /// <summary>
    /// ToolTip显示
    /// </summary>
    public void ToolTipShow(string content)
    {
        isToolTip = true;
        toolTip.Show(content);
    }

    /// <summary>
    /// ToolTip隐藏
    /// </summary>
    public void ToolTipHide()
    {
        isToolTip = false;
        toolTip.Hide();
    }

    //pickeditem展示
    public void PickedItem_ShowOrHide(bool showOrHide, int amount)
    {
        pickedItem.ShowOrHide(showOrHide);
        isPickedItem = showOrHide;
        pickedItem.ChangeAmount(amount);

        if (showOrHide)
        {
            ToolTipHide(); //Tips不能展示
        }
    }

    //获得pickedItem的数量
    public int PickedItem_GetAmount()
    {
        return pickedItem.Amount;
    }


    void Start()
    {
        ParseItemJson();
        toolTip = GameObject.FindObjectOfType<ToolTip>();

        //private RectTransform rectTransform;
        rectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        //老师是写的这个方法，然后后面又把canvas.transform as rectTransform, 类型转换了一下
        //private Canvas canvas;
        //canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        // rectTransform = GameObject.Find("Canvas").GetComponent<Canvas>();

        //pickeditem相关
        //获得它的脚本
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();

    }

void Update()
    {
        Vector2 position;

        
        if (isToolTip)
        {
            //获得鼠标的位置
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,
            Input.mousePosition, null, out position);

            toolTip.SetToolTipPosition(position + offset);
        }
          

          


        
        /*
        //if (isToolTipShow)
       // {
            //控制提示面板跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toolTip.SetToolTipPosition(position);
       // }
       */

    }


}

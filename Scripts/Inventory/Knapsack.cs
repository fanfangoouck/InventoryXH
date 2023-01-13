using UnityEngine;
using System.Collections;

public class Knapsack : Inventory
{
    /*
    #region 单例模式
    private static Knapsack _instance;
    public static Knapsack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
            }
            return _instance;
        }
    }
    #endregion
    */
    
    
    public static Knapsack Instance;
    public  void Awake()
    {
        Instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
    }
    
}

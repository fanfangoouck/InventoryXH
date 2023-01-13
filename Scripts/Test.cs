using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1,18);
            Debug.Log("id 是" + id);
            //Debug.Log("Knapsack.Instance" + Knapsack.Instance);
            Knapsack.Instance.StoreItem(id);
            //Inventory.Instance.TestInventory();
            //Inventory.Instance.StoreItem(id);
        }
    }
}

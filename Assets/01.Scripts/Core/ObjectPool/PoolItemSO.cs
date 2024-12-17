using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Pool/Item")]
public class PoolItemSO : ScriptableObject
{
    public string poolName;
    public GameObject prefab;
    public int count;

    private void OnValidate()
    {
        if(prefab != null)
        {
            IPoolable item  = prefab.GetComponent<IPoolable>();
            if(item == null)
            {
                Debug.LogWarning($"Cant find Ipoolable script on prefabs : check!");
                prefab = null;
            }
            else
            {
                poolName = item.PoolName;
            }
        }
    }
}
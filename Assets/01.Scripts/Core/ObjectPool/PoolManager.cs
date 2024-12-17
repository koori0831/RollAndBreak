using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private PoolListSO _poolList;

    private Dictionary<string, Pool> _pools;

    private void Awake()
    {
        _pools = new Dictionary<string, Pool>();
        foreach (PoolItemSO pair in _poolList.list)
        {
            CreatPool(pair);
        }
    }

    private void CreatPool(PoolItemSO pair)
    {
        IPoolable poolable = pair.prefab.GetComponent<IPoolable>();
        if (poolable == null)
        {
            Debug.LogWarning($"GameObject {pair.prefab.name} has no Ipoolabale Script");
            return;
        }

        Pool pool = new Pool(poolable, transform, pair.count);
        _pools.Add(poolable.PoolName, pool);
    }

    public IPoolable Pop(string itemName)
    {
        if (_pools.ContainsKey(itemName))
        {
            IPoolable item = _pools[itemName].Pop();
            item.ResetItem();
            return item;
        }
        Debug.LogError($"There is no pool{itemName}");
        return null;
    }

    public void Push(IPoolable item)
    {
        if (_pools.ContainsKey(item.PoolName))
        {
            _pools[item.PoolName].Push(item);
            return;
        }
        Debug.LogError($"There is no pool{item.PoolName}");
    }
}

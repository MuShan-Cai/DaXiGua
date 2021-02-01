using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FruitFactory")]
public class FruitFactory : ScriptableObject
{
    [SerializeField]
    private Fruit[] fruitPrefabs;

    private List<Fruit>[] fruitsPool;

    void CreatePool()
    {
        fruitsPool = new List<Fruit>[fruitPrefabs.Length];
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            fruitsPool[i] = new List<Fruit>();
        }
    }

    public Fruit GetFruit(int id)
    {
        if(fruitsPool == null)
        {
            CreatePool();
        }

        if(id < fruitsPool.Length)
        {
            List<Fruit> pool = fruitsPool[id];
            var fruit = pool.Count > 0 ? pool[0] : null;
            if (fruit == null)
            {
                fruit = Instantiate(fruitPrefabs[id]);
                fruit.ID = id;
                fruit.OriFruitFactory = this;
            }
            else
            {
                pool.RemoveAt(0);
                fruit.gameObject.SetActive(true);
            }
            return fruit;
        }
        return null;
    }

    public Fruit GetRandomFruit()
    {
        int randomValue = Random.Range(0, fruitPrefabs.Length);
        return GetFruit(randomValue);
    }

    public void Recycle(Fruit fruit)
    {
        var pool = fruitsPool[fruit.ID];
        pool.Add(fruit);
        fruit.gameObject.SetActive(false);
    }

}

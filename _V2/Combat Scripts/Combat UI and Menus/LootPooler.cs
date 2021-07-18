using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    //[SerializeField]
    //private AudioClip coinSFX;
    //private AudioSource LootSFX;
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    void Start()
    {
        //LootSFX = gameObject.GetComponent<AudioSource>();
        //LootSFX.clip = coinSFX;
        onParticleLeaveScreen.particleLeftScreen += RecycleParticle;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i= 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " not found.");
            return null;
        };
        if(poolDictionary[tag].Count > 0)
        {
            GameObject objToSpawn = poolDictionary[tag].Dequeue();
            objToSpawn.SetActive(true);
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;
            //LootSFX.Play();
            return objToSpawn;
        }
        else
        {
            GameObject spawnObject;
            for (int i = 0; i< pools.Count; i++)
            {
                if(pools[i].tag == tag)
                {
                    spawnObject = Instantiate(pools[i].prefab);
                    spawnObject.SetActive(false);
                    poolDictionary[tag].Enqueue(spawnObject);                    
                }
            }
            GameObject objToSpawn = poolDictionary[tag].Dequeue();
            objToSpawn.SetActive(true);
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;
            //LootSFX.Play();
            return objToSpawn;
        }
        //poolDictionary[tag].Enqueue(objToSpawn);
    }

    private void RecycleParticle(GameObject go)
    {
        poolDictionary[go.name].Enqueue(go);
    }

    private void OnDisable()
    {
        onParticleLeaveScreen.particleLeftScreen -= RecycleParticle;
    }
}

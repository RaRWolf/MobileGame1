﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    //This script inspired by the tutorial found here: https://www.raywenderlich.com/847-object-pooling-in-unity

    public static EnemyPooling SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }

    public List<GameObject> pooledGunmen;
    public GameObject gunmanToPool;
    public int gunAmountToPool;

    public List<GameObject> pooledRunmen;
    public GameObject runmanToPool;
    public int runAmountToPool;

    public List<GameObject> pooledAreas;
    public GameObject areaToPool;
    public int areaAmountToPool;

    public List<GameObject> pooledSections;
    public GameObject[] sectionsToPool;
    public int sectionAmountToPool;


    // Start is called before the first frame update
    void Start()
    {
        pooledGunmen = new List<GameObject>();
        for (int i = 0; i < gunAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(gunmanToPool);
            obj.SetActive(false);
            pooledGunmen.Add(obj);
        }


        pooledRunmen = new List<GameObject>();
        for (int i = 0; i < runAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(runmanToPool);
            obj.SetActive(false);
            pooledRunmen.Add(obj);
        }

        pooledAreas = new List<GameObject>();
        for (int i = 0; i < areaAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(areaToPool);
            obj.SetActive(false);
            pooledAreas.Add(obj);
        }


        pooledSections = new List<GameObject>();
        for (int i = 0; i < sectionAmountToPool; i++)
        {
            for(int y = 0; y < sectionsToPool.Length; y++)
            {
                GameObject obj = (GameObject)Instantiate(sectionsToPool[y]);
                StartCoroutine("WaitOne",obj);
            }
        }
    }

    public GameObject GetPooledGunman()
    {
        for (int i = 0; i < pooledGunmen.Count; i++)
        {
            if (!pooledGunmen[i].activeInHierarchy)
            {
                return pooledGunmen[i];
            }
        }
        return null;
    }

    public GameObject GetPooledRunman()
    {
        for (int i = 0; i < pooledRunmen.Count; i++)
        {
            if (!pooledRunmen[i].activeInHierarchy)
            {
                return pooledRunmen[i];
            }
        }
        return null;
    }

    public GameObject GetPooledArea()
    {
        Debug.Log("Poolercalled");
        for (int i = -1; i < pooledAreas.Count; i++)
        {
            Debug.Log("Searching");
            int x = Random.Range(0, pooledAreas.Count);
            Debug.Log("X is equal to:" + x);
            if (!pooledAreas[x].activeInHierarchy)
            {
                return pooledAreas[x];
            }
        }
        Debug.Log("No area found");
        return null;
    }

    public GameObject GetPooledSection()
    {
        for (int i = -1; i < pooledSections.Count; i++)
        {
            int x = Random.Range(0, pooledSections.Count);
            Debug.Log("X is equal to:" + x);
            if (!pooledSections[x].activeInHierarchy)
            {
                return pooledSections[x];
            }
        }
        return null;
    }

    IEnumerator WaitOne(GameObject obj)
    {
        yield return new WaitForSeconds(1);

        Debug.Log("No longer active");
        obj.SetActive(false);

        foreach (Transform human in obj.transform)
        {
            if (human.tag == "Human")
            {
                human.parent = null;
                human.gameObject.SetActive(false);
            }
        }

        foreach (Transform section in obj.transform)
        {
            if (section.tag == "Section")
            {
                section.parent = null;
                section.gameObject.SetActive(false);
            }
        }

        GameObject[] humansArray = GameObject.FindGameObjectsWithTag("Human");
       foreach (GameObject human in humansArray)
        {
            Transform humanT = human.transform;
            humanT.parent = null;
            humanT.gameObject.SetActive(false);
        }

        pooledSections.Add(obj);
    }
}
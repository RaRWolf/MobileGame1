using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGenerator : MonoBehaviour
{

    public GameObject[] zonePrefabs;
    public GameObject zoneWalls;
    public GameObject[] areaGenList;

    public LevelGenerator levelGenerator;

    private GameObject wallColliderPrefab;
    private Collider[] wallColliders;

    private GameObject emptySpot;
    public bool isASpot = false;
    public bool inPlace = false;


    void OnEnable()
    {
        //Find the level generator
        levelGenerator = FindObjectsOfType<LevelGenerator>()[0];
        StartCoroutine("LateStart");
        StartCoroutine("SpawnArea");
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);

        //Find out if there's an empty space. If there is, spawn the new tile.
        for (var i = 0; i < levelGenerator.currentAreas.Count; i++)
        {
            if (levelGenerator.currentAreas[i] == null && !isASpot)
            {
                isASpot = true;
            }
        }

        if (isASpot == false)
        {
            levelGenerator.spawning = false;
            levelGenerator.spawning2 = false;

            foreach (Transform section in transform)
            {
                if (section.tag == "Section")
                {
                    section.parent = null;
                    section.gameObject.SetActive(false);
                }
            }

            foreach (Transform walls in transform)
            {
                if (walls.tag == "Exit")
                {
                    Destroy(walls.gameObject);
                }
            }

            gameObject.SetActive(false);
        }

        if (isASpot && !inPlace)
        {
            StartCoroutine("SpawnArea");
        }
    }

    IEnumerator SpawnArea()
    {
        yield return new WaitForSeconds(0.1f);
        if (isASpot && !inPlace)
        {
            //Instantiate all of the camera-detecting walls, then spawn in random tiles in a 9x9 pattern.
            //Middle
            wallColliderPrefab = Instantiate(zoneWalls, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);


            GameObject m = EnemyPooling.SharedInstance.GetPooledSection();
            if (m != null)
            {
                m.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                m.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                m.SetActive(true);
            }

            //var thing1 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));



            //North
            //var thing2 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x + 0, 0, transform.position.z + 50), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject n = EnemyPooling.SharedInstance.GetPooledSection();
            if (n != null)
            {
                n.transform.position = new Vector3(transform.position.x + 0, 0, transform.position.z + 50);
                n.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                n.SetActive(true);
            }

            //North-East
            //var thing3 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x + 50, 0, transform.position.z + 50), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject ne = EnemyPooling.SharedInstance.GetPooledSection();
            if (ne != null)
            {
                ne.transform.position = new Vector3(transform.position.x + 50, 0, transform.position.z + 50);
                ne.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                ne.SetActive(true);
            }

            //East
            //var thing4 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x + 50, 0, transform.position.z + 0), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject e = EnemyPooling.SharedInstance.GetPooledSection();
            if (e != null)
            {
                e.transform.position = new Vector3(transform.position.x + 50, 0, transform.position.z + 0);
                e.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                e.SetActive(true);
            }

            //South-east
            //var thing5 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x + 50, 0, transform.position.z - 50), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject se = EnemyPooling.SharedInstance.GetPooledSection();
            if (se != null)
            {
                se.transform.position = new Vector3(transform.position.x + 50, 0, transform.position.z - 50);
                se.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                se.SetActive(true);
            }

            //South
            //var thing6 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x + 0, 0, transform.position.z - 50), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject s = EnemyPooling.SharedInstance.GetPooledSection();
            if (s != null)
            {
                s.transform.position = new Vector3(transform.position.x + 0, 0, transform.position.z - 50);
                s.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                s.SetActive(true);
            }

            //South-west
            //var thing7 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x - 50, 0, transform.position.z - 50), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject sw = EnemyPooling.SharedInstance.GetPooledSection();
            if (sw != null)
            {
                sw.transform.position = new Vector3(transform.position.x - 50, 0, transform.position.z - 50);
                sw.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                sw.SetActive(true);
            }

            //West
            //var thing8 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x - 50, 0, transform.position.z + 0), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject w = EnemyPooling.SharedInstance.GetPooledSection();
            if (w != null)
            {
                w.transform.position = new Vector3(transform.position.x - 50, 0, transform.position.z + 0);
                w.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                w.SetActive(true);
            }

            //North-west
            //var thing9 = Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Length)], new Vector3(transform.position.x - 50, 0, transform.position.z + 50), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));

            GameObject nw = EnemyPooling.SharedInstance.GetPooledSection();
            if (nw != null)
            {
                nw.transform.position = new Vector3(transform.position.x - 50, 0, transform.position.z + 50);
                nw.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                nw.SetActive(true);
            }


            wallColliderPrefab.transform.parent = gameObject.transform;
            m.transform.parent = gameObject.transform;
            n.transform.parent = gameObject.transform;
            ne.transform.parent = gameObject.transform;
            e.transform.parent = gameObject.transform;
            se.transform.parent = gameObject.transform;
            s.transform.parent = gameObject.transform;
            sw.transform.parent = gameObject.transform;
            w.transform.parent = gameObject.transform;
            nw.transform.parent = gameObject.transform;



            //Place this area into the first empty spot in the list.
            for (var y = 0; y < levelGenerator.currentAreas.Count; y++)
            {
                if (levelGenerator.currentAreas[y] == null && !inPlace)
                {
                    levelGenerator.currentAreas[y] = gameObject;
                    inPlace = true;
                    isASpot = false;
                    levelGenerator.spawning2 = false;
                }
            }
        }
    }
}

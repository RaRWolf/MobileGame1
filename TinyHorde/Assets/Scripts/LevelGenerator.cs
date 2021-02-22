using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject areaGenerator;
    public List<GameObject> currentAreas;
    public GameObject[] currentAreaSpawner;
    public GameObject[] wallColliders;
    public Collider[] actualColliders;
    public int currentAreaAmount = -1;
    public int latestCreatedSpawner = 0;
    public bool spawning = false;
    public bool spawning2 = false;
    public bool deleting = false;

    private Collider other;

    void Start()
    {
        StartCoroutine("WaitOne");
    }

    IEnumerator WaitOne()
    {
        yield return new WaitForSeconds(1);
        SpawnAreaGenerator(0f, 0f);
        Debug.Log("Spawned Area?");
    }


    void Update()
    {
        //This flickers the camera's collider on and off, so that it will always trigger "OnTriggerEnter," even if a trigger spawns and envelops it.
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    //When the camera comes into contact with a camera-detecting wall, it figures out which side of the tile it's exiting and spawns another tile in that direction.
    void OnTriggerEnter(Collider other)
    {
        if (!spawning && !spawning2)
        {
            
            StartCoroutine("LateSpawn", other);
            spawning = true;
            spawning2 = true;
        }

    }

    IEnumerator LateSpawn(Collider other)
    {
        yield return new WaitForSeconds(0.1f);
        if (other.name == "ExitNorth")
        {
            SpawnAreaGenerator(other.transform.position.x, other.transform.position.z + 100f);
        }
        if (other.name == "ExitEast")
        {
            SpawnAreaGenerator(other.transform.position.x + 100f, other.transform.position.z);
        }
        if (other.name == "ExitSouth")
        {
            SpawnAreaGenerator(other.transform.position.x, other.transform.position.z - 100f);
        }
        if (other.name == "ExitWest")
        {
            SpawnAreaGenerator(other.transform.position.x - 100f, other.transform.position.z);
        }
        //Corners
        if (other.name == "ExitNorthEast")
        {
            SpawnAreaGenerator(other.transform.position.x + 100f, other.transform.position.z + 100f);
        }
        if (other.name == "ExitSouthEast")
        {
            SpawnAreaGenerator(other.transform.position.x + 100f, other.transform.position.z - 100f);
        }
        if (other.name == "ExitSouthWest")
        {
            SpawnAreaGenerator(other.transform.position.x - 100f, other.transform.position.z - 100f);
        }
        if (other.name == "ExitNorthWest")
        {
            SpawnAreaGenerator(other.transform.position.x - 100f, other.transform.position.z + 100f);
        }
    }

    //This spawns tiles and tells the system to destroy the farthest away tile if there's more than the currentAreas array.
    void SpawnAreaGenerator(float X,float Z)
    {
        if (currentAreaAmount > currentAreas.Count && !deleting)
        {
            //Find the farthest area, deactivate it.
            DeleteAnArea().SetActive(false);
            currentAreaAmount -= 2;
            deleting = true;
        }
    
            SpawnAnArea(X, Z);
            Debug.Log("Spawning in a new generator");

        wallColliders = GameObject.FindGameObjectsWithTag("Exit");
        foreach(GameObject wallCollider in wallColliders)
        {
            actualColliders = wallCollider.GetComponentsInChildren<Collider>();

            foreach(Collider actualCollider in actualColliders)
            {
                actualCollider.enabled = false;
                actualCollider.enabled = true;
            }

        }
    }

    //This returns the farthest away area, then marks it for deletion in SpawnAreaGenerator
    GameObject DeleteAnArea()
    {
            GameObject farthest = null;
            float distance = Mathf.Infinity * -1;
            Vector3 position = transform.position;

            foreach (GameObject area in currentAreas)
            {
                if(area != null)
                {
                    Vector3 difference = area.transform.position - position;
                    float currentDistance = difference.sqrMagnitude;
                    if (currentDistance > distance)
                    {
                        farthest = area.gameObject;
                        distance = currentDistance;
                    }
                }
                
            }
            //Remove from array
            currentAreas[currentAreas.IndexOf(farthest)] = null;

        foreach(Transform human in farthest.transform)
        {
            if(human.tag == "Human")
            {
                human.parent = null;
                human.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 9; i++)
        {
            foreach (Transform section in farthest.transform)
            {
                if (section.tag == "Section")
                {
                    section.parent = null;
                    section.gameObject.SetActive(false);
                }
            }
        }

        foreach(Transform walls in farthest.transform)
        {
            if(walls.tag == "Exit")
            {
                Destroy(walls.gameObject);
            }
        }

        farthest.gameObject.GetComponent<AreaGenerator>().inPlace = false;



        return farthest;
    }

    void SpawnAnArea(float X, float Z)
    {
        Debug.Log("Attempting to call pooler");
        GameObject area = EnemyPooling.SharedInstance.GetPooledArea();
        if (area != null)
        {
            area.transform.position = new Vector3(X, 0, Z);
            area.transform.rotation = Quaternion.identity;
            area.SetActive(true);
            Debug.Log("Set to active!");
            currentAreaAmount++;
        }
        else
        {
            Debug.Log("Area was null");
        }
    }


}

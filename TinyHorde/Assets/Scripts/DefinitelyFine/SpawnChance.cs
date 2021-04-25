using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChance : MonoBehaviour
{
    public GameObject runMan;
    public GameObject gunMan;

    public float spawnChance;
    public float gunChance;

    private float actualSpawn;
    private float actualGun;

    private GameObject cam;
    private CameraController camScript;

    public MeshRenderer myMeshRenderer;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine("WaitOne");
    }

    IEnumerator WaitOne()
    {
        yield return new WaitForSeconds(1);
        myMeshRenderer.enabled = false;


        actualSpawn = Random.Range(0.0f, 1.0f);
        actualGun = Random.Range(0.0f, 1.0f);

        //If the horde size is over 10, start increasing the gunchance
        if (CameraController.SharedInstance.hordeSizeAfter > 10)
        {
            actualGun -= (CameraController.SharedInstance.hordeSizeAfter / 100f);
            actualSpawn -= (CameraController.SharedInstance.hordeSizeAfter / 100f);
            Debug.Log("Changing gun and spawn chance by:" + (CameraController.SharedInstance.hordeSizeAfter / 100f));
        }

        //If the generated number is less than the current spawn chance, spawn the creature
        if (actualSpawn < spawnChance)
        {
            //If the generated number is less than the gun chance, spawn it with a gun
            if (actualGun < gunChance)
            {

                GameObject gunMan = EnemyPooling.SharedInstance.GetPooledGunman();
                if (gunMan != null)
                {
                    gunMan.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                    gunMan.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    gunMan.SetActive(true);
                    gunMan.transform.SetParent(transform.parent.parent.parent);
                }
            }
            else
            {
                GameObject runMan = EnemyPooling.SharedInstance.GetPooledRunman();
                if (runMan != null)
                {
                    runMan.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                    runMan.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    runMan.SetActive(true);
                    runMan.transform.SetParent(transform.parent.parent.parent);
                }
            }
        }
    }
}

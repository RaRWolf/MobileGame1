using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public static CameraController SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }

    public GameObject[] horde;
    Vector3 hordeCenter;
    Vector3 targetPosition;

    private float cameraHeight;
    public int hordeSizeBefore;
    public int hordeSizeAfter;
    public int hordeSizePeak;
    public int cash;

    public ParticleSystem plusOne;
    public ParticleSystem minusOne;
    public ParticleSystem plusOneCash;

    private bool endingGame = false;


    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = 20;
        horde = GameObject.FindGameObjectsWithTag("zombie");
        cash = DataHolder.cash;
    }

    // Update is called once per frame
    void Update()
    {
        if(DataHolder.hordeSizePeak < horde.Length)
        {
            DataHolder.hordeSizePeak = horde.Length;
        }

        hordeSizeBefore = horde.Length;

        horde = GameObject.FindGameObjectsWithTag("zombie");

        hordeSizeAfter = horde.Length;

        if (hordeSizeBefore < hordeSizeAfter)
        {
            plusOne.Play();
            plusOneCash.Play();
            cash += 1;
        }
        if (hordeSizeBefore > hordeSizeAfter)
        {
            minusOne.Play();
        }


        //If all the zombies are dead
        if (hordeSizeBefore <= 0 && !endingGame)
        {
            StartCoroutine("EndGame");
            endingGame = true;
        }



        if (horde.Length >= 1)
        {
            hordeCenter = FindCenterPoint(horde);

            cameraHeight = hordeCenter.y + (20 + (horde.Length * 0.2f));

            targetPosition = new Vector3(hordeCenter.x, cameraHeight, hordeCenter.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 4f);
        }



    }

    Vector3 FindCenterPoint(GameObject[] horde)
    {
        Vector3 midPoint = new Vector3(0, 0, 0);

        foreach (GameObject z in horde)
        {
            midPoint = midPoint + z.transform.position;
        }

        midPoint = (midPoint / horde.Length);

        return midPoint;

        //return horde[0].transform.position;

    }

    IEnumerator EndGame()
    {
        DataHolder.cash = cash; //Save the cash value
        DataHolder.endGame();
        cash = DataHolder.cash;
        yield return new WaitForSeconds(3); //Wait
        SceneManager.LoadScene(2); //Load the Shop
    }

}
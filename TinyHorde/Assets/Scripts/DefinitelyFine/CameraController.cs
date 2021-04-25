using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public ParticleSystem plusOne;
    public ParticleSystem minusOne;


    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = 20;
        horde = GameObject.FindGameObjectsWithTag("zombie");
    }

    // Update is called once per frame
    void Update()
    {
        hordeSizeBefore = horde.Length;

        horde = GameObject.FindGameObjectsWithTag("zombie");

        hordeSizeAfter = horde.Length;

        if (hordeSizeBefore < hordeSizeAfter)
        {
            plusOne.Play();
        }
        if (hordeSizeBefore > hordeSizeAfter)
        {
            minusOne.Play();
        }



        if(horde.Length >= 1)
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

        foreach(GameObject z in horde)
        {
            midPoint = midPoint + z.transform.position;
        }

        midPoint = (midPoint / horde.Length);

        return midPoint;

            //return horde[0].transform.position;

    }
}

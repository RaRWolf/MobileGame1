using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject[] horde;
    Vector3 hordeCenter;
    Vector3 targetPosition;

    private float cameraHeight;


    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = 20;
    }

    // Update is called once per frame
    void Update()
    {
        horde = GameObject.FindGameObjectsWithTag("zombie");
        hordeCenter = FindCenterPoint(horde);

        cameraHeight = hordeCenter.y + (20 + (horde.Length * 0.2f));

        targetPosition = new Vector3(hordeCenter.x, cameraHeight, hordeCenter.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 4f);

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

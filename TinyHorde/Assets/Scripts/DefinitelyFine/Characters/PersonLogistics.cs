using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonLogistics : MonoBehaviour
{
    private float distance;

    void Start()
    {
    }

    void Update()
    {
        if(transform.position.y < -10f)
        {
            if (gameObject.tag != "zombie")
            {
                    gameObject.SetActive(false);
            }
            else
            {
               
            }
        }
    }
}

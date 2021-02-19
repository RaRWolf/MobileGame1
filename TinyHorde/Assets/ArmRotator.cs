using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotator : MonoBehaviour
{
    public Vector3 stolenTarget;
    private Vector3 v3;
    // Start is called before the first frame update
    void Start()
    {
        v3 = Vector3.zero;
        transform.eulerAngles = v3;
    }

    // Update is called once per frame
    void Update()
    {
        var lookPos = stolenTarget;

        transform.up = (lookPos - transform.position) * -1;

        transform.rotation *= Quaternion.Euler(1, 180, 1);
    }
}

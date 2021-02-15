using UnityEngine;
using System.Collections;
public class ZombieMove : MonoBehaviour
{

    Vector3 newPosition;
    Vector3 newPositionLimited;

    //Vector3 newPositionLimited2;
    public float speed = 1.0f;


    void Start()
    {
        newPosition = transform.position;
    }
    void Update()
    {
        //While the mouse is held
        if (Input.GetMouseButton(0))
        {
            //Send out a raycast
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
            }

            //Remove Y from the raycast
            newPositionLimited = new Vector3(newPosition.x, transform.position.y, newPosition.z);

            //Move towards this ^^
            transform.position = Vector3.MoveTowards(transform.position, newPositionLimited, Time.deltaTime * speed);

            //Rotate towards the raycast, not including Y.
            var lookPos = transform.position - newPosition;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        }

    }
}

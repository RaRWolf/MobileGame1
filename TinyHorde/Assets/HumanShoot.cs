using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanShoot : MonoBehaviour
{

    public LayerMask layerMask;
    public LineRenderer gunLine;
    public float lineWidth = 0.1f;
    public float maxLineLength = 5f;

    public GameObject gunSpot;

    public float radius;
    Vector3 center;

    public bool firing = false;



    // Start is called before the first frame update
    void Start()
    {

        gunLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = transform.position;





        TurnAndShoot(ZombieDetector(center, radius));

    }

    GameObject ZombieDetector(Vector3 center, float radius)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            Vector3 difference = hitCollider.transform.position - position;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance)
            {
                closest = hitCollider.gameObject;
                distance = currentDistance;
            }
        }
        return closest;
    }

    void TurnAndShoot(GameObject target)
    {
        if (target != null)
        {
            //Turn   
            var lookPos = gunSpot.transform.position - target.transform.position;
            lookPos.y = 0;

            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.0f);

            //And Shoot
            if (!firing)
            {
                StartCoroutine("FirePistol");
                firing = true;
            }
        }
        else
        {
            Debug.Log("NoTarget");
        }
    }

    IEnumerator FirePistol()
    {
        
        yield return new WaitForSeconds(Random.Range(1,2));
        RaycastHit hit;
        if (Physics.Raycast(gunSpot.transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(gunSpot.transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            ZombieMove zombieScript = hit.transform.GetComponent<ZombieMove>();
            if (zombieScript != null)
            {
                zombieScript.TakeDamage();
            }

            StartCoroutine("DrawShot",  hit);
        }
        firing = false;
    }

    IEnumerator DrawShot(RaycastHit hit)
    {
        //Draw the shot
        Vector3 endPosition = hit.transform.position + (hit.distance * transform.TransformDirection(Vector3.back));
        endPosition = hit.point;

        gunLine.enabled = true;
        gunLine.SetPosition(0, gunSpot.transform.position);
        gunLine.SetPosition(1, endPosition);
        gunLine.useWorldSpace = true;
        yield return new WaitForSeconds(0.05f);
        gunLine.useWorldSpace = false;
        gunLine.enabled = false;
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

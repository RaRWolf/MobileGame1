using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanShoot : MonoBehaviour
{
    //Gunstuff
    public GameObject gunSpot;

    public LayerMask layerMask;
    public LineRenderer gunLine;

    public float lineWidth = 0.1f;
    public float maxLineLength = 5f;

    public bool firing = false;

    public ArmRotator armScript;

    //Zombie detector stuff
    public float radius;
    Vector3 center;


    //Color stuff
    public Color myShirtColor;
    public MeshRenderer[] clothes;
    public MeshRenderer head;

    //Death stuff
    public GameObject weapon;
    public int HP;
    public Animator myAnimator;
    public RuntimeAnimatorController zombieAnimation;
    public Material zombieSkin;
    public ParticleSystem blood;
    public Rigidbody myBody;

    void Start()
    {
        //Creates random skin tone
        head.material.color = head.material.color * Random.Range(0.25f, 2f);

        //Creates random shirt color
        myShirtColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        foreach (MeshRenderer clothing in clothes)
        {
            clothing.material.color = myShirtColor;
        }
    }

    // Start is called before the first frame update
    void OnAwake()
    {
        //Disables Linerenderer by default, just in case
        gunLine.enabled = false;

        //Gets the armscript
        armScript = GetComponentInChildren<ArmRotator>();

        //Set myself to be kinematic, for lag
        myBody.isKinematic = true;
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
            if (target.gameObject.CompareTag("zombie"))
            {
                myBody.isKinematic = false;
                //Turn
                var lookPos = gunSpot.transform.position - target.transform.position;

                armScript.stolenTarget = target.transform.position;


                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3.0f);

                //And Shoot
                if (!firing)
                {
                    StartCoroutine("FirePistol");
                    firing = true;
                }
            }
            
        }
        else
        {
            myBody.isKinematic = true;
        }
    }

    IEnumerator FirePistol()
    {
        
        yield return new WaitForSeconds(Random.Range(0.5f,1f));
        RaycastHit hit;
        if (Physics.Raycast(gunSpot.transform.position, gunSpot.transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity))
        {
            ZombieMove zombieScript = hit.transform.GetComponent<ZombieMove>();
            if (zombieScript != null)
            {
                zombieScript.TakeDamage();
                Debug.DrawRay(gunSpot.transform.position, gunSpot.transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
                StartCoroutine("DrawShot", hit);
            }
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

    public void TakeDamage()
    {
        HP -= 1;
        blood.Play();
        if(HP <= 0)
        {
            DataHolder.pistolKills += 1;

            myAnimator.runtimeAnimatorController = zombieAnimation as RuntimeAnimatorController;
            gameObject.AddComponent<AnimationScript>();
            gameObject.tag = "zombie";
            gameObject.layer = 8;

            head.material = zombieSkin;

            gameObject.AddComponent<ZombieMove>();
            Destroy(weapon);
            Destroy(gunLine);
            transform.parent = null;
            Destroy(this);
        }
    }

    //Deactivate many functions if the object is off-screen.
    void OnBecameInvisible()
    {
        myAnimator.enabled = false;
    }

    //Reactivate those functions upon becoming visible
    void OnBecameVisible()
    {
        myAnimator.enabled = true;
    }
}

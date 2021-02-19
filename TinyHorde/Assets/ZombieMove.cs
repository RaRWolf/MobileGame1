using UnityEngine;
using System.Collections;
public class ZombieMove : MonoBehaviour
{
    Vector3 newPosition;
    Vector3 newPositionLimited;

    public bool isMoving = false;
    public float speed = 4.0f;
    public ParticleSystem blood;

    AnimationScript walkScript;

    public int hp = 3;


    void Start()
    {
        newPosition = transform.position;

        blood = gameObject.GetComponent<ParticleSystem>();

        walkScript = gameObject.GetComponent<AnimationScript>();
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
            if ((Mathf.Abs(transform.position.x - newPositionLimited.x) > 0.5f) || (Mathf.Abs(transform.position.y - newPositionLimited.y) > 0.5f))
            {
                transform.position = Vector3.MoveTowards(transform.position, newPositionLimited, Time.deltaTime * speed);
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            //Rotate towards the raycast, not including Y.
            var lookPos = transform.position - newPosition;
            lookPos.y = 0;

            if (lookPos != new Vector3(0, 0, 0))
            {
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
            }

            
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            walkScript.normalIsMoving = true;
        }
        else
        {
            walkScript.normalIsMoving = false;
        }
    }

    //Getting shot
    public void TakeDamage()
    {
        hp = hp - 1;
        blood.Play();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Killing Humans
    void OnTriggerEnter(Collider human)
    {
        if (human.gameObject.CompareTag("Human"))
        {
            StartCoroutine("Attacking");
            human.GetComponent<Rigidbody>().velocity = transform.forward * -10.0f;
            human.GetComponent<HumanShoot>().TakeDamage();
            
        }
    }

    IEnumerator Attacking()
    {
        walkScript.normalIsAttacking = true;
        Debug.Log("IEnumerator Activated");
        yield return new WaitForSeconds(1);
        walkScript.normalIsAttacking = false;
    }
}

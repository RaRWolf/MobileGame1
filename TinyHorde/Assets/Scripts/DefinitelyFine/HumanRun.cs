using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRun : MonoBehaviour
{
    //Gunstuff
    public LayerMask layerMask;

    //Zombie detector stuff
    public Collider[] hitColliders;
    public float radius;
    private Vector3 center;
    private GameObject threat;

    //Animator
    Animator m_Animator;
    public bool normalIsMoving;

    //Color stuff
    public Color myShirtColor;
    public MeshRenderer[] clothes;
    public MeshRenderer head;

    //Death stuff
    public int HP;
    public Animator myAnimator;
    public RuntimeAnimatorController zombieAnimation;
    public Material zombieSkin;
    public ParticleSystem blood;
    public bool firstZombie;

    //Rigidbody
    public Rigidbody myBody;

    //Deactivation stuff
    public bool isVisible;

    void Start()
    {
        isVisible = true;
        //Creates random skin tone
        head.material.color = head.material.color * Random.Range(0.25f, 2f);

        //Start the detector
        threat = null;
        center = transform.position;
        StartCoroutine("Detect", radius);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        //Animator
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetFloat("Rando", Random.Range(0.0f, 1f));
        m_Animator.Play(0, -1, Random.value);

        //Creates random shirt color
        myShirtColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        foreach (MeshRenderer clothing in clothes)
        {
            clothing.material.color = myShirtColor;
        }

        //Set self to kinematic, for lag purposes
        myBody.isKinematic = true;

        //If I'm the first zombie, kill me
        if (firstZombie)
        {
            TakeDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isVisible)
        {
            center = transform.position;

            normalIsMoving = false;

            //TurnAndRun(ZombieDetector(center, radius));
            TurnAndRun(threat);

            if (normalIsMoving)
            {
                m_Animator.SetBool("IsMoving", true);
            }
            else
            {
                m_Animator.SetBool("IsMoving", false);
            }
        }
    }

    IEnumerator Detect(float radius)
    {
        while (isVisible)
        {
            yield return new WaitForSeconds(1);

            threat = null;

            Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);

            if (hitColliders.Length != 0)
            {
                threat = hitColliders[0].gameObject;
            }
            else
            {
                threat = null;
            }
        }
    }

    void TurnAndRun(GameObject target)
    {
        if (target != null)
        {
            if (target.gameObject.CompareTag("zombie"))
            {
                myBody.isKinematic = false;
                //Turn
                var lookPos = transform.position - target.transform.position;

                lookPos.y = 0;
                lookPos *= -1;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2.5f);

                //And Run
                if(DoIRun(target) == true)
                {
                    transform.position -= transform.forward * Time.deltaTime * 3f;
                    normalIsMoving = true;
                }
            }
        }
        else
        {
            myBody.isKinematic = true;
        }
    }

    private bool DoIRun(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        {
            ZombieMove zombieScript = hit.transform.GetComponent<ZombieMove>();
            if (zombieScript != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void TakeDamage()
    {
        HP -= 1;
        blood.Play();

        //On death:
        if(HP <= 0)
        {

            myAnimator.runtimeAnimatorController = zombieAnimation as RuntimeAnimatorController;
            gameObject.AddComponent<AnimationScript>();
            gameObject.tag = "zombie";
            gameObject.layer = 8;

            head.material = zombieSkin;
            gameObject.AddComponent<ZombieMove>();
            myBody.isKinematic = false;
            transform.parent = null;
            Destroy(this);
        }
    }


    //Deactivate many functions if the object is off-screen.
    void OnBecameInvisible()
    {
        isVisible = false;
        myAnimator.enabled = false;
    }

    //Reactivate those functions upon becoming visible
    void OnBecameVisible()
    {
        isVisible = true;
        threat = null;
        center = transform.position;
        StartCoroutine("Detect", radius);
        myAnimator.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerBehaviour : MonoBehaviour
{
    public UnityEvent OnPlayerDeath;

    public LayerMask enemyMask;
    public LayerMask objectIdentifiable;

    public Text frontObjectState;

    public GameObject footStep;
    public GameObject projectileStone;

    public Light2D playerLight;

    public Animator playerAnimator;

    public AIManager aIManager; // AI Property

    private float movingTime;
    private float clickCoolTime;

    public float moveCheckTime;
    private float noiseRadius;

    private float itemDuration;
    private float itemDistance;
    private bool isSword;
       
    public float noise
    {
        get
        {
            return noiseRadius;
        }
        set
        {
            noiseRadius = value;
        }
    }

    public float itemDura
    {
        get
        {
            return itemDuration;
        }
        set
        {
            itemDuration = value;
        }
    }

    public float itemDist
    {
        get
        {
            return itemDistance;
        }
        set
        {
            itemDistance = value;
        }
    }

    public bool isSwordAcquired
    {
        get
        {
            return isSword;
        }
        set
        {
            isSword = value;
        }
    }

    void Awake() 
    {
        aIManager = GameObject.Find("AIManager").GetComponent<AIManager>();
        playerAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        noise = 1.5f;
        playerLight.pointLightInnerRadius = 1f;
    }

    void Update()
    {
        StartCoroutine("ObjectIdentify");
        StartCoroutine("SetNoiseArea");

        clickCoolTime -= Time.deltaTime;
        moveCheckTime -= Time.deltaTime;
        itemDuration -= Time.deltaTime;

        if(moveCheckTime <= 0)
        {
           noise -= Time.deltaTime * 0.75f;

            if(noise < 2)
            {
                noise = 2f;
            }
        }

        if (movingTime > 0)
        {
            PlayerMove();
        }

        if(itemDuration > 0)
        {
            if(isSwordAcquired)
            {
                playerAnimator.SetBool("isInsane", true);
                playerLight.color = Color.red;
                playerLight.pointLightOuterRadius = itemDist;
                playerLight.gameObject.SetActive(true);
            }
            else
            {
                playerLight.color = Color.yellow;
                playerLight.pointLightOuterRadius = itemDist;
                playerLight.gameObject.SetActive(true);
            }
        }
        else
        {
            playerAnimator.SetBool("isInsane", false);
            playerLight.pointLightOuterRadius = 0f;
            playerLight.gameObject.SetActive(false);
        }
    }

    public void SetMovingTime()
    {
        if(clickCoolTime <= 0)
        {
            movingTime = 0.5f;
            clickCoolTime = 0.5f;
            moveCheckTime = 1f;

            GameObject footstep = Instantiate(footStep, transform.position, transform.rotation);
            footStep.GetComponent<AudioSource>().Play();

            noise += 0.6f;
        }
    }

    public void PlayerMove()
    {        
        transform.position += transform.right * 2f * Time.deltaTime;
        movingTime -= Time.deltaTime;
    }

    public void PlayerRotate()
    {
        transform.Rotate(0, 0, -90);
    }

    public void OnUsingSkill()
    {
        ProjectileBehaviour projectileInstance1 = Instantiate(projectileStone, transform.position, transform.rotation).GetComponent<ProjectileBehaviour>();
        projectileInstance1.FireProjectile(Vector2.up);

        ProjectileBehaviour projectileInstance2 = Instantiate(projectileStone, transform.position, transform.rotation).GetComponent<ProjectileBehaviour>();
        projectileInstance2.FireProjectile(Vector2.down);

        ProjectileBehaviour projectileInstance3 = Instantiate(projectileStone, transform.position, transform.rotation).GetComponent<ProjectileBehaviour>();
        projectileInstance3.FireProjectile(Vector2.right);

        ProjectileBehaviour projectileInstance4 = Instantiate(projectileStone, transform.position, transform.rotation).GetComponent<ProjectileBehaviour>();
        projectileInstance4.FireProjectile(Vector2.left);
    }

    public void OnPlayerDefeat()
    {
        GetComponent<AudioSource>().Play();

        Debug.Log("죽었다!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (isSwordAcquired)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                OnPlayerDeath.Invoke();
            }
        }
    }

    IEnumerator ObjectIdentify()
    {
        RaycastHit2D frontObject = Physics2D.Raycast(transform.position * 1f, transform.right, 1f, objectIdentifiable);

        if (frontObject.collider != null)
        {
            frontObjectState.text = "앞에 물체가 존재한다.";
        }
        else
        {
            frontObjectState.text = null;
        }

        yield return null;
    }

    IEnumerator SetNoiseArea()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, noiseRadius, enemyMask);

        aIManager.ZombieInRange(colliders);
        yield return null;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    // nav agent
    public GameObject enemyNav;

    public GameObject footStepRight;
    public GameObject footStepLeft;

    public GameObject playerObj;
    public Transform player;
    public Transform spawnPoint;

    private float speed = 2f;
    public int type = 1;
    private bool isAttacking = false;
    private bool isMoving = false;
    public bool GetIsAttack() {
        return isAttacking;
    }

    public AudioClip zombieSound_idle;
    public AudioClip zombieSound_attacking;


    // 4초후 플레이어 감지 확인
    IEnumerator CheckDetection()
    {        
        yield return new WaitForSeconds(3f);
        if (aIManager.CheckZombieInRange(GetComponent<BoxCollider2D>())) {
            Debug.Log("다시 AttackOn");
            ZombieAttack_On();
        }
        else {
            Debug.Log("AttackOff");
            ZombieAttack_Off();
        }
    }

    // // 좀비 페트롤
    // IEnumerator ZombiePatrol()
    // {                       
    //     Vector2 spawnDist = new Vector2(transform.position.x-spawnPoint.position.x,transform.position.y-spawnPoint.position.y);
    //     Vector2 endDist = new Vector2(transform.position.x-endPoint.position.x,transform.position.y-endPoint.position.y);

    //     if (!isMoving && spawnDist.magnitude<0.1) {
    //         enemyNav.GetComponent<NavMeshAgent>().SetDestination(endPoint.position);            
    //         isMoving = true;
    //     }                   
    //     else if (!isMoving && endDist.magnitude<0.1) {
    //         enemyNav.GetComponent<NavMeshAgent>().SetDestination(spawnPoint.position);            
    //         isMoving = true;
    //     }
    //     else if (isMoving && spawnDist.magnitude<0.1) {
    //         isMoving = false;
    //         yield return new WaitForSeconds(2f);
    //     }
    //     else if (isMoving && endDist.magnitude<0.1) {
    //         isMoving = false;
    //         yield return new WaitForSeconds(2f);            
    //     }
    // }

    // AI 프로퍼티
    public AIManager aIManager;

    private void Awake() {
        aIManager = GameObject.Find("AIManager").GetComponent<AIManager>();
        enemyNav.GetComponent<NavMeshAgent>().speed = speed;
    }


    private void Update() {       
        // 위치, 회전 동기화  
        transform.position = enemyNav.transform.position;   
        Debug.Log(playerObj.gameObject.activeSelf);
        if (!playerObj.activeSelf && isAttacking) {
            Debug.Log(playerObj.activeSelf);
            ZombieAttack_Off();            
        }

        if (isAttacking) {
            // Vector3 dir = player.transform.position - enemyNav.transform.position;
            // dir.z = 0f;
            // Debug.Log(dir);

            // //Vector3 targetPosition = new Vector3(enemyNav.transform.position.x, transform.position.y, enemyNav.transform.position.z);
            // //transform.LookAt(targetPosition);

            // //Quaternion.LookRotation();
            // Quaternion rot = Quaternion.LookRotation(dir);            
            // transform.rotation = rot;

            return;
        }

        // 정지 타입
        if (type == 1) {
            
        }
        // // 순찰 타입
        // else if (type == 2) {            
        //     StartCoroutine(ZombiePatrol());
        // }
    }

    public void ZombieAttack_On() {
        isAttacking = true;    
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = zombieSound_attacking;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();

        enemyNav.GetComponent<NavMeshAgent>().SetDestination(player.position);                   
        StartCoroutine(CheckDetection());
        StartCoroutine("ZombieFootStep");
    }

    public void ZombieAttack_Off() {
        isAttacking = false;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = zombieSound_idle;        
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
        
        StopCoroutine("ZombieFootStep");
        //enemyNav.GetComponent<NavMeshAgent>().isStopped = true;
        // 대기 시간을 넣을지 고민
        enemyNav.GetComponent<NavMeshAgent>().SetDestination(spawnPoint.position);                   
    }   

    IEnumerator ZombieFootStep()
    {
        Instantiate(footStepRight, transform.position, transform.rotation);

        yield return new WaitForSeconds(1.5f);

        Instantiate(footStepLeft, transform.position, transform.rotation);

        yield return new WaitForSeconds(0.35f);
    }

    // IEnumerator ZombieIdleSoundPlay() {
    //     GetComponent<AudioSource>().Play();
    //     yield return new WaitForSeconds(3f);
    // }
}

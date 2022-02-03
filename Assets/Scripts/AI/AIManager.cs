using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    Collider2D[] attackingZombie;

    // 좀비가 감지 범위 안에 있는가
    public void ZombieInRange(Collider2D[] colls) {
        attackingZombie = colls;

        foreach (Collider2D zombieColl in colls) {
            if (!zombieColl.GetComponent<ZombieBehaviour>().GetIsAttack()) {
                zombieColl.GetComponent<ZombieBehaviour>().ZombieAttack_On();
            }            
        }
    }   

    // 특정 좀비가 탐지 범위 안에 있는가
    public bool CheckZombieInRange(Collider2D coll) {        
        foreach (Collider2D zombieColl in attackingZombie) {
            if (zombieColl == coll) {
                return true;
            }
        }
        return false;
    }   
}

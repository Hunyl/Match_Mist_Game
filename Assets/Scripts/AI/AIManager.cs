using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    Collider2D[] attackingZombie;

    // ���� ���� ���� �ȿ� �ִ°�
    public void ZombieInRange(Collider2D[] colls) {
        attackingZombie = colls;

        foreach (Collider2D zombieColl in colls) {
            if (!zombieColl.GetComponent<ZombieBehaviour>().GetIsAttack()) {
                zombieColl.GetComponent<ZombieBehaviour>().ZombieAttack_On();
            }            
        }
    }   

    // Ư�� ���� Ž�� ���� �ȿ� �ִ°�
    public bool CheckZombieInRange(Collider2D coll) {        
        foreach (Collider2D zombieColl in attackingZombie) {
            if (zombieColl == coll) {
                return true;
            }
        }
        return false;
    }   
}

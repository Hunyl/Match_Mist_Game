using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Rigidbody2D projectileRigidBody;
    public Collider2D projectileCollider;

    public void FireProjectile(Vector2 projectileVector)
    {
        StartCoroutine("EnableCollider");

        projectileRigidBody.AddForce(projectileVector, ForceMode2D.Impulse);

        Destroy(gameObject, 3f);
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);

        projectileCollider.isTrigger = false;
    }
}

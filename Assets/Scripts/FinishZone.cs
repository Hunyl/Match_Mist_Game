using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    public GameObject ClearUI;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            ClearUI.gameObject.SetActive(true);
        }
    }
}

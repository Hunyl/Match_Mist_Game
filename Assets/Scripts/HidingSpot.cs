using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingSpot : MonoBehaviour
{
    public Button buttonHide;
    public Button buttonReturn;
    public Camera camera;
    public GameObject player;
    public PlayerBehaviour playerNoise;
    public LayerMask playerInteractable;

    public bool isHiding;

    void Update()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, playerInteractable);

        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green);

        if((playerHit.collider != null && playerHit.collider.name == "Player"))
        {
            Debug.Log("!");
            buttonHide.gameObject.SetActive(true);
            buttonHide.onClick.AddListener(Hide);
        }
        else if(isHiding)
        {
            buttonReturn.gameObject.SetActive(true);
            buttonReturn.onClick.AddListener(Return);
        }
        else
        {
            buttonHide.gameObject.SetActive(false);
            buttonReturn.gameObject.SetActive(false);

            buttonHide.onClick.RemoveAllListeners();
            buttonReturn.onClick.RemoveAllListeners();
        }
    }

    public void Hide()
    {
        if(isHiding == false)
        {
            playerNoise.noise = 2f;

            isHiding = true;

            buttonHide.gameObject.SetActive(false);
            buttonReturn.gameObject.SetActive(true);

            camera.transform.parent = null;
            player.gameObject.SetActive(false);
        }
    }

    public void Return()
    {
        if(isHiding == true)
        {
            isHiding = false;

            buttonHide.gameObject.SetActive(true);
            buttonReturn.gameObject.SetActive(false);

            player.gameObject.SetActive(true);
            camera.transform.parent = player.transform;
        }
    }
}

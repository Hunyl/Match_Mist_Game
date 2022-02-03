using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public PlayerBehaviour player;

    public void OnUIEnable()
    {
        gameObject.SetActive(true);
    }
}

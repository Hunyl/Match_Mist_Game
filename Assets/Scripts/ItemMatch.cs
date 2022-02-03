using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMatch : MonoBehaviour
{
    public enum ItemType
    {
        Small, Big, Sword
    }

    public ItemType matchType;

    public PlayerBehaviour player;

    private float duration;
    private float distance;
    private bool isSwordAc;

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (matchType)
        {
            case ItemType.Small:
                duration = 6f;
                distance = 2f;
                isSwordAc = false;
                break;
            case ItemType.Big:
                duration = 6f;
                distance = 3f;
                isSwordAc = false;
                break;
            case ItemType.Sword:
                duration = 8f;
                distance = 2.5f;
                isSwordAc = true;
                break;
        }

        player.itemDura = duration;
        player.itemDist = distance;
        player.isSwordAcquired = isSwordAc;

        Destroy(gameObject);
    }
}

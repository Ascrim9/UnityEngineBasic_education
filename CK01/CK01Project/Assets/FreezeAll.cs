using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAll : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerManager.Instance.player.moveSpeed -= 4;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerManager.Instance.player.moveSpeed += 4;
        }
    }
}

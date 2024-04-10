using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            Player.lastCheckPoint = this.transform.position;
            GetComponent<SpriteRenderer>().color = Color.white;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

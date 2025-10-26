using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBarrier : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bullet = collision.GetComponent<BulletController>();
        if (bullet != null) {
            bullet.BarrierHit();
        }
    }
}

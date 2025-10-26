using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Fader fader;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I ran fader??");
        
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SetActive(false);
            fader.FadeOut();
            GameManager.instance.playing = true;
        }
    }
}

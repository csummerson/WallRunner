using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPortal : MonoBehaviour
{
    Camera cam;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SetActive(false);
            
            cam = Camera.main;

            //cam.GetComponent<CameraController>().ZoomIn();
            
            GameManager.instance.ChangeGameState(GameManager.GameState.Win);
        }
    }
}

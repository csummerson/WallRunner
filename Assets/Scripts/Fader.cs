using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public float transitionSpeed = 2;
    public Image background;

    void Start()
    {
        Color color = background.color;
        color.a = 1;
        background.color = color;
    }

    void Update()
    {
        Color color = background.color;
        
        if (!fadeOut && color.a >= 0) {
            color.a -= transitionSpeed * Time.unscaledDeltaTime;
            background.color = color;
        } else if (fadeOut) {
            color.a += transitionSpeed * Time.unscaledDeltaTime * 2;
            background.color = color;
            if (color.a >= 1 && !worstCodeStructureBooleanEver) {
                StartCoroutine(GameManager.instance.LoadDelayed(0.2f));
            }
        } 
        
        
    }

    bool fadeOut = false;

    public bool worstCodeStructureBooleanEver = false;

    public void QUitOut() {
        GameManager.instance.JustQuit(); 
    }

    public void FadeOut() {
        fadeOut = true;
    }

    public IEnumerator LetThemThink() {


        yield return new WaitForSeconds(1f);
        

        fadeOut = true; 

        GameManager.instance.ChangeGameState(GameManager.GameState.Dead);
   
    }

    public void LetThemThink1()
    {
        StartCoroutine(LetThemThink());

    }
}

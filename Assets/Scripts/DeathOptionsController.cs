//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeathOptionsController : MonoBehaviour
{
    ModifierObject opt1, opt2;

    public TextMeshProUGUI opt1text, opt2text;

    System.Random random = new System.Random();

    void Start()
    {
        deathMessage.text = strings[random.Next(0, strings.Count)];
        
        opt1 = null;
        opt2 = null;
        
        List<ModifierObject> options = new List<ModifierObject>(GameManager.instance.enemeyModifiers);

        if (options.Count >= 2) {
            opt1 = options[Random.Range(0, options.Count)];
            options.Remove(opt1);
            opt2 = options[Random.Range(0, options.Count)];
            
            opt1text.text = opt1.menuText;
            opt2text.text = opt2.menuText;

        } else if (options.Count == 1) {
            opt1 = options[Random.Range(0, options.Count)];
            options.Remove(opt1);
            
            opt1text.text = opt1.menuText;
            opt2text.text = "None";

        } else {
            opt1text.text = "None";
            opt2text.text = "None";

        }
    }

    public Fader fader;

    public Mover mover, messageMover;

    public void Opt1() {
        StartCoroutine(Option1());
    }

    public void Opt2() {
        StartCoroutine(Option2());
    }

    public void Skip() {
        StartCoroutine(SkipIt());
    }

    IEnumerator Option1() {
        if (opt1 != null) { opt1.Decrease(); }
        GameManager.instance.enemeyModifiers.Remove(opt1);
        mover.inPlace = false;
        messageMover.inPlace = true;
        yield return new WaitForSeconds(2.5f);
        fader.worstCodeStructureBooleanEver = true;
        fader.FadeOut();
        StartCoroutine(GameManager.instance.Reload(0.8f));
    }

    IEnumerator Option2() {
        if (opt2 != null) { opt2.Decrease(); }
        GameManager.instance.enemeyModifiers.Remove(opt2);
        mover.inPlace = false;
        messageMover.inPlace = true;
        yield return new WaitForSeconds(2.5f);
        fader.worstCodeStructureBooleanEver = true;
        fader.FadeOut();
        StartCoroutine(GameManager.instance.Reload(0.8f));
    }

    IEnumerator SkipIt() {
        mover.inPlace = false;
        messageMover.inPlace = true;
        yield return new WaitForSeconds(2.5f);
        fader.worstCodeStructureBooleanEver = true;
        fader.FadeOut();
        StartCoroutine(GameManager.instance.Reload(0.8f));
    }



    public TextMeshProUGUI deathMessage;

    public List<string> strings = new List<string>();
}

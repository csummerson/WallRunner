using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpOptionsController : MonoBehaviour
{
    public TextMeshProUGUI text1p, text1n, text2p, text2n;
    ModifierObject pos1, neg1, pos2, neg2;
    
    public void SetOptions(ModifierObject p1, ModifierObject n1, ModifierObject p2, ModifierObject n2) {
        text1p.text = p1.menuText;
        text1n.text = n1.menuText;
        text2p.text = p2.menuText;
        text2n.text = n2.menuText;
        pos1 = p1;
        pos2 = p2;
        neg1 = n1;
        neg2 = n2;
    }
    
    public void Option1() {
        pos1.Increase();
        neg1.Increase();
        GameManager.instance.enemeyModifiers.Add(neg1);
        GameManager.instance.LevelUpFinished = true;
        GameManager.instance.ChangeGameState(GameManager.GameState.Play);
    }

    public void Option2() {
        pos2.Increase();
        neg2.Increase();
        GameManager.instance.enemeyModifiers.Add(neg2);
        GameManager.instance.LevelUpFinished = true;
        GameManager.instance.ChangeGameState(GameManager.GameState.Play);
    }

    public void Skip() {
        GameManager.instance.LevelUpFinished = true;
        GameManager.instance.ChangeGameState(GameManager.GameState.Play);
    }
}

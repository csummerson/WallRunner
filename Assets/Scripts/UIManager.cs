using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool levellingUp = false;

    public LevelUpOptionsController levelUpController;

    ModifierObject opt1p, opt1n, opt2p, opt2n;

    public LevelUpUIAnimator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        animator.MoveIn(false);
        GameManager.instance.levelUp += LevelUpSetUp;
    }

    // Update is called once per frame
    void Update()
    {
        levellingUp = GameManager.instance.gameState == GameManager.GameState.LevelUp;
        
        if (!levellingUp) {
            animator.MoveIn(false);
        } else {
            animator.MoveIn(true);
        }
    }

    void LevelUpSetUp() {
        List<ModifierObject> benefits = new List<ModifierObject>();
        List<ModifierObject> negatives = new List<ModifierObject>();
        foreach (ModifierObject obj in GameManager.instance.multipliers.definitions) {
            if (obj.benefit) {
                benefits.Add(obj);
            } else {
                negatives.Add(obj);
            }
        }
        
        if (GameManager.instance.playerLevel > 10) {
            opt1p = benefits[2];
        } else {
            opt1p = benefits[Random.Range(0, benefits.Count)];
        }
        benefits.Remove(opt1p);
        opt2p = benefits[Random.Range(0, benefits.Count)];

        opt1n = negatives[Random.Range(0, negatives.Count)];
        negatives.Remove(opt1n);
        opt2n = negatives[Random.Range(0, negatives.Count)];

        levelUpController.SetOptions(opt1p, opt1n, opt2p, opt2n);
    }

    public void QuitGame() {
        GameManager.instance.QuitGame(0.1f);
    }
}

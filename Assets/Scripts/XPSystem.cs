using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSystem : MonoBehaviour
{
    public AudioClip[] sounds;

    public void AddXPPoints(float xp) {
        GameManager.instance.xpCurrent += xp;
        StartCoroutine(LevelUpProcess());
    }

    private IEnumerator LevelUpProcess() {
        while (GameManager.instance.xpCurrent >= GameManager.instance.xpMax) {
            GameManager.instance.ChangeGameState(GameManager.GameState.LevelUp);

            yield return new WaitUntil(() => GameManager.instance.LevelUpFinished);

            GameManager.instance.xpCurrent -= GameManager.instance.xpMax;
            GameManager.instance.xpMax += 2;
        }
    }
}

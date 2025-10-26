using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierTarget {
    PlayerSpeed,
    PlayerShoot,
    PlayerHealth,
    EnemySpeed,
    EnemeyShoot,
    EnemyHealth,
    FractureSpeed
}

[System.Serializable]
public class ModifierObject 
{
    public string menuText;
    public float magnitude;
    public ModifierTarget modifierTarget;

    public bool benefit = false;
    public bool multiply = true;

    public void Increase()
    {
        // yeah there's probably some way of making this better but idk what that is...

        GameManager manager = GameManager.instance;
        if (multiply) {
            switch (modifierTarget)
            {
                case ModifierTarget.PlayerSpeed:
                    manager.playerSpeedModifier *= magnitude;
                    break;

                case ModifierTarget.PlayerShoot:
                    manager.playerShootModifier *= magnitude;
                    break;

                case ModifierTarget.PlayerHealth:
                    manager.playerHealthModifier *= magnitude;
                    manager.ChangePlayerHealth(magnitude);
                    break;

                case ModifierTarget.EnemySpeed:
                    manager.enemySpeedModifier *= magnitude;
                    break;

                case ModifierTarget.EnemeyShoot:
                    manager.enemyShootModifier *= magnitude;
                    break;

                case ModifierTarget.EnemyHealth:
                    manager.enemyHealthModifier *= magnitude;
                    manager.ChangeEnemeyHealth(magnitude);
                    break;

                case ModifierTarget.FractureSpeed:
                    manager.fractureSpeedModifer *= magnitude;
                    break;
            }
        } else {
            switch (modifierTarget)
            {
                case ModifierTarget.PlayerSpeed:
                    manager.playerSpeedModifier += magnitude;
                    break;

                case ModifierTarget.PlayerShoot:
                    manager.playerShootModifier += magnitude;
                    break;

                case ModifierTarget.PlayerHealth:
                    manager.playerHealthModifier += magnitude;
                    manager.ChangePlayerHealth(magnitude);
                    break;

                case ModifierTarget.EnemySpeed:
                    manager.enemySpeedModifier += magnitude;
                    break;

                case ModifierTarget.EnemeyShoot:
                    manager.enemyShootModifier += magnitude;
                    break;

                case ModifierTarget.EnemyHealth:
                    manager.enemyHealthModifier += magnitude;
                    manager.ChangeEnemeyHealth(magnitude);
                    break;

                case ModifierTarget.FractureSpeed:
                    manager.fractureSpeedModifer += magnitude;
                    break;
            }
        }
    }

    public void Decrease()
    {
        // yeah there's probably some way of making this better but idk what that is...

        GameManager manager = GameManager.instance;

        if (multiply) {
            switch (modifierTarget)
            {
                case ModifierTarget.PlayerSpeed:
                    manager.playerSpeedModifier /= magnitude;
                    break;

                case ModifierTarget.PlayerShoot:
                    manager.playerShootModifier /= magnitude;
                    break;

                case ModifierTarget.PlayerHealth:
                    manager.playerHealthModifier /= magnitude;
                    manager.ChangePlayerHealth(-magnitude);
                    break;

                case ModifierTarget.EnemySpeed:
                    manager.enemySpeedModifier /= magnitude;
                    break;

                case ModifierTarget.EnemeyShoot:
                    manager.enemyShootModifier /= magnitude;
                    break;

                case ModifierTarget.EnemyHealth:
                    manager.enemyHealthModifier /= magnitude;
                    manager.ChangeEnemeyHealth(-magnitude);
                    break;

                case ModifierTarget.FractureSpeed:
                    manager.fractureSpeedModifer /= magnitude;
                    break;
            }
        } else {
            switch (modifierTarget)
            {
                case ModifierTarget.PlayerSpeed:
                    manager.playerSpeedModifier -= magnitude;
                    break;

                case ModifierTarget.PlayerShoot:
                    manager.playerShootModifier -= magnitude;
                    break;

                case ModifierTarget.PlayerHealth:
                    manager.playerHealthModifier -= magnitude;
                    manager.ChangePlayerHealth(-magnitude);
                    break;

                case ModifierTarget.EnemySpeed:
                    manager.enemySpeedModifier -= magnitude;
                    break;

                case ModifierTarget.EnemeyShoot:
                    manager.enemyShootModifier -= magnitude;
                    break;

                case ModifierTarget.EnemyHealth:
                    manager.enemyHealthModifier -= magnitude;
                    manager.ChangeEnemeyHealth(-magnitude);
                    break;

                case ModifierTarget.FractureSpeed:
                    manager.fractureSpeedModifer -= magnitude;
                    break;
            }
        }
    }
}

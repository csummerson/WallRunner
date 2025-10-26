using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ModifierScriptableObject", order = 1)]
public class ModifierScriptableObject : ScriptableObject
{
    public ModifierObject[] definitions;
}

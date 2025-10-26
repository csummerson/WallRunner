using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HallGenerator))]
public class DungeonGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HallGenerator dungeonGenerator = (HallGenerator)target;

        if (GUILayout.Button("Generate Dungeon"))
        {
            dungeonGenerator.GenerateHall();
        }
    }
}

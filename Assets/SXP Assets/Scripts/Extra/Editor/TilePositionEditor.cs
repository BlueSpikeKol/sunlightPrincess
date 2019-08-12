using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilePosition))]
public class TilePositionEditor : Editor
{
    TilePosition tile;

    void OnEnable()
    {
        tile = (TilePosition) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GUILayout.Space(8f);
        GUILayout.Label("X: " + tile.GetTilePosition.x + " | Y: " + tile.GetTilePosition.y);
    }
}

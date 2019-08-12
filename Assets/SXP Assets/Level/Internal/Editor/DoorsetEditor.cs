using UnityEngine;
using SXP_EDITOR;
using UnityEditor;

[CustomEditor(typeof(DoorSet))]
public class DoorsetEditor : Editor
{
    DoorSet doorset;

    bool showInternal;

    void OnEnable()
    {
        doorset = (DoorSet) target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

        GUILayout.Label("Puzzle - Porte & Clef", CustomEditorUtilities.TitleLabelStyle);
        GUILayout.Space(4f);

        EditorGUI.BeginChangeCheck();
        DrawColorsSelection();
        if (EditorGUI.EndChangeCheck())
        {
            doorset.SetDoorsetColors();
        }

        DrawInternal();

        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(doorset);
    }

    void DrawColorsSelection()
    {
        EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

        GUILayout.Label("Couleur", CustomEditorUtilities.TitleLabelStyle);
        GUILayout.Space(4f);

        doorset.doorSetColors =
            (DoorSet.DoorSetColorsEnum)
                CustomEditorUtilities.DrawEnumPopup(
                    "",
                    doorset.doorSetColors);

        EditorGUILayout.EndVertical();
    }

    void DrawInternal()
    {
        EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

        GUILayout.Label("Internal", CustomEditorUtilities.TitleLabelStyle);
        GUILayout.Space(4f);

        showInternal = CustomEditorUtilities.SmallToggle("Show Internal", showInternal);

        if (showInternal)
        {
            GUILayout.Space(4f);

            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);
            GUILayout.Label("Renderer", CustomEditorUtilities.SubTitleLabelStyle);

            doorset.lockerRenderer = 
                (SpriteRenderer)
                EditorGUILayout.ObjectField(
                    "locker", 
                    doorset.lockerRenderer,
                    typeof (SpriteRenderer),
                    true);

            doorset.keyRenderer =
                (SpriteRenderer)
                EditorGUILayout.ObjectField(
                    "key",
                    doorset.keyRenderer,
                    typeof(SpriteRenderer),
                    true);

            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);
            GUILayout.Label("Colors Sets", CustomEditorUtilities.SubTitleLabelStyle);

            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);
            GUILayout.Label("Yellow", CustomEditorUtilities.SubTitleLabelStyle);
            doorset.yellowDoorsetColors.KeySprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Key Sprite",
                    doorset.yellowDoorsetColors.KeySprite);
            doorset.yellowDoorsetColors.LockerSprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Locker Sprite",
                    doorset.yellowDoorsetColors.LockerSprite);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);
            GUILayout.Label("Orange", CustomEditorUtilities.SubTitleLabelStyle);
            doorset.orangeDoorsetColors.KeySprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Key Sprite",
                    doorset.orangeDoorsetColors.KeySprite);
            doorset.orangeDoorsetColors.LockerSprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Locker Sprite",
                    doorset.orangeDoorsetColors.LockerSprite);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);
            GUILayout.Label("Blue", CustomEditorUtilities.SubTitleLabelStyle);
            doorset.blueDoorsetColors.KeySprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Key Sprite",
                    doorset.blueDoorsetColors.KeySprite);
            doorset.blueDoorsetColors.LockerSprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Locker Sprite",
                    doorset.blueDoorsetColors.LockerSprite);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);
            GUILayout.Label("Green", CustomEditorUtilities.SubTitleLabelStyle);
            doorset.greenDoorsetColors.KeySprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Key Sprite",
                    doorset.greenDoorsetColors.KeySprite);
            doorset.greenDoorsetColors.LockerSprite =
                CustomEditorUtilities.DrawEditableSpriteField(
                    "Locker Sprite",
                    doorset.greenDoorsetColors.LockerSprite);
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndVertical();
    }
}

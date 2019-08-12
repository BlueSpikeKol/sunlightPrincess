#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SXP_EDITOR
{
    public class CustomEditorUtilities
    {
        // Unity default GUI Styles list
        // https://gist.github.com/MadLittleMods/ea3e7076f0f59a702ecb

        const float DEFAULT_INDENTATION_WIDTH = 15.0f;
        const float DEFAULT_LABEL_WIDTH = 100.0f;
        const float DEFAULT_SPACER_WIDTH = 20.0f;
        const float DEFAULT_EDITABLE_WIDTH = 200.0f;

        static float indentationWidth = DEFAULT_INDENTATION_WIDTH;
        static float labelWidth = DEFAULT_LABEL_WIDTH;
        static float spacerWidth = DEFAULT_SPACER_WIDTH;
        static float editableWidth = DEFAULT_EDITABLE_WIDTH;

        static GUIStyle linkStyle = null;
        static GUIStyle labelWrapStyle = null;
        static GUIStyle smallTextStyle = null;
        static GUIStyle noteStyle = null;
        static GUIStyle smallButtonStyle = null;
        static GUIStyle rightAlignedPathStyle = null;
        static GUIStyle leftAlignedPathStyle = null;
        static GUIStyle centeredBoxStyle = null;
        static GUIStyle centeredBoxStyleBold = null;
        static GUIStyle centeredStyleBold = null;
        static GUIStyle frameBoxStyle = null;
        static GUIStyle previewBoxStyle = null;
        static GUIStyle titleLabelStyle = null;
        static GUIStyle subtitleTextStyle = null;


        #region SETTER

        public static void SetWidthsToDefault()
        {
            indentationWidth = DEFAULT_INDENTATION_WIDTH;
            labelWidth = DEFAULT_LABEL_WIDTH;
            spacerWidth = DEFAULT_SPACER_WIDTH;
            editableWidth = DEFAULT_EDITABLE_WIDTH;
        }

        public static void SetWidths(float indentationW, float labelW, float spacerW, float editableW)
        {
            indentationWidth = indentationW;
            labelWidth = labelW;
            spacerWidth = spacerW;
            editableWidth = editableW;
        }

        #endregion


        #region GETTER

        public static float IndentationWidth
        {
            get { return indentationWidth; }
        }

        public static float LabelWidth
        {
            get { return labelWidth; }
        }

        public static float SpacerWidth
        {
            get { return spacerWidth; }
        }

        public static float EditableWidth
        {
            get { return editableWidth; }
        }

        public static GUIStyle LinkStyle
        {
            get
            {
                if (linkStyle == null)
                {
                    linkStyle = new GUIStyle("Label");
                    linkStyle.normal.textColor = (EditorGUIUtility.isProSkin
                        ? new Color(0.8f, 0.8f, 1.0f, 1.0f)
                        : Color.blue);
                }
                return linkStyle;
            }
        }

        public static GUIStyle LabelWrapStyle
        {
            get
            {
                if (labelWrapStyle == null)
                {
                    labelWrapStyle = new GUIStyle("Label");
                    labelWrapStyle.wordWrap = true;
                }

                return labelWrapStyle;
            }
        }

        public static GUIStyle SmallTextStyle
        {
            get
            {
                if (smallTextStyle == null)
                {
                    smallTextStyle = new GUIStyle("Label");
                    smallTextStyle.fontSize = 9;
                    smallTextStyle.wordWrap = true;
                }

                return smallTextStyle;
            }
        }


        public static GUIStyle NoteStyle
        {
            get
            {
                if (noteStyle == null)
                {
                    noteStyle = new GUIStyle("Label");
                    noteStyle.fontSize = 9;
                    noteStyle.alignment = TextAnchor.LowerCenter;
                }

                return noteStyle;
            }
        }

        public static GUIStyle SmallButtonStyle
        {
            get
            {
                if (smallButtonStyle == null)
                {
                    smallButtonStyle = new GUIStyle("Button");
                    smallButtonStyle.fontSize = 8;
                    smallButtonStyle.alignment = TextAnchor.MiddleCenter;
                    smallButtonStyle.margin.left = 1;
                    smallButtonStyle.margin.right = 1;
                    smallButtonStyle.padding = new RectOffset(0, 4, 0, 2);
                }

                return smallButtonStyle;
            }
        }

        public static GUIStyle RightAlignedPathStyle
        {
            get
            {
                if (rightAlignedPathStyle == null)
                {
                    rightAlignedPathStyle = new GUIStyle("Label");
                    rightAlignedPathStyle.fontSize = 9;
                    rightAlignedPathStyle.alignment = TextAnchor.LowerRight;
                }

                return rightAlignedPathStyle;
            }
        }

        public static GUIStyle LeftAlignedPathStyle
        {
            get
            {
                if (leftAlignedPathStyle == null)
                {
                    leftAlignedPathStyle = new GUIStyle("Label");
                    leftAlignedPathStyle.fontSize = 9;
                    leftAlignedPathStyle.alignment = TextAnchor.LowerLeft;
                    leftAlignedPathStyle.padding = new RectOffset(0, 0, 2, 0);
                }

                return leftAlignedPathStyle;
            }
        }

        public static GUIStyle CenteredBoxStyle
        {
            get
            {
                if (centeredBoxStyle == null)
                {
                    centeredBoxStyle = new GUIStyle("Label");
                    centeredBoxStyle.fontSize = 10;
                    centeredBoxStyle.alignment = TextAnchor.LowerLeft;
                }

                return centeredBoxStyle;
            }
        }

        public static GUIStyle CenteredStyleBold
        {
            get
            {
                if (centeredStyleBold == null)
                {
                    centeredStyleBold = new GUIStyle("Label");
                    centeredStyleBold.fontSize = 10;
                    centeredStyleBold.alignment = TextAnchor.LowerLeft;
                    centeredStyleBold.fontStyle = FontStyle.Bold;
                }

                return centeredStyleBold;
            }
        }

        public static GUIStyle CenteredBoxStyleBold
        {
            get
            {
                if (centeredBoxStyleBold == null)
                {
                    centeredBoxStyleBold = new GUIStyle("TextField");
                    centeredBoxStyleBold.fontSize = 10;
                    centeredBoxStyleBold.alignment = TextAnchor.LowerLeft;
                    centeredBoxStyleBold.fontStyle = FontStyle.Bold;
                }

                return centeredBoxStyleBold;
            }
        }

        public static GUIStyle FrameBoxStyle
        {
            get
            {
                if (frameBoxStyle == null)
                {
                    frameBoxStyle = new GUIStyle("Box");
                }

                return frameBoxStyle;
            }
        }

        public static GUIStyle PreviewBoxStyle
        {
            get
            {
                if (previewBoxStyle == null)
                {
                    previewBoxStyle = new GUIStyle("Button");
                }

                return previewBoxStyle;
            }
        }


        public static GUIStyle TitleLabelStyle
        {
            get
            {
                if (titleLabelStyle == null)
                {
                    titleLabelStyle = new GUIStyle("IN Title");
                    titleLabelStyle.fontSize = 13;
                    titleLabelStyle.alignment = TextAnchor.LowerLeft;
                    titleLabelStyle.fontStyle = FontStyle.Bold;
                }

                return titleLabelStyle;
            }
        }

        public static GUIStyle SubTitleLabelStyle
        {
            get
            {
                if (subtitleTextStyle == null)
                {
                    subtitleTextStyle = new GUIStyle("IN TitleText");
                }
                
                return subtitleTextStyle;
            }
        }

        #endregion


        #region EDITABLE FIELDS

        /// <summary>
        /// Draw a Editable Text Field and a Label that fit the Window
        /// </summary>
        public static string DrawEditableTextField(string label, string output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.TextField(
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Draw a Editable Text Box and a Label that fit the Window
        /// </summary>
        public static string DrawEditableTextBox(string label, string output, float height = 70.0f)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.TextArea(
                    output,
                    GUILayout.Width(editableWidth),
                    GUILayout.Height(height));
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Draw a Editable GameObject field and a Label that fit the Window
        /// </summary>
        public static GameObject DrawEditableGameObjectField(string label, GameObject output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                (GameObject)
                    EditorGUILayout.ObjectField(
                        output,
                        typeof (GameObject),
                        false,
                        GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Draw a Editable Sprite field and a Label that fit the Window
        /// </summary>
        public static Sprite DrawEditableSpriteField(string label, Sprite output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                (Sprite) EditorGUILayout.ObjectField(
                    output,
                    typeof (Sprite),
                    false,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Draw a Editable Texture field and a Label that fit the Window
        /// </summary>
        public static Texture2D DrawEditableTextureField(string label, Texture2D output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                (Texture2D) EditorGUILayout.ObjectField(
                    output,
                    typeof (Texture2D),
                    false,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Draw a Editable Audioclip field and a Label that fit the Window
        /// </summary>
        public static AudioClip DrawEditableAudioClipField(string label, AudioClip output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                (AudioClip)
                    EditorGUILayout.ObjectField(
                        output,
                        typeof (AudioClip),
                        false,
                        GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Draw a Editable Mesh field and a Label that fit the Window
        /// </summary>
        public static Mesh DrawEditableMeshField(string label, Mesh output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                (Mesh)
                    EditorGUILayout.ObjectField(
                        output,
                        typeof (Mesh),
                        false,
                        GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Draw a Editable PhysicMaterial field and a Label that fit the Window
        /// </summary>
        public static PhysicMaterial DrawEditablePhysicMaterialField(string label, PhysicMaterial output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                (PhysicMaterial)
                    EditorGUILayout.ObjectField(
                        output,
                        typeof (PhysicMaterial),
                        false,
                        GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static int DrawEditableIntField(string label, int output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.IntField(
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static int DrawEditableIntField(string label, int output, int minValue, int maxValue)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);

            int lastValue = output;

            output =
                EditorGUILayout.IntField(
                    output,
                    GUILayout.Width(editableWidth));

            if (lastValue != output)
            {
                output = Mathf.Clamp(output, minValue, maxValue);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static int DrawEditableIntSlider(string label, int output, int minValue = 0, int maxValue = 100)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.IntSlider(
                    output,
                    minValue,
                    maxValue,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static float DrawEditableFloatField(string label, float output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.FloatField(
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static float DrawEditableFloatField(string label, float output, float minValue, float maxValue)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);

            float lastValue = output;

            output =
                EditorGUILayout.FloatField(
                    output,
                    GUILayout.Width(editableWidth));

            if (lastValue != output)
            {
                output = Mathf.Clamp(output, minValue, maxValue);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static float DrawEditableFloatSlider(string label, float output, float minValue = 0, float maxValue = 100)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.Slider(
                    output,
                    minValue,
                    maxValue,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static Vector2 DrawEditableVector2(string label, Vector2 output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.Vector2Field(
                    "",
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static Vector3 DrawEditableVector3(string label, Vector3 output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.Vector3Field(
                    "",
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static Vector4 DrawEditableVector4(string label, Vector4 output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.Vector4Field(
                    "",
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static Rect DrawEditableRect(string label, Rect output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.RectField(
                    "",
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static Enum DrawEnumPopup(string label, Enum output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.EnumPopup(
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        public static Color DrawEditableColorField(string label, Color output)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            output =
                EditorGUILayout.ColorField(
                    output,
                    GUILayout.Width(editableWidth));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return output;
        }

        /// <summary>
        /// Creates an Editable list of Unity objects
        /// </summary>
        public static List<UnityEngine.Object> DrawObjectList(string label, List<UnityEngine.Object> list, Type type)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            if (GUILayout.Button("Add", SmallButtonStyle, GUILayout.MinWidth(50), GUILayout.MaxWidth(50),
                GUILayout.MinHeight(15)))
            {
                list.Add(null);
            }

            GUILayout.Space(37);
            EditorGUILayout.EndHorizontal();

            for (int v = 0; v < list.Count; v++)
            {
                EditorGUILayout.BeginHorizontal();
                list[v] = EditorGUILayout.ObjectField(v.ToString(), list[v], type, false);
                if (GUILayout.Button("X", SmallButtonStyle, GUILayout.MinWidth(15), GUILayout.MaxWidth(15),
                    GUILayout.MinHeight(15)))
                {
                    list.RemoveAt(v);
                }

                EditorGUILayout.EndHorizontal();
            }

            return list;
        }

        /// <summary>
        /// Creates an Editable list of Unity materials
        /// </summary>
        public static List<Material> DrawMaterialsList(string label, List<Material> list, Type type)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            if (GUILayout.Button("Add", SmallButtonStyle, GUILayout.MinWidth(50), GUILayout.MaxWidth(50),
                GUILayout.MinHeight(15)))
            {
                list.Add(null);
            }

            GUILayout.Space(37);
            EditorGUILayout.EndHorizontal();

            for (int v = 0; v < list.Count; v++)
            {
                EditorGUILayout.BeginHorizontal();
                list[v] = (Material) EditorGUILayout.ObjectField(v.ToString(), list[v], type, false);
                if (GUILayout.Button("X", SmallButtonStyle, GUILayout.MinWidth(15), GUILayout.MaxWidth(15),
                    GUILayout.MinHeight(15)))
                {
                    list.RemoveAt(v);
                }

                EditorGUILayout.EndHorizontal();
            }

            return list;
        }

        #endregion


        #region BUTTONS

        /// <summary>
        /// Creates a Foldout button to clearly distinguish a section
        /// of controls from others
        /// </summary>
        public static bool SectionButton(string label, bool state)
        {
            GUI.color = new Color(0.9f, 0.9f, 1, 1);
            if (GUILayout.Button((state ? "- " : "+ ") + label.ToUpper(), GUILayout.Height(20)))
            {
                state = !state;
            }

            GUI.color = Color.white;
            return state;
        }

        /// <summary>
        /// Creates a big 2-button toggle with ON/OFF caption
        /// </summary>
        public static bool ButtonToggle(string label, bool state)
        {
            GUIStyle onStyle = new GUIStyle("Button");
            GUIStyle offStyle = new GUIStyle("Button");

            if (state)
            {
                onStyle.normal = onStyle.active;
            }
            else
            {
                offStyle.normal = offStyle.active;
            }

            EditorGUILayout.BeginHorizontal();
            if (!string.IsNullOrEmpty(label))
            {
                GUILayout.Label(label, GUILayout.Width(labelWidth + 50f));
            }

            GUILayout.FlexibleSpace();

            GUI.color = state ? Color.green : Color.white;
            if (GUILayout.Button("ON", onStyle, GUILayout.Width(editableWidth * 0.3f)))
            {
                state = true;
            }

            GUI.color = state ? Color.white : Color.red;
            if (GUILayout.Button("OFF", offStyle, GUILayout.Width(editableWidth * 0.3f)))
            {
                state = false;
            }

            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();
            return state;
        }

        /// <summary>
        /// Creates a big 2-button toggle with your own caption
        /// </summary>
        public static bool ButtonToggle(string label, bool state, string positiveCaption, string negativeCaption)
        {
            GUIStyle onStyle = new GUIStyle("Button");
            GUIStyle offStyle = new GUIStyle("Button");

            if (state)
            {
                onStyle.normal = onStyle.active;
            }
            else
            {
                offStyle.normal = offStyle.active;
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(label);
            if (GUILayout.Button(positiveCaption, onStyle))
            {
                state = true;
            }

            if (GUILayout.Button(negativeCaption, offStyle))
            {
                state = false;
            }

            EditorGUILayout.EndHorizontal();
            return state;
        }

        /// <summary>
        /// Creates a small toggle
        /// </summary>
        public static bool SmallToggle(string label, bool state)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            state =
                GUILayout.Toggle(
                    state,
                    "",
                    GUILayout.Width(12));
            EditorGUILayout.EndHorizontal();

            return state;
        }

        #endregion


        #region MASK FIELDS

        /// <summary>
        /// Returns a layermask popup
        /// </summary>
        public static LayerMask DrawLayerMaskField(string label, LayerMask selected, bool showSpecial,
            string tooltip = "")
        {
            List<string> layers = new List<string>();
            List<int> layerNumbers = new List<int>();
            string selectedLayers = "";

            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);

                if (layerName != "")
                {
                    if (selected == (selected | (1 << i)))
                    {
                        if (selectedLayers == "")
                        {
                            selectedLayers = layerName;
                        }
                        else
                        {
                            selectedLayers = "Mixed ...";
                        }
                    }
                }
            }

            if (Event.current.type != EventType.MouseDown && Event.current.type != EventType.ExecuteCommand)
            {
                if (selected.value == 0)
                {
                    layers.Add("Nothing");
                }
                else if (selected.value == -1)
                {
                    layers.Add("Everything");
                }
                else
                {
                    layers.Add(selectedLayers);
                }

                layerNumbers.Add(-1);
            }

            if (showSpecial)
            {
                layers.Add((selected.value == 0 ? "\u2714   " : "      ") + "Nothing");
                layerNumbers.Add(-2);

                layers.Add((selected.value == -1 ? "\u2714   " : "      ") + "Everything");
                layerNumbers.Add(-3);
            }

            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);

                if (layerName != "")
                {
                    if (selected == (selected | (1 << i)))
                    {
                        layers.Add("\u2714   " + layerName);
                    }
                    else
                    {
                        layers.Add("      " + layerName);
                    }

                    layerNumbers.Add(i);
                }
            }

            bool preChange = GUI.changed;
            GUI.changed = false;
            int newSelected = 0;

            if (Event.current.type == EventType.MouseDown)
            {
                newSelected = -1;
            }

            string[] strings = layers.ToArray();
            GUIContent[] gcStrings = new GUIContent[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                gcStrings[i] = new GUIContent(strings[i]);
            }

            GUILayout.BeginHorizontal();

            GUILayout.Space(indentationWidth);
            GUILayout.Label(label, GUILayout.Width(labelWidth));
            GUILayout.Space(spacerWidth);
            GUIContent empty = new GUIContent();
            newSelected =
                EditorGUILayout.Popup(
                    empty,
                    newSelected,
                    gcStrings,
                    EditorStyles.layerMaskField,
                    GUILayout.Width(editableWidth));
            GUILayout.EndHorizontal();

            if (GUI.changed && newSelected >= 0)
            {
                if (showSpecial && newSelected == 0)
                {
                    selected = 0;
                }
                else if (showSpecial && newSelected == 1)
                {
                    selected = -1;
                }
                else
                {
                    if (selected == (selected | (1 << layerNumbers[newSelected])))
                    {
                        selected &= ~(1 << layerNumbers[newSelected]);
                    }
                    else
                    {
                        selected = selected | (1 << layerNumbers[newSelected]);
                    }
                }
            }
            else
            {
                GUI.changed = preChange;
            }

            return selected;
        }

        /// <summary>
        /// Displays a Tag mask popup
        /// </summary>
        public static int TagMaskField(string label, int selected, ref List<string> list)
        {
            string[] options = UnityEditorInternal.InternalEditorUtility.tags;
            selected = EditorGUILayout.MaskField(selected, options, GUILayout.MinWidth(100));

            list.Clear();
            for (int i = 0; i < options.Length; i++)
            {
                if ((selected & 1 << i) != 0)
                {
                    list.Add(options[i]);
                }
            }

            return selected;
        }

        #endregion


        #region ITEMS

        /// <summary>
        /// Creates a horizontal line to visually separate groups of
        /// controls
        /// </summary>
        public static void Separator()
        {
            GUI.color = new Color(1, 1, 1, 0.25f);
            GUILayout.Box("", "HorizontalSlider", GUILayout.Height(16));
            GUI.color = Color.white;
        }

        #endregion


        #region UTILS TOOLS

        /// <summary>
        /// Returns the world scale of a screen pixel at a given world
        /// position, as seen from a given camera. this can be used to
        /// calculate overlaps with tool handles / gizmos. NOTE: this
        /// is all VERY brute force, so don't use every frame
        /// </summary>
        public static float GetPixelScaleAtWorldPosition(Vector3 position, Transform camera)
        {
            // in order to get a smooth result we calculate the distance to
            // all 8 pixels surrounding the source pixel and return average

            return (
                DistanceFromPixelToWorldPosition(-1, -1, position, camera) +
                DistanceFromPixelToWorldPosition(0, -1, position, camera) +
                DistanceFromPixelToWorldPosition(1, -1, position, camera) +
                DistanceFromPixelToWorldPosition(-1, 0, position, camera) +
                DistanceFromPixelToWorldPosition(1, 0, position, camera) +
                DistanceFromPixelToWorldPosition(-1, 1, position, camera) +
                DistanceFromPixelToWorldPosition(0, 1, position, camera) +
                DistanceFromPixelToWorldPosition(1, 1, position, camera))
                   *0.125f; // div by 8
        }

        /// <summary>
        /// Calculates the distance from a screen pixel at its world position
        /// to a given world position with a pixel offset. raycasts from the
        /// offset screen position to a camera-aligned plane at the world
        /// position, and returns the distance from the hit point to the
        /// world position
        /// </summary>
        static float DistanceFromPixelToWorldPosition(float xOffset, float yOffset, Vector3 position, Transform camera)
        {
            Handles.BeginGUI();
            Vector2 point = HandleUtility.WorldToGUIPoint(position);
            point.x += xOffset;
            point.y += yOffset;
            Ray ray = HandleUtility.GUIPointToWorldRay(point);
            Plane plane = new Plane(-camera.forward, position);
            float distance = 0.0f;
            Vector3 hitPoint = Vector3.zero;

            if (plane.Raycast(ray, out distance))
            {
                hitPoint = ray.GetPoint(distance);
            }

            Handles.EndGUI();
            return Vector3.Distance(position, hitPoint);
        }

        #endregion
    }
}

#endif
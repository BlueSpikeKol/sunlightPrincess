namespace SXP
{
    using System;

    public static class ArrayUtils
    {
        public static void AddItem<T>(ref T[] array, T item)
        {
            IncreaseSize(ref array);
            array[array.Length - 1] = item;
        }

        public static void IncreaseSize<T>(ref T[] array)
        {
            if (array == null) array = new T[0];
            Array.Resize(ref array, array.Length + 1);
        }

        public static void RemoveItem<T>(ref T[] array, int index)
        {
            int length = array.Length;
            bool move = false;
            for (int i = 0; i < length; i++)
            {
                if (move)
                {
                    array[i - 1] = array[i];
                }
                else if (i == index)
                {
                    move = true;
                }
            }
            Array.Resize(ref array, length - 1);
        }

        public static void MoveItemUp<T>(ref T[] array, int index)
        {
            int length = array.Length;
            T item = array[index];
            for (int i = 0; i < length; i++)
            {
                if (i == index)
                {
                    if (i != 0)
                    {
                        array[i] = array[i - 1];
                        array[i - 1] = item;
                    }
                    break;
                }
            }
        }

        public static void MoveItemDown<T>(ref T[] array, int index)
        {
            int length = array.Length;
            T item = array[index];
            for (int i = 0; i < length; i++)
            {
                if (i == index)
                {
                    if (i != length - 1)
                    {
                        array[i] = array[i + 1];
                        array[i + 1] = item;
                    }
                    break;
                }
            }
        }
    }
}



#if UNITY_EDITOR
namespace SXP
{
    using UnityEngine;
    using System;

    public static class ArrayEditorUtils
    {
        public static void AddControls<T>(ref T[] array, bool createItem = true, GUIStyle controlsStyle = null)
        {
            if (GUILayout.Button("✛", GetButtonStyle(controlsStyle)))
            {
                if (createItem)
                {
                    ArrayUtils.AddItem(ref array, Activator.CreateInstance<T>());
                }
                else
                {
                    ArrayUtils.IncreaseSize(ref array);
                }
            }
        }

        public static void AddItemControls<T>(ref T[] array, int index, bool copyItemRef = false, GUIStyle controlsStyle = null)
        {
            GUIStyle style = GetButtonStyle(controlsStyle);

            if (copyItemRef && GUILayout.Button("C", style))
            {
                ArrayUtils.AddItem(ref array, array[index]);
            }
            if (GUILayout.Button("▲", style))
            {
                ArrayUtils.MoveItemUp(ref array, index);
            }
            if (GUILayout.Button("▼", style, GUILayout.Width(24)))
            {
                ArrayUtils.MoveItemDown(ref array, index);
            }
            if (GUILayout.Button("☓", style))
            {
                ArrayUtils.RemoveItem(ref array, index);
            }
        }

        private static GUIStyle GetButtonStyle(GUIStyle style = null)
        {
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.GetStyle("button"));
                style.fontSize = 8;
                style.fixedWidth = 16;
                style.fixedHeight = 16;
            }
            return style;
        }
    }


}
#endif

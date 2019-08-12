namespace SXP
{
    using UnityEngine;

    [ExecuteInEditMode]
    public class TilesetsMapperOpener : MonoBehaviour
    {
        new Transform transform;

        void OnEnable()
        {
            transform = GetComponent<Transform>();
        }

        void LateUpdate()
        {
            transform.position = Vector3.zero;
        }
    }
}

#if UNITY_EDITOR
namespace SXP_EDITOR
{
    using SXP;
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(TilesetsMapperOpener))]
    public class TilesetsMapperOpener_Editor : Editor
    {
        TilesetsMapperOpener opener;

        void OnEnable()
        {
            opener = (TilesetsMapperOpener) target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Ouvrir Editeur de Niveau"))
            {
                TilesetsMapper.OnEnable();
            }
        }
    }
}
#endif
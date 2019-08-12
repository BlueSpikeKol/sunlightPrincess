using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Lava))]
public class LavaEditor : Editor 
{
	Lava lava;
	
	#region GETTERS 
	
	static GUIStyle headerStyle = null;
	static GUIStyle subtitleStyle = null;
	static GUIStyle contentStyle = null;

	static GUIStyle HeaderStyle
	{
		get
		{
			if (headerStyle == null)
			{
				headerStyle = new GUIStyle("IN Title");
				headerStyle.fontSize = 18;
				headerStyle.fixedHeight = 22;
				headerStyle.alignment = TextAnchor.MiddleLeft;
				headerStyle.fontStyle = FontStyle.Bold;
			}

			return headerStyle;
		}
	}

	static GUIStyle SubtitleStyle
	{
		get
		{
			if (subtitleStyle == null)
			{
				subtitleStyle = new GUIStyle(EditorStyles.largeLabel);
				subtitleStyle.fontSize = 15;
				subtitleStyle.alignment = TextAnchor.MiddleLeft;
				subtitleStyle.fontStyle = FontStyle.Bold;
			}

			return subtitleStyle;
		}
	}

	static GUIStyle ContentStyle
	{
		get
		{
			if (contentStyle == null)
			{
				contentStyle = new GUIStyle("Label");
				contentStyle.fontSize = 12;
				contentStyle.alignment = TextAnchor.LowerLeft;
				contentStyle.fontStyle = FontStyle.Bold;
			}

			return contentStyle;
		}
	}
	
	#endregion

	void OnEnable()
	{
		lava = (Lava) target;
	}
	
	public override void OnInspectorGUI()
	{
		DrawCustomHeader();
		
		PrefabType prType = PrefabUtility.GetPrefabType(lava);
		if (AssetDatabase.Contains(lava))
		{
			DrawProjectInspector();
		}
		else if (prType != PrefabType.DisconnectedPrefabInstance)
		{
			DrawPrefabInspector();
		}
		else
		{
			DrawCustomWaterInspector();
		}
	}

	void DrawCustomHeader()
	{
		EditorGUILayout.BeginVertical("Box");
		GUILayout.Space(4f);

		GUIContent headerContent = new GUIContent("Zone de Lave");
		GUILayout.Label(headerContent, HeaderStyle);

		GUILayout.Space(8f);
		EditorGUILayout.EndVertical();
	}

	void DrawProjectInspector()
	{
		EditorGUILayout.BeginVertical("Box");
		GUILayout.Space(4f);
		
		EditorGUILayout.HelpBox("Ajouter une zone de lave à la scène pour pouvoir la configurer.", MessageType.Info);
		
		GUILayout.Space(8f);
		EditorGUILayout.EndVertical();
	}

	void DrawPrefabInspector()
	{
		EditorGUILayout.BeginVertical("Box");
		GUILayout.Space(4f);

		GUI.color = Color.green;
		if (GUILayout.Button("Modifier la zone de lave", GUILayout.Height(30f)))
		{
			PrefabUtility.DisconnectPrefabInstance(lava);
			lava.RefreshLava();
		}
		GUI.color = Color.white;
		
		GUILayout.Space(8f);
		EditorGUILayout.EndVertical();
	}

	void DrawCustomWaterInspector()
	{
		EditorGUILayout.BeginVertical("Box");
		GUILayout.Space(4f);
		EditorGUI.BeginChangeCheck();

		lava.size = EditorGUILayout.Vector2Field("Dimension", lava.size);

		if (EditorGUI.EndChangeCheck())
		{
			lava.RefreshLava();
		}
		
		GUILayout.Space(8f);
		EditorGUILayout.EndVertical();
	}
}

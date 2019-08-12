using System.IO;
using UnityEditor;
using UnityEngine;

public static class Screenshooter
{
	const int RES_SCALE = 1;
	const int DEFAULT_WIDTH  = 1920;
	const int DEFAULT_HEIGHT = 1080;

	const string DEFAULT_PATH = "Etudiant/Screenshots/";
	const string DEFAULT_NAME = "Screenshot";
	const string SCRNSHOT_TYPE = ".png";

	const bool SCENEVIEW_SIZE = false;
	const bool SHOW_IN_EXPLORER = true;
	

	[MenuItem("Studio XP/Capture d'écran", false, 100)]
	public static void TakeScreenshot()
	{
		Screenshot();
	}
	
	public static void Screenshot()
	{
	    Camera editorCam = null;
	    if(!EditorApplication.isPlaying)
	    {
		    editorCam = ((SceneView) SceneView.sceneViews[0]).camera;
	    }
		else
		{
			editorCam = Camera.current;
		}

		if(editorCam == null)
		{
			Debug.LogWarning("SCREENSHOOTER:: No camera found to screenshot");
			return;
		}

		int resWidth = SCENEVIEW_SIZE ? (int) Handles.GetMainGameViewSize().x : DEFAULT_WIDTH;
		resWidth *= RES_SCALE;
		int resHeight = SCENEVIEW_SIZE ? (int) Handles.GetMainGameViewSize().y : DEFAULT_HEIGHT;
		resHeight *= RES_SCALE;
		
		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		editorCam.targetTexture = rt;
		
		Texture2D screenshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
		editorCam.Render();
		RenderTexture.active = rt;
		
		screenshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		editorCam.targetTexture = null;
		RenderTexture.active = null; 
		
		byte[] bytes = screenshot.EncodeToPNG();
		string filename = Application.dataPath + "/" + DEFAULT_PATH + DEFAULT_NAME + SCRNSHOT_TYPE;

		if (!Directory.Exists(Application.dataPath + "/" + DEFAULT_PATH))
		{
			Directory.CreateDirectory(Application.dataPath + "/" + DEFAULT_PATH);
		}

		filename = filename.Substring(filename.IndexOf("Assets/"));
		filename = AssetDatabase.GenerateUniqueAssetPath(filename);
		filename = filename.Substring(filename.IndexOf("Assets") + 6);
		filename = Application.dataPath + filename;
		
		File.WriteAllBytes(filename, bytes);
		
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		if (SHOW_IN_EXPLORER)
		{
			EditorUtility.RevealInFinder(filename);
		}
	}
}

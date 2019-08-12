#if UNITY_EDITOR
namespace SXP_EDITOR
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.SceneManagement;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class TilesetsMapper : EditorWindow
    {
        #region PROPERTIES

        // Windows Instance
        static EditorWindow window;
        static bool shouldDestroyWindows;

        // Windows State
        public enum MapperWindowStateEnum
        {
            Editeur,
            Parametres,
            //Help
        }

        static MapperWindowStateEnum windowState;

        // Tools State
        public enum MapperToolsStateEnum
        {
            Peinturer,
            Effacer,
            BoxPaint
        }

        static MapperToolsStateEnum toolsState;

        // Settings
        static TilesetsMapper_Settings settings;

        // Cached Tilesets
        static Texture[] tilesetTexturesList;
        static Sprite[] tilesetSpritesList;
        static Sprite[] loadedTilesetsList;

        // Current Selection
        static int currentSelectedTileSet = 0;
        static List<int> currentSelectedTilesIndexes = new List<int>();
        static List<Sprite> currentSelectedTilesSprites = new List<Sprite>();

        // Scene Object
        static GameObject sceneObject;

        // Inspector Tiles Preview
        static Vector2 tilePreviewScrollPosition = Vector2.zero;
        static Texture2D tilePreviewSelectionColor;

        // SceneView Tile Preview
        static Vector3 scenePreviewPosition;
        static Vector2 scenePreviewBox;

        // Layers
        static GameObject currentLayerObject;
        static int currentLayerIndex = 0;
        static List<Transform> layersList = new List<Transform>();
        static bool highlightLayer = false;

        // Edition
        static bool generateCollider = true;
        // static bool isPlatform = false;
        static string[] tilesetsNames;
        static int index_a;
        static int index_b;

        // Editor Cached Textures
        static Texture2D texVisible;
        static Texture2D texHidden;

        // Event
        static Event mapperEvent;

        // Grid
        static int gridSizeX = 32;
        static int gridSizeY = 32;
        static int padSizeX = 1;
        static int padSizeY = 1;

        // Tiles Material
        static Material tileMat;

        #endregion

        #region ENGINE OVERRIDE

        [MenuItem("Studio XP/Éditeur de Niveau", false, 0)]
        public static void OnEnable()
        {
            if (shouldDestroyWindows)
            {
                if (window != null)
                {
                    window.Close();
                }

                shouldDestroyWindows = false;
            }

            //Reset variables chunk. This is for new files being added, generated, etc.
            AssetDatabase.Refresh();
            tilesetTexturesList = new Texture[0];
            tilesetSpritesList = new Sprite[0];
            layersList.Clear();

            SceneView.onSceneGUIDelegate += OnSceneGUI; //Sets delegate for adding the OnSceneGUI event
            toolsState = MapperToolsStateEnum.Peinturer;

            // Load Settings
            LoadMapperSettings();
            LoadMapperTilesets();

            tilePreviewSelectionColor = new Texture2D(1, 1); //makes highlight color for selecting tiles
            tilePreviewSelectionColor.alphaIsTransparency = true;
            tilePreviewSelectionColor.filterMode = FilterMode.Point;
            tilePreviewSelectionColor.SetPixel(0, 0, new Color(.5f, .5f, 1f, .5f));
            tilePreviewSelectionColor.Apply();

            bool asUtitlity = settings != null && settings.AsUtilityWindow;

            window = EditorWindow.GetWindow(typeof (TilesetsMapper), asUtitlity, "Éditeur de Niveau", true);
            window.minSize = new Vector2(350f, 400f);
        }

        [MenuItem("Studio XP/Créer un nouvel ensemble de tuiles", false, 10)]
        public static void CreateNewTileset()
        {
            string templatePath = "Assets/SXP Assets/Sprites/Tileset_Template.png";
            string tilesetEtudiantPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Etudiant/Tilesets/Tileset_Etudiant.png");

            if(AssetDatabase.CopyAsset(templatePath, tilesetEtudiantPath))
            {
                var tilesetEtudiant = AssetDatabase.LoadAssetAtPath<Texture>(tilesetEtudiantPath);
                Selection.activeObject = tilesetEtudiant;
            }
            else
            {
                var tilesetTemplate = AssetDatabase.LoadAssetAtPath<Texture>(templatePath);

                if(tilesetTemplate == null)
                {
                    Debug.Log("Tileset template does not exist at path : \"Assets/SXP Assets/Sprites/Tileset_Template.png\"");
                }
            }
        }

        void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;

            if(!Application.isPlaying)
            {
                AssetDatabase.SaveAssets();
                EditorSceneManager.SaveOpenScenes();
            }
        }

        static void ResetMapper()
        {
            //Intended to create objects required for the tileset editor to work
            if (sceneObject == null)
            {
                //Look for my tileset object. If it doesn't exist, create a very large quad.
                sceneObject = GameObject.Find("TilesetsMapperObject");
                if (sceneObject == null)
                {
                    sceneObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    sceneObject.name = "TilesetsMapperObject";
                    sceneObject.transform.localScale = new Vector3(1000000, 1000000, 1000000);
                    AddLayer();
                    ResetLayers();
                    currentLayerObject = layersList[0].gameObject;
                    sceneObject.AddComponent<SXP.TilesetsMapperOpener>();
                }

                sceneObject.GetComponent<Renderer>().enabled = false; // disable quad's renderer
                EditorUtility.SetSelectedRenderState(sceneObject.GetComponent<Renderer>(), EditorSelectedRenderState.Hidden);
            }

            if (window != null)
            {
                //force repaint to show proper layersList if the window exists.
                window.Repaint();
            }
        }

        void Update()
        {
            if (EditorApplication.isPlaying)
            {
                Close();
            }
            /*
            else if (Selection.activeObject != null && !Selection.activeObject.name.Equals("TilesetsMapperObject"))
            {
                Close();
            }
            */
        }

        void OnGUI()
        {
            ResetMapper(); //Remakes game objects require to operate.
            
            /*
            mapperEvent = Event.current; //Gets current event (mouse move, repaint, keyboard press, etc)

            if (editionState != -1 &&
                (mapperEvent.keyCode == KeyCode.Return || mapperEvent.keyCode == KeyCode.KeypadEnter))
            {
                editionState = -1;
            }
            */

            if (tilesetTexturesList == null)
            {
                EditorGUILayout.LabelField("No tilesets found. Retrying.");
                OnEnable();
            }
            else
            {
                tilesetsNames = new string[tilesetTexturesList.Length];

                for (index_a = 0; index_a < tilesetTexturesList.Length; index_a++)
                {
                    try
                    {
                        tilesetsNames[index_a] = tilesetTexturesList[index_a].name;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(
                            "There was an error getting the names of the files. We'll try to reload the tilesets. " +
                            "If this continues to show, please close the script and try remimporting and check your images.");
                        Debug.Log("Full system error: " + e.Message);
                        OnEnable();
                    }
                }

                DrawWindowsToolbar();

                switch (windowState)
                {
                    case MapperWindowStateEnum.Editeur:
                        DrawMapperWindow();
                        break;

                    case MapperWindowStateEnum.Parametres:
                        DrawSettingsWindow();
                        break;
                        /*
                    case MapperWindowStateEnum.Help:
                        DrawHelpWindow();
                        break;*/

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            EditorUtility.SetDirty(window);
        }

        void OnSelectionChange()
        {
            Repaint();
        }

        static void OnSceneGUI(SceneView sceneview)
        {
            int i, j;
            if (sceneObject != null)
            {
                if (sceneObject.transform.childCount <= 0)
                {
                    //double checks there is at least 1 layer inside of sceneObject.
                    AddLayer();
                    ResetLayers();
                }

                if (Event.current.type == EventType.Layout)
                {
                    HandleUtility.AddDefaultControl(GUIUtility.GetControlID(window.GetHashCode(), FocusType.Passive));
                }

                mapperEvent = Event.current;

                Ray ray = HandleUtility.GUIPointToWorldRay(mapperEvent.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    //Draw gui elements in scene based on mouse position
                    Handles.BeginGUI();
                    Handles.color = Color.white;
                    Handles.Label(scenePreviewPosition, " ", EditorStyles.boldLabel);
                    
                    if ((scenePreviewPosition.x != scenePreviewBox.x || scenePreviewPosition.y != scenePreviewBox.y) && toolsState == MapperToolsStateEnum.BoxPaint)
                    {
                        if (scenePreviewPosition.x >= scenePreviewBox.x)
                        {
                            if (scenePreviewPosition.y <= scenePreviewBox.y)
                            {
                                Handles.DrawLine(new Vector3(scenePreviewBox.x, scenePreviewBox.y + 1, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y + 1, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y + 1, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewBox.x, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x, scenePreviewBox.y + 1, 0));
                            }
                            else
                            {
                                Handles.DrawLine(new Vector3(scenePreviewBox.x, scenePreviewBox.y, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewBox.x, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x, scenePreviewBox.y, 0));
                            }
                        }
                        else
                        {
                            if (scenePreviewPosition.y <= scenePreviewBox.y)
                            {
                                Handles.DrawLine(new Vector3(scenePreviewBox.x + 1, scenePreviewBox.y + 1, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y + 1, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y + 1, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x + 1, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewBox.x + 1, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x + 1, scenePreviewBox.y + 1, 0));
                            }
                            else
                            {
                                Handles.DrawLine(new Vector3(scenePreviewBox.x + 1, scenePreviewBox.y, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewBox.y, 0), new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewPosition.x + 1, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x + 1, scenePreviewPosition.y, 0));
                                Handles.DrawLine(new Vector3(scenePreviewBox.x + 1, scenePreviewPosition.y, 0), new Vector3(scenePreviewBox.x + 1, scenePreviewBox.y, 0));
                            }
                        }
                    }
                    else
                    {
                        Handles.DrawLine(scenePreviewPosition + new Vector3(0, 0, 0), scenePreviewPosition + new Vector3(1, 0, 0));
                        Handles.DrawLine(scenePreviewPosition + new Vector3(1, 0, 0), scenePreviewPosition + new Vector3(1, 1, 0));
                        Handles.DrawLine(scenePreviewPosition + new Vector3(1, 1, 0), scenePreviewPosition + new Vector3(0, 1, 0));
                        Handles.DrawLine(scenePreviewPosition + new Vector3(0, 1, 0), scenePreviewPosition + new Vector3(0, 0, 0));
                    }
                    Handles.EndGUI();

                    if (mapperEvent.isMouse)
                    {
                        if (mapperEvent.button == 0 && (mapperEvent.type == EventType.MouseUp || mapperEvent.type == EventType.MouseDrag))
                        {
                            if (toolsState == MapperToolsStateEnum.Peinturer)
                            {
                                Sprite[] tmpCurSprite = new Sprite[currentSelectedTilesSprites.Count];
                                currentSelectedTilesSprites.CopyTo(tmpCurSprite);

                                if (tmpCurSprite.Length > 0)
                                {
                                    GameObject tmpObj = GenerateTile(hit.point.x, hit.point.y);

                                    tmpObj.GetComponent<SpriteRenderer>().sprite = tmpCurSprite[UnityEngine.Random.Range((int) 0, (int) tmpCurSprite.Length)];
                                    tmpObj.transform.localPosition = new Vector3(Mathf.Floor(hit.point.x) + .5f, Mathf.Floor(hit.point.y) + .5f, layersList[currentLayerIndex].transform.position.z);

                                    Undo.RegisterCreatedObjectUndo(tmpObj, "Created Tile");
                                }
                                else
                                {
                                    Debug.LogWarning("Tile not selected for painting. Please select a tile to paint.");
                                }
                            }
                            else if (toolsState == MapperToolsStateEnum.Effacer)
                            {
                                Transform tmpObj = layersList[currentLayerIndex].Find("Tile|" + (Mathf.Floor(hit.point.x) + .5f) + "|" + (Mathf.Floor(hit.point.y) + .5f));
                                if (tmpObj != null)
                                {
                                    Undo.DestroyObjectImmediate(tmpObj.gameObject);
                                }
                            }
                            else if (toolsState == MapperToolsStateEnum.BoxPaint)
                            {
                                if (mapperEvent.type == EventType.MouseUp)
                                {
                                    Vector2 distance;
                                    bool drawLeft, drawUp;


                                    if (scenePreviewBox.x >= hit.point.x)
                                    {
                                        distance.x = scenePreviewBox.x - hit.point.x;
                                        drawLeft = true;
                                    }
                                    else
                                    {
                                        distance.x = hit.point.x - scenePreviewBox.x;
                                        drawLeft = false;
                                    }

                                    if (scenePreviewBox.y >= hit.point.y)
                                    {
                                        distance.y = scenePreviewBox.y - hit.point.y;
                                        drawUp = false;
                                    }
                                    else
                                    {
                                        distance.y = hit.point.y - scenePreviewBox.y;

                                        distance.y -= 1;
                                        drawUp = true;
                                    }

                                    if (scenePreviewPosition.y > scenePreviewBox.y)
                                    {
                                        distance.y -= 1;
                                    }


                                    for (i = 0; i <= distance.x; i++)
                                    {
                                        for (j = 0; j <= Mathf.Ceil(distance.y); j++)
                                        {
                                            int curI = i;
                                            int curJ = j;
                                            if (drawLeft)
                                            {
                                                curI = -curI;
                                            }
                                            if (drawUp)
                                            {
                                                curJ = -curJ;
                                            }
                                            if (currentSelectedTilesSprites != null)
                                            {
                                                GameObject tmpObj = GenerateTile(scenePreviewBox.x + curI, scenePreviewBox.y - curJ);
                                                Undo.RegisterCreatedObjectUndo(tmpObj, "Created Tiles in Box");
                                                Sprite[] tmpCurSprite = new Sprite[currentSelectedTilesSprites.Count];
                                                currentSelectedTilesSprites.CopyTo(tmpCurSprite);
                                                tmpObj.GetComponent<SpriteRenderer>().sprite = tmpCurSprite[UnityEngine.Random.Range((int) 0, (int) tmpCurSprite.Length)];
                                                tmpObj.transform.localPosition = new Vector3(Mathf.Floor(scenePreviewBox.x + curI) + .5f, Mathf.Floor(scenePreviewBox.y - curJ) + .5f, layersList[currentLayerIndex].transform.position.z);
                                            }
                                            else
                                            {
                                                Debug.LogWarning("No tiles selected.");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (mapperEvent.button == 0 && mapperEvent.type == EventType.MouseDown)
                        {
                            scenePreviewBox.x = Mathf.Floor(hit.point.x);
                            scenePreviewBox.y = Mathf.Floor(hit.point.y);
                        }
                        else if (mapperEvent.type == EventType.MouseMove)
                        {
                            scenePreviewBox.x = Mathf.Floor(hit.point.x);
                            scenePreviewBox.y = Mathf.Floor(hit.point.y);
                        }
                    }
                    scenePreviewPosition.x = (float) Mathf.Floor(hit.point.x);
                    scenePreviewPosition.y = (float) Mathf.Floor(hit.point.y);
                    if (currentLayerObject != null)
                    {
                        scenePreviewPosition.z = currentLayerObject.transform.position.z - 1;
                    }
                    else
                    {
                        scenePreviewPosition.z = 0;
                    }
                }
            }
            else
            {
                ResetMapper();
            }

            SceneView.RepaintAll();
        }

        #endregion

        #region EDITION

        static void LoadMapperSettings()
        {
            //Load setting
            settings = Resources.Load("EditorResources/TilesetsMapperSettings") as TilesetsMapper_Settings;

            if (settings == null)
            {
                string path = "Assets/Resources/EditorResources";
                string name = "TilesetsMapperSettings";
                settings = ScriptableObjectUtility.CreateAsset<TilesetsMapper_Settings>(path, name);
            }
        }

        static void LoadMapperTilesets()
        {
            tilesetTexturesList = Resources.LoadAll<Texture>("Tilesets"); //Load all tilesets as texture
            tilesetSpritesList = Resources.LoadAll<Sprite>("Tilesets"); //Load all tileset sub objects as tiles
            texVisible = Resources.Load("EditorResources/Visible") as Texture2D; //Load visible icon
            texHidden = Resources.Load("EditorResources/Hidden") as Texture2D; //Load hidden icon
            tileMat = Resources.Load("EditorResources/mat_tiles") as Material;
            
            List<Texture> texturesEtudiant = new List<Texture>();
            List<Sprite> spritesEtudiant = new List<Sprite>();

            string filePath = Application.dataPath + "/Etudiant/Tilesets";
            string[] aFilePaths = System.IO.Directory.GetFiles(filePath);
            aFilePaths = aFilePaths.Select(path => "Assets/Etudiant/Tilesets/" + System.IO.Path.GetFileName(path)).ToArray();

            foreach(var path in aFilePaths)
            {
                var texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path).Where(x => x is Sprite).Cast<Sprite>();

                if(texture != null && sprites.Count() > 0)
                {
                    texturesEtudiant.Add(texture);
                    spritesEtudiant.AddRange(sprites);
                }
            }
            
            tilesetTexturesList = texturesEtudiant.Concat(tilesetTexturesList).ToArray();
            tilesetSpritesList = spritesEtudiant.Concat(tilesetSpritesList).ToArray();

            LoadTileset(0); //processes loaded tiles into proper tilesets
        }

        static void DrawWindowsToolbar()
        {
            try
            {
                EditorGUILayout.BeginHorizontal(CustomEditorUtilities.FrameBoxStyle);
                GUILayout.Space(10f);

                var states = Enum.GetValues(typeof (MapperWindowStateEnum));
                foreach (var state in states)
                {
                    GUI.color = windowState.Equals((MapperWindowStateEnum) state) ? Color.green : Color.white;
                    if (GUILayout.Button(state.ToString()))
                    {
                        windowState = (MapperWindowStateEnum) state;
                    }
                    GUI.color = Color.white;
                }

                GUILayout.Space(10f);
                EditorGUILayout.EndHorizontal();
            }
            catch (Exception e) { }
        }

        void DrawMapperWindow()
        {
            try
            {
                EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

                //DrawLayersBox();
                ResetLayers();
                layersList = ResortLayers(layersList);

                DrawMapperWindow_Tools();

                DrawMapperWindow_Options();

                DrawMapperWindow_TilesPreviews();

                EditorGUILayout.EndVertical();
            }
            catch(Exception e) { }
        }

        static void DrawMapperWindow_Tools()
        {
            try
            {
                //If in standard paint mode, display the proper gui elements for the user to use.
                EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("OUTILS", CustomEditorUtilities.TitleLabelStyle);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Recharger", GUILayout.Width(70f), GUILayout.Height(20f)))
                {
                    OnEnable();
                }
                GUILayout.Space(15f);
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(4f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15f);
                GUILayout.Label("Ensemble de Tuiles");
                
                int tempTileset = currentSelectedTileSet;
                tempTileset = EditorGUILayout.Popup(tempTileset, tilesetsNames);
                //Forces selection of first tileset if none are selected.
                if (tempTileset != currentSelectedTileSet)
                {
                    LoadTileset(tempTileset);
                }

                // Sets the selected tileset value
                currentSelectedTileSet = tempTileset;
                
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Allez À", "MiniButton", GUILayout.Width(70f), GUILayout.Height(15f)))
                {
                    // Focus on Tileset
                    Sprite curTileset = tilesetSpritesList[currentSelectedTileSet];
                    Selection.activeObject = curTileset;
                }

                GUILayout.Space(15f);
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(4f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10f);

                var tools = Enum.GetValues(typeof(MapperToolsStateEnum));
                foreach (var tool in tools)
                {
                    GUI.color = toolsState.Equals((MapperToolsStateEnum)tool) ? Color.green : Color.white;
                    if (GUILayout.Button(tool.ToString()))
                    {
                        toolsState = (MapperToolsStateEnum)tool;
                    }
                    GUI.color = Color.white;
                }

                GUILayout.Space(10f);
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(5f);
                EditorGUILayout.EndVertical();
            }
            catch (Exception e) { }
        }

        static void DrawMapperWindow_Options()
        {
            try
            {
                //Causes an error on editor load if the window is visible.
                //This seems to be a problem with how the gui is drawn the first
                //loop of the script. It only happens the once, and I can't figure
                //out why. I've been trying for literally weeks and still can't
                //find an answer. This is the only known bug, but it doesn't
                //stop the functionality of the script in any way, and only serves
                //as an annoying message on unity load or this script being 
                //recompiled. Sorry for this bug. I am looking for a way to remove
                //this error, but I really am stummped as to why it's happening
                //and I just can not find an answer online.

                EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

                GUILayout.Label("Options", CustomEditorUtilities.TitleLabelStyle);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(25f);

                generateCollider =
                    CustomEditorUtilities.ButtonToggle(
                        "Tuiles Solide",
                        generateCollider);

                GUILayout.Space(15f);
                EditorGUILayout.EndHorizontal();

                /*
                if (generateCollider)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(25f);
                    
                    isPlatform =
                        CustomEditorUtilities.ButtonToggle(
                            "Is Platform",
                            isPlatform);

                    GUILayout.Space(15f);
                    EditorGUILayout.EndHorizontal();
                }
                else if (!generateCollider && isPlatform)
                {
                    isPlatform = false;
                }
                */
                
                GUILayout.Space(5f);
                EditorGUILayout.EndVertical();
            }
            catch (Exception e) { }
        }

        static void DrawMapperWindow_TilesPreviews()
        {
            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

            index_a = 0;
            int columnCount = Mathf.RoundToInt((window.position.width) / (settings.TilesPreviewReferences[settings.TilesPreviewSize] + 7)) - 2;
            //figures out how many columns are required for the tileset
            index_b = 0;
            int current = 0;

            tilePreviewScrollPosition = GUILayout.BeginScrollView(tilePreviewScrollPosition, false, false, GUILayout.Width(window.position.width - 22));
            EditorGUILayout.BeginHorizontal();

            for (int q = 0; q < tilesetSpritesList.Length; q++)
            {
                Sprite child = tilesetSpritesList[q];
                try
                {
                    if (child.texture.name == tilesetsNames[currentSelectedTileSet] && child.name != tilesetsNames[currentSelectedTileSet])
                    {
                        //if it's the tiles inside the image, not the entire image
                        Rect newRect = 
                            new Rect(
                                child.rect.x / child.texture.width, 
                                child.rect.y / child.texture.height, 
                                child.rect.width / child.texture.width, 
                                child.rect.height / child.texture.height);

                        //gets the x and y position in pixels of where the image is. Used later for display.
                        if (GUILayout.Button(
                            "", 
                            CustomEditorUtilities.PreviewBoxStyle,
                            GUILayout.Width(settings.TilesPreviewReferences[settings.TilesPreviewSize] + 2), 
                            GUILayout.Height(settings.TilesPreviewReferences[settings.TilesPreviewSize] + 2)))
                        {
                            if (currentSelectedTilesIndexes != null && !mapperEvent.control)
                            {
                                //empty the selected tile list if control isn't held. Allows multiselect of tiles.
                                currentSelectedTilesIndexes.Clear();
                                currentSelectedTilesSprites.Clear();
                            }
                            

                            currentSelectedTilesIndexes.Add(current);
                            currentSelectedTilesSprites.Add(loadedTilesetsList[current]);
                        }
                        

                        // Draws tile base on pixels gotten at the beginning of the loop
                        GUI.DrawTextureWithTexCoords(
                            new Rect(
                                5 + (index_b * (settings.TilesPreviewReferences[settings.TilesPreviewSize] + 6)), 
                                4 + (index_a * (settings.TilesPreviewReferences[settings.TilesPreviewSize] + 5)), 
                                settings.TilesPreviewReferences[settings.TilesPreviewSize], 
                                settings.TilesPreviewReferences[settings.TilesPreviewSize]),
                            child.texture, 
                            newRect, 
                            true);

                        if (currentSelectedTilesIndexes != null && currentSelectedTilesIndexes.Contains(current))
                        {
                            //if the current tile is inside of the list of selected tiles, draw a highlight indicator over the button.
                            if (tilePreviewSelectionColor == null)
                            {
                                tilePreviewSelectionColor = new Texture2D(1, 1);
                                tilePreviewSelectionColor.alphaIsTransparency = true;
                                tilePreviewSelectionColor.filterMode = FilterMode.Point;
                                tilePreviewSelectionColor.SetPixel(0, 0, new Color(.5f, .5f, 1f, .5f));
                                tilePreviewSelectionColor.Apply();
                            }

                            GUI.DrawTexture(
                                new Rect(
                                    5 + (index_b * (settings.TilesPreviewReferences[settings.TilesPreviewSize] + 6)),
                                    4 + (index_a * (settings.TilesPreviewReferences[settings.TilesPreviewSize] + 5)),
                                    settings.TilesPreviewReferences[settings.TilesPreviewSize],
                                    settings.TilesPreviewReferences[settings.TilesPreviewSize]), 
                                tilePreviewSelectionColor, 
                                ScaleMode.ScaleToFit, 
                                true);
                        }

                        if (index_b < columnCount)
                        {
                            index_b++;
                        }
                        else
                        {
                            // if we have enough columns to fill the scroll area, reset the column count and start a new line of buttons
                            index_b = 0;
                            index_a++;
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                        }
                        current++;
                    }
                }
                catch (System.Exception ex)
                {
                    if (ex.Message.StartsWith("IndexOutOfRangeException"))
                    {
                        Debug.Log("Tileset index was out of bounds, reloading and trying again.");
                        OnEnable();
                        return;
                    }
                }
            }
            
            EditorGUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        void DrawSettingsWindow()
        {
            EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);
            GUILayout.Space(5f);

            GUILayout.Label("Editor Options", CustomEditorUtilities.TitleLabelStyle);

            bool oldValueAsUtil = settings.AsUtilityWindow;
            settings.AsUtilityWindow =
                CustomEditorUtilities.ButtonToggle(
                    "As Utility Window",
                    settings.AsUtilityWindow);
            
            if (settings.AsUtilityWindow != oldValueAsUtil)
            {
                shouldDestroyWindows = true;
                OnEnable();
            }

            GUILayout.Space(10f);

            EditorGUI.BeginChangeCheck();
            
            GUILayout.Label("Tiles Preview", CustomEditorUtilities.TitleLabelStyle);

            settings.TilesPreviewSize =
                (TilesetsMapper_Settings.TilesPreviewSizeEnum)
                    CustomEditorUtilities.DrawEnumPopup(
                        "Preview Size",
                        settings.TilesPreviewSize);

            GUILayout.Space(10f);

            /*

            GUILayout.Label("Layer Highlight", CustomEditorUtilities.TitleLabelStyle);

            settings.HighlightColor =
                CustomEditorUtilities.DrawEditableColorField(
                    "Highlight Color",
                    settings.HighlightColor);

            GUILayout.Space(10f);

            */

            /*

            GUILayout.Label("Platform Collision Parameters", CustomEditorUtilities.TitleLabelStyle);

            settings.PlatformLayers =
                CustomEditorUtilities.DrawLayerMaskField(
                    "Collison Layers",
                    settings.PlatformLayers,
                    true,
                    "The Layers which the platform will let you pass by the bottom");

            settings.UseSurfaceArc =
                CustomEditorUtilities.ButtonToggle(
                    "Use Surface Arc",
                    settings.UseSurfaceArc);

            if (settings.UseSurfaceArc)
            {
                settings.SurfaceArcValue =
                    CustomEditorUtilities.DrawEditableFloatSlider(
                        "Surface Arc Value",
                        settings.SurfaceArcValue,
                        0f,
                        360f);
            }

            settings.UseSideBounce =
                CustomEditorUtilities.ButtonToggle(
                    "Use Side Bounce",
                    settings.UseSideBounce);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
            }

            */

            GUILayout.Space(10f);

            GUI.color = Color.yellow;
            EditorGUILayout.BeginVertical("Box");
            GUILayout.Label("L'option Clean TilesetsMapperObject est lourde et peut prendre plusieurs minutes afin de s'éxécuter", CustomEditorUtilities.LabelWrapStyle);
            EditorGUILayout.EndVertical();
            GUI.color = Color.white;

            if (GUILayout.Button("Clean TilesetMapperObject"))
            {
                CleanTilesetMapperObject();
            }


            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();

            EditorUtility.SetDirty(settings);
        }

        void DrawHelpWindow()
        {
        }

        #endregion

        #region LAYERS

        static void DrawLayersBox()
        {
            try
            {
                EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

                //Display all of the layersList. May be put into a foldout for if there are too many layersList. Haven't decided yet.
                GUILayout.Label("Layers:", CustomEditorUtilities.TitleLabelStyle);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(25f);

                if (GUILayout.Button("Add Layer"))
                {
                    AddLayer();
                }


                GUILayout.Space(25f);
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(10f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(25f);

                highlightLayer =
                    CustomEditorUtilities.ButtonToggle(
                        "Highlight Current",
                        highlightLayer);


                //EditorGUILayout.LabelField("Highlight Current Layer", GUILayout.Width(150));
                //highlightLayer = EditorGUILayout.Toggle(highlightLayer, GUILayout.Width(25));

                GUILayout.Space(25f);
                EditorGUILayout.EndHorizontal();


                GUILayout.Space(10f);

                String[] operatorChar = {"-", "+", "x", "r"};

                ResetLayers();
                layersList = ResortLayers(layersList);
                //Sort the layersList based on their sorting order instead of name
                int destroyFlag = -1;

                EditorGUILayout.BeginVertical(CustomEditorUtilities.FrameBoxStyle);

                for (int i = 0; i < layersList.Count; i++)
                {
                    //iterates through layersList and displays gui for options.
                    EditorGUILayout.BeginHorizontal();

                    RectOffset tmpPadding = GUI.skin.button.padding;
                    GUI.skin.button.padding = new RectOffset(3, 3, 3, 3);

                    if (layersList[i].gameObject.activeSelf)
                    {
                        if (GUILayout.Button(texVisible, GUILayout.Width(15), GUILayout.Height(15)))
                        {
                            layersList[i].gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button(texHidden, GUILayout.Width(15), GUILayout.Height(15)))
                        {
                            layersList[i].gameObject.SetActive(true);
                        }
                    }
                    GUI.skin.button.padding = tmpPadding;

                    if (i == currentLayerIndex)
                    {
                        //if selected layer, draw checked checkbox to show it's selected
                        //if (i != editionState)
                        //{
                            EditorGUILayout.ToggleLeft(layersList[i].name + " - " + layersList[i].GetComponent<SpriteRenderer>().sortingOrder, true);
                        //}
                        //else
                        //{
                        //    layersList[i].name = EditorGUILayout.TextField(layersList[i].name);
                        //}
                    }
                    else
                    {
                        //if not the selected layer, and is clicked, set it as the selected layer
                        //if (i != editionState)
                        //{
                            if (EditorGUILayout.ToggleLeft(layersList[i].name + " - " + layersList[i].GetComponent<SpriteRenderer>().sortingOrder, false))
                            {
                                currentLayerIndex = i;
                            }
                        //}
                        //else
                        //{
                        //    layersList[i].name = EditorGUILayout.TextField(layersList[i].name);
                        //}
                    }

                    //sets pressed value to -1 if nothing is pressed.
                    int pressed = GUILayout.Toolbar(-1, operatorChar);

                    switch (pressed)
                    {
                        case 0:
                            if (i > 0)
                            {
                                //moves layer and all tiles in it to move away from the camera, and moves the layer above it toward the camera.
                                layersList[i - 1].GetComponent<SpriteRenderer>().sortingOrder += 1;
                                int upLayer = layersList[i - 1].GetComponent<SpriteRenderer>().sortingOrder;

                                foreach (SpriteRenderer sr in layersList[i - 1].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = upLayer;
                                }

                                //layersList[i].GetComponent<SpriteRenderer>().sortingOrder -= 1;
                                int downLayer = layersList[i].GetComponent<SpriteRenderer>().sortingOrder -= 1;

                                foreach (SpriteRenderer sr in layersList[i].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = downLayer;
                                }
                                currentLayerIndex = i - 1;
                            }
                            layersList = ResortLayers(layersList);
                            break;

                        case 1:
                            if (i < layersList.Count - 1)
                            {
                                //moves layer and all tiles in it to move toward the camera, and moves the layer above it away from the camera.
                                layersList[i + 1].GetComponent<SpriteRenderer>().sortingOrder -= 1;
                                int upLayer = layersList[i + 1].GetComponent<SpriteRenderer>().sortingOrder;

                                foreach (SpriteRenderer sr in layersList[i + 1].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = upLayer;
                                }

                                //layersList[i].GetComponent<SpriteRenderer>().sortingOrder += 1;
                                int downLayer = layersList[i].GetComponent<SpriteRenderer>().sortingOrder += 1;

                                foreach (SpriteRenderer sr in layersList[i].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = downLayer;
                                }
                                currentLayerIndex = i + 1;
                            }
                            layersList = ResortLayers(layersList);
                            break;

                        case 2:
                            //deletes the layer game object, which also deletes all the children
                            destroyFlag = i;
                            break;
                    }
                    EditorGUILayout.EndHorizontal(); //end the layer gui
                }

                EditorGUILayout.EndVertical();

                if (currentLayerIndex <= layersList.Count - 1 && currentLayerIndex > 0)
                {
                    //double check to make certain a layer of some sort is selected and is in valid range
                    currentLayerObject = layersList[currentLayerIndex].gameObject;
                }

                if (currentLayerIndex <= layersList.Count - 1 && layersList[currentLayerIndex] != null)
                {
                    ResetHighlight(layersList[currentLayerIndex].gameObject, highlightLayer);
                    currentLayerObject = layersList[currentLayerIndex].gameObject;
                }
                else
                {
                    if (layersList.Count - 1 > 0 && layersList[currentLayerIndex] != null)
                    {
                        currentLayerObject = layersList[currentLayerIndex].gameObject;
                    }
                }

                if (destroyFlag != -1)
                {
                    DestroyImmediate(layersList[destroyFlag].gameObject);
                    return;
                    //Breaks method to not have errors down the line. Forces reload of tilesets to keep inside the bounds of the array.
                }

                EditorGUILayout.EndVertical();
            }
            catch (Exception e)
            {
            }
        }

        static void AddLayer()
        {
            //Creates new layer, renames it, and sets it's proper layer settings
            layersList.Add(new GameObject().transform);
            int index = layersList.Count - 1;
            layersList[index].name = "New Layer";
            layersList[index].transform.parent = sceneObject.transform;
            SpriteRenderer tmpRenderer = layersList[index].gameObject.AddComponent<SpriteRenderer>();
            tmpRenderer.sortingOrder = index;
        }

        static void ResetLayers()
        {
            layersList.Clear(); //Rebuilds a list of all of the layersList.

            foreach (Transform t in sceneObject.GetComponentsInChildren(typeof (Transform), true))
            {
                //gets a list of all possible layersList, and checks to see if the parent is the main game object for the tiles.
                if (t.parent == sceneObject.transform)
                {
                    layersList.Add(t);
                }
            }
        }

        static List<Transform> ResortLayers(List<Transform> layers)
        {
            //sorts layersList based on the sorting order
            layers.Sort(delegate(Transform x, Transform y)
            {
                int sortOrderX = x.GetComponent<SpriteRenderer>().sortingOrder;
                int sortOrderY = y.GetComponent<SpriteRenderer>().sortingOrder;
                return sortOrderX.CompareTo(sortOrderY);
            });
            return layers;
        }

        #endregion

        #region TILESET

        static void LoadTileset(int tileSetID)
        {
            currentSelectedTileSet = tileSetID;

            currentSelectedTilesIndexes = new List<int>();
            currentSelectedTilesSprites = new List<Sprite>();

            //loads the tilesets into proper variables
            ResetMapper();

            loadedTilesetsList = new Sprite[tilesetSpritesList.Length];

            int curCount = 0;
            int i = 0;

            //sets the displayed tileset based on the name of the tile. Also counts the number of tiles in the new tileset that's loaded.
            for(i = 0; i < tilesetSpritesList.Length; i++)
            {
                if(tilesetSpritesList[i].texture.name == tilesetTexturesList[tileSetID].name)
                {
                    loadedTilesetsList[curCount] = tilesetSpritesList[i];
                    curCount++;
                }
            }

            //resizes the displayed tileset's array size to match the new size
            Sprite[] tmpSprite = new Sprite[curCount];
            for(i = 0; i < curCount; i++)
            {
                tmpSprite[i] = loadedTilesetsList[i];
            }
            loadedTilesetsList = tmpSprite;

            // Select the first one in the list as a default selection
            if(tilesetsNames == null)
            {
                currentSelectedTilesIndexes.Add(0);
                currentSelectedTilesSprites.Add(tilesetSpritesList[0]);
            }
            else
            {
                for(i = 0; i < tilesetSpritesList.Length; i++)
                {
                    Sprite child = tilesetSpritesList[i];
                    
                    if(child.texture.name == tilesetsNames[currentSelectedTileSet] && child.name != tilesetsNames[currentSelectedTileSet])
                    {
                        currentSelectedTilesIndexes.Add(i);
                        currentSelectedTilesSprites.Add(tilesetSpritesList[i]);

                        break;
                    }
                }
            }
        }

        static GameObject GenerateTile(float x, float y)
        {
            string tileName = "Tile|" + (Mathf.Floor(x) + .5f) + "|" + (Mathf.Floor(y) + .5f);

            // Try to Get the current tile at this position if it exist
            if (currentLayerObject == null)
            {
                currentLayerObject = layersList[currentLayerIndex].gameObject;
            }

            Transform[] children = currentLayerObject.GetComponentsInChildren<Transform>();
            if (children != null)
            {
                foreach (Transform current in children)
                {
                    if (current.name == tileName && current.parent == currentLayerObject.transform)
                    {
                        Undo.DestroyObjectImmediate(current.gameObject);
                    }
                }
            }


            // Create a new tile
            GameObject tileObj = new GameObject(tileName);
            SpriteRenderer sprRenderer = tileObj.AddComponent<SpriteRenderer>();

            // Be sure the Layer is Ok
            if (currentLayerIndex > layersList.Count - 1)
            {
                currentLayerIndex = layersList.Count - 1;
                ResetLayers();
                layersList = ResortLayers(layersList);
            }

            // Setup the tile
            tileObj.transform.parent = layersList[currentLayerIndex];
            tileObj.transform.localScale = new Vector3(1, 1, 1);

            // Adjust sorting layer to the same of other sprite on this layer
            tileObj.GetComponent<SpriteRenderer>().sortingOrder = layersList[currentLayerIndex].GetComponent<SpriteRenderer>().sortingOrder;
            tileObj.layer = LayerMask.NameToLayer("Default");
            PolygonCollider2D tileCollider = tileObj.GetComponent<PolygonCollider2D>();

            // If no Collider was found and it suppose to have a collider, create one
            if (tileCollider == null && generateCollider)
            {
                tileCollider = tileObj.AddComponent<PolygonCollider2D>();
                Vector2[] points =
                {
                    new Vector2(-.5f, .5f),
                    new Vector2(.5f, .5f),
                    new Vector2(.5f, -.5f),
                    new Vector2(-.5f, -.5f)
                };

                tileCollider.points = points;
            }

            // If the tile have a collider and not suppose to, remove them
            if (tileCollider != null && !generateCollider)
            {
                Undo.DestroyObjectImmediate(tileCollider);
            }
            
            //Repaint();
            SceneView.RepaintAll();
            return tileObj;
        }

        #endregion

        #region HIGHLIGHT

        static void ResetHighlight(GameObject layer, bool status)
        {
            //highlights the layer in red if status is true, unhighlights if false
            foreach (SpriteRenderer sr in sceneObject.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1, 1, 1, 1);
            }
            foreach (SpriteRenderer sr in layersList[currentLayerIndex].GetComponentsInChildren<SpriteRenderer>())
            {
                if (status)
                {
                    sr.color = settings.HighlightColor;
                }
                else
                {
                    sr.color = new Color(1, 1, 1, 1);
                }
            }
        }

        #endregion

        static Color32[] RotateMatrix(Color32[] matrix, int n)
        {
            Color32[] ret = new Color32[n * n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ret[i * n + j] = matrix[(n - j - 1) * n + i];
                }
            }

            return ret;
        }

        static void CleanTilesetMapperObject()
        {
            if (currentLayerObject == null)
            {
                currentLayerObject = layersList[currentLayerIndex].gameObject;
            }

            Transform[] children = currentLayerObject.GetComponentsInChildren<Transform>();
            if (children != null)
            {
                foreach (Transform current in children)
                {
                    if (current == null) continue;
                    string currentTileName = current.name;

                    foreach (Transform next in children)
                    {
                        if (next == null) continue;

                        if (current != next && currentTileName.Equals(next.name))
                        {
                            DestroyImmediate(next.gameObject);
                        }
                    }
                }
            }

            EditorUtility.SetDirty(sceneObject);
            SceneView.RepaintAll();
            AssetDatabase.Refresh();
            EditorSceneManager.SaveOpenScenes();
        }
    }
}
#endif
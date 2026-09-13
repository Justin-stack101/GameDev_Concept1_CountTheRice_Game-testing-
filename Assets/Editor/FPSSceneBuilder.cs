#pragma warning disable CS0618
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.AI;
using System.IO;

/// <summary>
/// Automatic FPS Level Builder for IT2107 Lab 1.
/// Generates:
/// - 2 connected rooms with hallway (Lab Task 2)
/// - Custom materials with contrasting colors (Lab Task 3)
/// - Functional Player with First-Person camera, movement, and shooting (Lab Task 1)
/// - Bullet Prefab with physics and damage
/// - Enemy AI bot that patrols/chases on NavMesh (Lab Task 4)
/// - Auto-bakes NavMesh and registers scene in Build Settings
/// </summary>
public class FPSSceneBuilder : EditorWindow
{
    [MenuItem("FPS Lab/Build Complete FPS Scene")]
    public static void BuildScene()
    {
        // 1. Ensure folders exist
        EnsureDirectory("Assets/Materials");
        EnsureDirectory("Assets/Prefabs");
        EnsureDirectory("Assets/Scenes");

        // 2. Ensure "Player" tag exists
        EnsurePlayerTag();

        // 3. Create or load Materials
        Material floorMat = GetOrCreateMaterial("Assets/Materials/Mat_Floor.mat", new Color(0.22f, 0.24f, 0.28f));
        Material room1Mat = GetOrCreateMaterial("Assets/Materials/Mat_Room1_Wall.mat", new Color(0.85f, 0.85f, 0.88f));
        Material room2Mat = GetOrCreateMaterial("Assets/Materials/Mat_Room2_Wall.mat", new Color(0.12f, 0.48f, 0.78f)); // Blue theme
        Material enemyMat = GetOrCreateMaterial("Assets/Materials/Mat_Enemy.mat", new Color(0.92f, 0.28f, 0.15f));      // Red/Orange
        Material bulletMat = GetOrCreateMaterial("Assets/Materials/Mat_Bullet.mat", new Color(1.0f, 0.85f, 0.1f));     // Yellow glowing

        // 4. Create Bullet Prefab
        GameObject bulletPrefab = CreateBulletPrefab(bulletMat);

        // 5. Create new scene or clear existing
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Create Root Level Container
        GameObject levelRoot = new GameObject("=== LEVEL ENVIRONMENT ===");

        // --- ROOM 1 (Main Spawn Room: 20m x 20m) ---
        GameObject r1 = new GameObject("Room_1_Spawn");
        r1.transform.SetParent(levelRoot.transform);

        CreateBlock("Floor_Room1", new Vector3(0, -0.1f, 0), new Vector3(20, 0.2f, 20), floorMat, r1.transform, true);
        CreateBlock("Wall_South", new Vector3(0, 2f, -10f), new Vector3(20, 4f, 0.6f), room1Mat, r1.transform, true);
        CreateBlock("Wall_West", new Vector3(-10f, 2f, 0), new Vector3(0.6f, 4f, 20), room1Mat, r1.transform, true);
        CreateBlock("Wall_East", new Vector3(10f, 2f, 0), new Vector3(0.6f, 4f, 20), room1Mat, r1.transform, true);
        // North wall with doorway
        CreateBlock("Wall_North_Left", new Vector3(-6.5f, 2f, 10f), new Vector3(7f, 4f, 0.6f), room1Mat, r1.transform, true);
        CreateBlock("Wall_North_Right", new Vector3(6.5f, 2f, 10f), new Vector3(7f, 4f, 0.6f), room1Mat, r1.transform, true);
        CreateBlock("Wall_North_Top", new Vector3(0, 3.5f, 10f), new Vector3(6f, 1f, 0.6f), room1Mat, r1.transform, true);

        // --- CORRIDOR (Connects Room 1 to Room 2: 8m long, 6m wide) ---
        GameObject corridor = new GameObject("Corridor");
        corridor.transform.SetParent(levelRoot.transform);

        CreateBlock("Floor_Corridor", new Vector3(0, -0.1f, 14f), new Vector3(6, 0.2f, 8), floorMat, corridor.transform, true);
        CreateBlock("Wall_Corridor_West", new Vector3(-3f, 2f, 14f), new Vector3(0.6f, 4f, 8), room1Mat, corridor.transform, true);
        CreateBlock("Wall_Corridor_East", new Vector3(3f, 2f, 14f), new Vector3(0.6f, 4f, 8), room1Mat, corridor.transform, true);

        // --- ROOM 2 (Lab Task 2: Second Room - 20m x 20m with Lab Task 3: Blue Theme) ---
        GameObject r2 = new GameObject("Room_2_EnemyZone");
        r2.transform.SetParent(levelRoot.transform);

        CreateBlock("Floor_Room2", new Vector3(0, -0.1f, 28f), new Vector3(20, 0.2f, 20), floorMat, r2.transform, true);
        CreateBlock("Wall_North", new Vector3(0, 2f, 38f), new Vector3(20, 4f, 0.6f), room2Mat, r2.transform, true);
        CreateBlock("Wall_West", new Vector3(-10f, 2f, 28f), new Vector3(0.6f, 4f, 20), room2Mat, r2.transform, true);
        CreateBlock("Wall_East", new Vector3(10f, 2f, 28f), new Vector3(0.6f, 4f, 20), room2Mat, r2.transform, true);
        // South wall with doorway
        CreateBlock("Wall_South_Left", new Vector3(-6.5f, 2f, 18f), new Vector3(7f, 4f, 0.6f), room2Mat, r2.transform, true);
        CreateBlock("Wall_South_Right", new Vector3(6.5f, 2f, 18f), new Vector3(7f, 4f, 0.6f), room2Mat, r2.transform, true);
        CreateBlock("Wall_South_Top", new Vector3(0, 3.5f, 18f), new Vector3(6f, 1f, 0.6f), room2Mat, r2.transform, true);

        // Obstacles / Cover pillars in Room 2
        CreateBlock("Pillar_Left", new Vector3(-4f, 1.5f, 26f), new Vector3(2f, 3f, 2f), room2Mat, r2.transform, true);
        CreateBlock("Pillar_Right", new Vector3(4f, 1.5f, 30f), new Vector3(2f, 3f, 2f), room2Mat, r2.transform, true);

        // --- 6. CREATE PLAYER ---
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = new Vector3(0, 1f, -4f);

        CharacterController cc = player.AddComponent<CharacterController>();
        cc.height = 2f;
        cc.radius = 0.5f;
        cc.center = new Vector3(0, 0, 0);

        PlayerHealth playerHp = player.AddComponent<PlayerHealth>();
        playerHp.maxHealth = 100f;

        FPSController fpsCtrl = player.AddComponent<FPSController>();
        fpsCtrl.moveSpeed = 6f;
        fpsCtrl.jumpForce = 5f;
        fpsCtrl.mouseSensitivity = 2f;
        fpsCtrl.bulletSpeed = 35f;
        fpsCtrl.fireRate = 0.2f;
        fpsCtrl.bulletPrefab = bulletPrefab;

        // Player Camera
        Camera existingCam = Camera.main;
        GameObject camObj = (existingCam != null) ? existingCam.gameObject : new GameObject("Main Camera", typeof(Camera), typeof(AudioListener));
        camObj.transform.SetParent(player.transform);
        camObj.transform.localPosition = new Vector3(0, 0.65f, 0);
        camObj.transform.localRotation = Quaternion.identity;
        fpsCtrl.cameraTransform = camObj.transform;

        // Fire Point (at muzzle position)
        GameObject firePoint = new GameObject("FirePoint");
        firePoint.transform.SetParent(camObj.transform);
        firePoint.transform.localPosition = new Vector3(0.2f, -0.15f, 0.6f);
        firePoint.transform.localRotation = Quaternion.identity;
        fpsCtrl.firePoint = firePoint.transform;

        // --- 7. CREATE ENEMY BOT ---
        GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        enemy.name = "Enemy_Bot";
        enemy.transform.position = new Vector3(0, 1.1f, 30f);
        enemy.GetComponent<Renderer>().sharedMaterial = enemyMat;

        NavMeshAgent agent = enemy.AddComponent<NavMeshAgent>();
        agent.speed = 4f;
        agent.stoppingDistance = 1.5f;
        agent.radius = 0.5f;
        agent.height = 2f;

        EnemyAI enemyAI = enemy.AddComponent<EnemyAI>();
        enemyAI.detectionRange = 18f;
        enemyAI.attackRange = 2.2f;
        enemyAI.attackDamage = 15f;
        enemyAI.player = player.transform;

        EnemyHealth enemyHp = enemy.AddComponent<EnemyHealth>();
        enemyHp.maxHealth = 50f;

        // --- 8. LIGHTING ---
        Light dirLight = Object.FindFirstObjectByType<Light>();
        if (dirLight == null)
        {
            GameObject lightObj = new GameObject("Directional Light", typeof(Light));
            dirLight = lightObj.GetComponent<Light>();
            dirLight.type = LightType.Directional;
        }
        dirLight.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        dirLight.intensity = 1.1f;
        dirLight.color = Color.white;

        // --- 9. BAKE NAVMESH ---
        try
        {
            UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
            Debug.Log("[FPS Lab] NavMesh baked successfully!");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("[FPS Lab] Auto NavMesh bake skipped: " + ex.Message + ". You can bake via Window > AI > Navigation.");
        }

        // --- 10. SAVE SCENE & ADD TO BUILD SETTINGS ---
        string scenePath = "Assets/Scenes/FPS_Level.unity";
        EditorSceneManager.SaveScene(scene, scenePath);
        Debug.Log("[FPS Lab] Scene successfully saved to: " + scenePath);

        // Register in Build Settings
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[]
        {
            new EditorBuildSettingsScene(scenePath, true)
        };
        EditorBuildSettings.scenes = scenes;
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("FPS Scene Built!", 
            "Success! The entire FPS demonstration level has been generated:\n\n" +
            "• Room 1 & Room 2 with corridor (Task 2)\n" +
            "• Custom materials & colors (Task 3)\n" +
            "• Player with Camera & Gun setup (Task 1)\n" +
            "• Enemy AI Bot on NavMesh (Task 4)\n\n" +
            "Press the Play [▶] button at the top to test right away!", "Awesome!");
    }

    private static GameObject CreateBlock(string name, Vector3 pos, Vector3 scale, Material mat, Transform parent, bool isStatic)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = name;
        cube.transform.position = pos;
        cube.transform.localScale = scale;
        cube.transform.SetParent(parent);
        if (mat != null) cube.GetComponent<Renderer>().sharedMaterial = mat;
        if (isStatic) GameObjectUtility.SetStaticEditorFlags(cube, StaticEditorFlags.NavigationStatic);
        return cube;
    }

    private static GameObject CreateBulletPrefab(Material bulletMat)
    {
        string path = "Assets/Prefabs/Bullet.prefab";
        GameObject existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (existing != null) return existing;

        GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        temp.name = "Bullet";
        temp.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        if (bulletMat != null) temp.GetComponent<Renderer>().sharedMaterial = bulletMat;

        Rigidbody rb = temp.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        temp.AddComponent<Bullet>();

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(temp, path);
        Object.DestroyImmediate(temp);
        AssetDatabase.SaveAssets();
        return prefab;
    }

    private static Material GetOrCreateMaterial(string path, Color color)
    {
        Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (mat == null)
        {
            Shader s = Shader.Find("Standard");
            if (s == null) s = Shader.Find("Universal Render Pipeline/Lit");
            if (s == null) s = Shader.Find("Diffuse");
            mat = new Material(s);
            mat.color = color;
            AssetDatabase.CreateAsset(mat, path);
        }
        return mat;
    }

    private static void EnsureDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            AssetDatabase.Refresh();
        }
    }

    private static void EnsurePlayerTag()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        bool found = false;
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Player")) { found = true; break; }
        }

        // "Player" is actually a built-in Unity tag, but just in case:
        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
            n.stringValue = "Player";
            tagManager.ApplyModifiedProperties();
        }
    }
}

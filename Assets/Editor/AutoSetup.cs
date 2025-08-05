using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Unity.Netcode;
using System.IO;

public class AutoSetup : EditorWindow
{
    [MenuItem("Tools/Auto Setup Shattered Star")]
    public static void SetupProject()
    {
        // Create folder structure if missing
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");

        if (!AssetDatabase.IsValidFolder("Assets/Textures"))
            AssetDatabase.CreateFolder("Assets", "Textures");

        // Create dummy sprites
        Texture2D tex = new Texture2D(32, 32);
        for (int y = 0; y < 32; y++)
            for (int x = 0; x < 32; x++)
                tex.SetPixel(x, y, Color.white);
        tex.Apply();

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
        AssetDatabase.CreateAsset(tex, "Assets/Textures/Dummy.png");

        // Create prefabs
        CreateSpritePrefab("Player", Color.blue, typeof(NetworkObject));
        CreateSpritePrefab("Boss", Color.red);
        CreateSpritePrefab("Wall", Color.gray);
        CreateSpritePrefab("Floor", Color.green);
        CreateEmptyPrefab("SpawnPoint");

        // Open scene or create one
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Create GameManager
        GameObject manager = new GameObject("DungeonLoader");
        manager.AddComponent<DungeonLoader>();
        var loader = manager.GetComponent<DungeonLoader>();
        loader.playerPrefab = LoadPrefab("Player");
        loader.bossPrefab = LoadPrefab("Boss");
        loader.wallPrefab = LoadPrefab("Wall");
        loader.floorPrefab = LoadPrefab("Floor");

        // Add spawn manager
        var spawnManager = manager.AddComponent<SpawnManager>();
        spawnManager.playerPrefab = LoadPrefab("Player");

        // Add spawn points
        spawnManager.spawnPoints = new Transform[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject sp = Instantiate(LoadPrefab("SpawnPoint"), new Vector3(i * 2, 0, 0), Quaternion.identity);
            sp.name = $"SpawnPoint_{i}";
            spawnManager.spawnPoints[i] = sp.transform;
        }

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/AutoSetupScene.unity");
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("Shattered Star", "Setup complete! Scene saved in Assets/Scenes", "OK");
    }

    private static void CreateSpritePrefab(string name, Color color, System.Type extraType = null)
    {
        GameObject go = new GameObject(name);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.color = color;
        if (extraType != null)
            go.AddComponent(extraType);
        SaveAsPrefab(go, name);
    }

    private static void CreateEmptyPrefab(string name)
    {
        GameObject go = new GameObject(name);
        SaveAsPrefab(go, name);
    }

    private static void SaveAsPrefab(GameObject go, string name)
    {
        string path = $"Assets/Prefabs/{name}.prefab";
        PrefabUtility.SaveAsPrefabAsset(go, path);
        GameObject.DestroyImmediate(go);
    }

    private static GameObject LoadPrefab(string name)
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/{name}.prefab");
    }
}

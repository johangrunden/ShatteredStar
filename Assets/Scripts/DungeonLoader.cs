using UnityEngine;
using System.IO;

public class DungeonLoader : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject playerPrefab;
    public GameObject bossPrefab;

    public float tileSize = 1f;

    void Start()
    {
        string path = Application.dataPath + "/Dungeon/DungeonLayout.txt";
        string[] lines = File.ReadAllLines(path);

        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                Vector3 pos = new Vector3(x * tileSize, -y * tileSize, 0);
                char tile = line[x];
                GameObject toSpawn = null;

                switch (tile)
                {
                    case '#':
                        toSpawn = wallPrefab;
                        break;
                    case '.':
                        toSpawn = floorPrefab;
                        break;
                    case 'P':
                        Instantiate(playerPrefab, pos, Quaternion.identity);
                        toSpawn = floorPrefab;
                        break;
                    case 'B':
                        Instantiate(bossPrefab, pos, Quaternion.identity);
                        toSpawn = floorPrefab;
                        break;
                }

                if (toSpawn != null)
                    Instantiate(toSpawn, pos, Quaternion.identity);
            }
        }
    }
}

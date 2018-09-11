using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmass : MonoBehaviour {

    // Objects to spawn via prefab
    public GameObject prefabTerrain;
    public GameObject settlements;
    public GameObject towns;

    public Transform player;
    
    //public LayerMask settlementMask;
        
    int planeSize = 10;                     // The default size of a plane in Unity is a 10x10 grid of squares.
    public int mapLength;                   // This is how many planes there are when connected together to make the overall size of the map. Defined in the inspector.
    public const float maxTilesSeen = 50f;  // Acts as the view distance. Once the player reaches this value the settlements is recalculated

    //Vector3 perviousSettlementPos;
    
    float townRandomPlacementX, townRandomPlacementZ;  

    Vector3 nextPlane;          // The position of the next plane to be added.
    Vector3 oldPlayerPosition;  // keep track where the player was when the terrain last generated

    // A list of planes (tiles) used to make up the maps.
    List<TerrainTile> newMap = new List<TerrainTile>();
    Hashtable terrain = new Hashtable();
    Hashtable newSettle = new Hashtable();

	// Use this for initialization
	void Start () {
        
        oldPlayerPosition = Vector3.zero;                   //Player starts here all the timed when the game is run

        float newTileIndex = Time.realtimeSinceStartup;     //Gives each piece of terrain its own unique identifier

        townRandomPlacementX = Random.Range((-player.transform.position.x - 150f), (player.transform.position.x + 150f));
        townRandomPlacementZ = Random.Range((-player.transform.position.z - 150f), (player.transform.position.z + 150f));
        

        //Random placement of the settlements
        for (int i =0; i < 5; i++)
        {
            Vector3 pos = new Vector3(i + Random.Range(-100, 100), 2, i + Random.Range(-100, 100));

            GameObject poi = Instantiate(settlements, pos, Quaternion.identity);

            string newName = "Settlement " + ((int)(pos.x)).ToString() + " , " + ((int)(pos.z)).ToString();
            poi.name = newName;
            Settlements s = new Settlements(poi, newTileIndex, transform);
            newSettle.Add(newName, s);
            
        }
       
        towns.transform.position = new Vector3(townRandomPlacementX, 2, townRandomPlacementZ);

        // Draw the starting terrain 
        for (int i = -mapLength; i < mapLength; i++)
        {
            for (int j = -mapLength; j < mapLength; j++)
            {
                Vector3 newPosition = new Vector3((i * planeSize + nextPlane.x), 0, (j * planeSize + nextPlane.z));

                GameObject newPlane = Instantiate(prefabTerrain, newPosition, Quaternion.identity);

                string terrainTileIndex = "Index at (" + ((int)(newPosition.x)).ToString() + " , " + ((int)(newPosition.z)).ToString() + ")";
                newPlane.name = terrainTileIndex;
                TerrainTile tilePlane = new TerrainTile(newPlane, newTileIndex, transform);
                terrain.Add(terrainTileIndex, tilePlane);   //add each piece to the hastable
            }
        }
        
    }

    private void Update()
    {
        // calculate how far the player is in relation to his last position when the terrain generated
        int generateX = (int)(player.transform.position.x - oldPlayerPosition.x);
        int generateY = (int)(player.transform.position.z - oldPlayerPosition.z);

        //if the moves moves a certain distance away...
        if (Mathf.Abs(generateX) > maxTilesSeen || Mathf.Abs(generateY) > maxTilesSeen)
        {
            float newTileIndex = Time.realtimeSinceStartup;

            // ... then spawn random settlements 
            for (int i =0; i<5; i++)
            {
                int generateDistanceX = (int)(player.position.x - settlements.transform.position.x);
                int generateDistanceZ = (int)(player.position.z - settlements.transform.position.z);

                Vector3 pos = new Vector3(i + Random.Range(-100, 100) + player.transform.position.x, 2, i + Random.Range(-100, 100) + player.transform.position.z);

                string newName = "Settlement " + ((int)(pos.x)).ToString() + " , " + ((int)(pos.z)).ToString();

                // if a settlement does not exists in the hashatble then add it
                if(!newSettle.ContainsKey(newName))
                {
                    GameObject poi = Instantiate(settlements, pos, Quaternion.identity);
                    poi.name = newName;
                    Settlements s = new Settlements(poi, newTileIndex, transform);
                    newSettle.Add(newName, s);
                }
                else
                {
                    // if a settlement already exists in the hashatble then update its index
                    (newSettle[newName] as Settlements).index = newTileIndex;
                }
                
            }

            // round down the x and z positions to the nearest integer 
            int planeX = (int)Mathf.Floor(player.transform.position.x / planeSize) * planeSize;
            int planeZ = (int)Mathf.Floor(player.transform.position.z / planeSize) * planeSize;

            for (int x = -mapLength; x < mapLength; x++)
            {
                for (int y = -mapLength; y < mapLength; y++)
                {
                    // move the piece one plane size in a given direction
                    Vector3 newPosition = new Vector3((x * planeSize + planeX), 0, (y * planeSize + planeZ));

                    string terrainTileIndex = "Index at (" + ((int)(newPosition.x)).ToString() + " , " + ((int)(newPosition.z)).ToString() + ")";

                    // if the piece does not exist then spawn it and add it to the hashtable
                    // if it does then keep it where it is and update its index
                    if (!terrain.ContainsKey(terrainTileIndex))
                    {
                        GameObject newPlane = Instantiate(prefabTerrain, newPosition, Quaternion.identity);
                        newPlane.name = terrainTileIndex;
                        TerrainTile infiniteTileMember = new TerrainTile(newPlane, newTileIndex, transform);
                        terrain.Add(terrainTileIndex, infiniteTileMember);

                    }
                    else
                    {
                        (terrain[terrainTileIndex] as TerrainTile).tileIndex = newTileIndex;
                    }
                }
            }

            // if a piece is too far away the destroy it, if not then add it to the temp hashtable
            Hashtable infiniteTerrain = new Hashtable();
            foreach (TerrainTile tt in terrain.Values)
            {
                if (tt.tileIndex != newTileIndex)
                {
                    Destroy(tt.newTile);
                }
                else
                {
                    infiniteTerrain.Add(tt.newTile.name, tt);
                }
            }

            Hashtable infiniteSettlements = new Hashtable();
            foreach (Settlements ss in newSettle.Values)
            {
                if (ss.index != newTileIndex)
                {
                    Destroy(ss.settlements);
                }
                else
                {
                    infiniteSettlements.Add(ss.settlements.name, ss);
                }
            }
            // combine the temp hashtable to the original one
            newSettle = infiniteSettlements;
            terrain = infiniteTerrain;
            oldPlayerPosition = player.transform.position;

        }
        
    }
   
}

// Represents each piece of terrain that mke up the map
public class TerrainTile
{
    public GameObject newTile;
    Bounds bounds;
    public Vector3 position;
    public float tileSize;
    public float tileIndex;

    public TerrainTile(Vector3 _position, float size)
    {
        position = _position;
        bounds = new Bounds(position, Vector3.one * size);

        //newTile.transform.position = position;
    }

    public TerrainTile(GameObject obj, float index, Transform parent)
    {
        newTile = obj;
        newTile.transform.parent = parent;
        tileIndex = index;
    }

    public void Updater()
    {
        float mapEdge = Mathf.Sqrt(bounds.SqrDistance(position));
        
    }


}
// represents each spawned cube
public class Settlements
{
    public GameObject settlements;
    public float index;

    public Settlements(GameObject s, float _index, Transform parent)
    {
        settlements = s;
        settlements.transform.parent = parent;
        index = _index;
    }
}

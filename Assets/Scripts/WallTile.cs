using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class WallTile : Tile
{
    public Sprite[] walls;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for( int y = -1; y <= 1;  y++ )
        {
            for( int x = -1; x <=1; x++)
            {
                Vector3Int location = new Vector3Int(position.x + x, position.y + y, position.z);
                if (HasTile(tilemap, location))
                {
                    tilemap.RefreshTile(location);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        int i_mask = HasTile(tilemap, position + new Vector3Int(0, 1, 0)) ? 1 : 0; // above
        i_mask += HasTile(tilemap, position + new Vector3Int(1, 0, 0)) ? 2 : 0;    // right
        i_mask += HasTile(tilemap, position + new Vector3Int(0, -1, 0)) ? 4 : 0;   // below
        i_mask += HasTile(tilemap, position + new Vector3Int(-1, 0, 0)) ? 8 : 0;   // left
        // all are valid, no need to convert mask to index
        //int index = GetIndex((byte)mask);

        if (i_mask >= 0 && i_mask <= walls.Length)
        {
            tileData.sprite = walls[i_mask];
            tileData.color = Color.white;
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = ColliderType.Sprite;
        }
    }

    private bool HasTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }
    /*
    private int GetIndex(byte mask)
    {

    }
    */

#if UNITY_EDITOR
    [MenuItem("Assets/Tile")]
    public static void CreateWallTiles()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Wall Tile", "New Wall Tile", "Asset", "Save Wall Tile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WallTile>(), path);
    }
#endif

}


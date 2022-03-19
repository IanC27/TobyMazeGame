using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeConstructor : MonoBehaviour
{

    [SerializeField]
    [Range(1, 50)]
    private int width = GameStats.CurrentParams.mazeSize;

    [SerializeField]
    [Range(1, 50)]
    private int height = GameStats.CurrentParams.mazeSize;

    [SerializeField]
    private WallTile wall;
    [SerializeField]
    private WallTile outerWall;

    public Tilemap wallMap;
    public Tilemap outerWallMap;

    public GameObject cannon;
    public Transform playerTransform;
    void Start()
    {
        width = GameStats.CurrentParams.mazeSize;
        height = GameStats.CurrentParams.mazeSize;
        var maze = MazeGenerator.Generate(width, height);
        mazeDraw(maze);
        outerBoxDraw(width, height, 3);
        var levelCannon = Instantiate(cannon, new Vector3(width*2 + 2.5f, height*2 + 2.5f, 0), Quaternion.identity);
        levelCannon.GetComponent<CannonFire>().target = playerTransform;
        levelCannon.GetComponent<CannonFire>().period = GameStats.CurrentParams.cannonPeriod;


    }

    private void mazeDraw(WallState[,] maze)
    {
        // Draw all corners
        Vector3Int origin = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        for (int i = 0; i < width + 1; ++i)
        {
            for (int j = 0; j < height + 1; ++j)
            {
                Vector3Int position = new Vector3Int(origin.x + i*2, origin.y + j*2, 0);
                wallMap.SetTile(position, wall);
            }
        }
        // Fill in with the walls

        for (int i=0; i < width; ++i)
        {
            for (int j=0; j < height; ++j)
            {
                var cell = maze[i, j];
                // Debug.LogFormat("{0}, {1}, {2}, {3}", i, width, j, height);
                // location of a "cell" tile with walls to be placed around
                var position = new Vector3Int(origin.x + i*2 + 1, origin.y + j*2 + 1, 0);
                if (cell.HasFlag(WallState.down))
                {
                    wallMap.SetTile(position + Vector3Int.down, wall);
                }
                if (cell.HasFlag(WallState.left))
                {
                    wallMap.SetTile(position + Vector3Int.left, wall);
                }
                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.right))
                    {
                        wallMap.SetTile(position + Vector3Int.right, wall);
                    }
                }
                if (j == height - 1)
                {
                    if (cell.HasFlag(WallState.up))
                    {
                        wallMap.SetTile(position + Vector3Int.up, wall);
                    }
                }
                
                
            }
        }
        
    }

    private void outerBoxDraw(int width, int height, int offset)
    {
        //width = width;
        //height = height;
        Vector3Int origin = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        Vector3Int drawPos = new Vector3Int (origin.x - offset, origin.y - offset , 0);
        Vector3Int deltaY = new Vector3Int(0, height*2 + offset*2, 0);
        Vector3Int deltaX = new Vector3Int(width*2 + offset*2, 0, 0);
        for (int i = 0; i < width*2 + offset*2; ++i)
        {
            outerWallMap.SetTile(drawPos, outerWall);
            outerWallMap.SetTile(drawPos + deltaY, outerWall);
            drawPos = drawPos + Vector3Int.right;
        }
        drawPos = drawPos - deltaX;
        for (int j = 0; j < height*2 + offset*2; ++j)
        {
            outerWallMap.SetTile(drawPos, outerWall);
            outerWallMap.SetTile(drawPos + deltaX, outerWall);
            drawPos = drawPos + Vector3Int.up;
        }
        outerWallMap.SetTile(drawPos + deltaX, outerWall);
        drawPos = drawPos - deltaY;
        drawPos = drawPos + new Vector3Int(width + offset, 1, 0);
        Vector3Int topside = new Vector3Int(0, height * 2 + offset - 1, 0);
        for (int p = 0; p < offset; ++p)
        {
            outerWallMap.SetTile(drawPos, outerWall);
            outerWallMap.SetTile(drawPos + topside, outerWall);
            drawPos = drawPos + Vector3Int.up;
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

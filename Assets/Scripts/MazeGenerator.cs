using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// algorithm adapted from here: https://github.com/gamedolphin/youtube_unity_maze
[Flags]
public enum WallState
{
    nil = 0,       // 0000
    up = 1,        // 0001
    right = 2,     // 0010
    down = 4,      // 0100
    left = 8,      // 1000

    visited = 128, // 1000 0000
}

public struct Position
{
    public int x;
    public int y;
}

public struct Neighbor
{
    public Position position;
    public WallState sharedWall;
}
public static class MazeGenerator
{

    public static List<Neighbor> GetUnvisitedNeighbors(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbor>();

        if (p.x > 0) // left
        {
            if (!maze[p.x - 1, p.y].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                {
                    position = new Position
                    {
                        x = p.x - 1,
                        y = p.y
                    },
                    sharedWall = WallState.left
                });
            }
        }
        if (p.y > 0) // down
        {
            if (!maze[p.x, p.y - 1].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                {
                    position = new Position
                    {
                        x = p.x,
                        y = p.y - 1
                    },
                    sharedWall = WallState.down
                });
            }
        }
        if (p.y < height - 1) // up
        {
            if (!maze[p.x, p.y + 1].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                {
                    position = new Position
                    {
                        x = p.x,
                        y = p.y + 1
                    },
                    sharedWall = WallState.up
                });
            }
        }
        if (p.x < width - 1) // right
        {
            if (!maze[p.x + 1, p.y].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                {
                    position = new Position
                    {
                        x = p.x + 1,
                        y = p.y
                    },
                    sharedWall = WallState.right
                });
            }
        }

        return list;
    }

    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.right: return WallState.left;
            case WallState.left: return WallState.right;
            case WallState.up: return WallState.down;
            case WallState.down: return WallState.up;
            default: return WallState.nil;
        }
    }

    public static WallState[,] DepthFirstBacktracker(WallState[,] maze, int width, int height)
    {
        var rng = new System.Random();
        var pos_stk = new Stack<Position>();
        var position = new Position { x = rng.Next(0, width), y = rng.Next(0, height) };

        maze[position.x, position.y] |= WallState.visited; // 1000 1111;
        pos_stk.Push(position);

        while (pos_stk.Count > 0)
        {
            Position curr = pos_stk.Pop();
            List<Neighbor> neighbors = GetUnvisitedNeighbors(curr, maze, width, height);

            if (neighbors.Count > 0)
            {
                pos_stk.Push(curr);

                int rand_i = rng.Next(0, neighbors.Count);
                var rand_neighbor = neighbors[rand_i];

                var n_pos = rand_neighbor.position;
                // remove walls between current and neighbor
                maze[curr.x, curr.y] &= ~rand_neighbor.sharedWall;
                maze[n_pos.x, n_pos.y] &= ~GetOppositeWall(rand_neighbor.sharedWall);

                maze[n_pos.x, n_pos.y] |= WallState.visited;

                pos_stk.Push(n_pos);
            }

        }
        
        return maze;
    }
    public static WallState[,] Generate(int width, int height)
    {
        // initialize and fill maze
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.right | WallState.left | WallState.up | WallState.down;
        for (uint i = 0; i < width; ++i)
        {
            for (uint j = 0; j < height; ++j)
            {
                maze[i, j] = initial;
            }
        }

        DepthFirstBacktracker(maze, width, height);

        // create entrance and exit
        maze[0, 0] &= ~WallState.left;
        //maze[width - 1, height - 1] &= ~WallState.right;
        return maze;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    public Node[,] nodes { get; private set; }
    public Node start { get; private set; }
    public Node end { get; private set; }

    public AStar(int cols, int rows) {
        nodes = new Node[cols, rows];
    }
    private bool checkGrid()
    {
        if (end == null)
            return false;
        if (start == null)
            return false;
        return true;
    }

    public List<Vector2Int> getPath(Vector3Int[,] grid, Vector2Int start, Vector2Int end)
    {
        initNodes(grid, start, end);
        return calculatePath();
    }

    public void initNodes(Vector3Int[,] grid, Vector2Int start, Vector2Int end)
    {
        this.end = null;
        this.start = null;
        var columns = nodes.GetUpperBound(0) + 1;
        var rows = nodes.GetUpperBound(1) + 1;
        nodes = new Node[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                nodes[i, j] = new Node(grid[i, j].x, grid[i, j].y, grid[i,j].z);
            }
        }

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                nodes[i, j].addNeighboors(nodes, i, j);
                if (nodes[i, j].x == start.x && nodes[i, j].y == start.y)
                    this.start = nodes[i, j];
                else if (nodes[i, j].x == end.x && nodes[i, j].y == end.y)
                    this.end = nodes[i, j];
            }
        }
        
    }

    //https://fr.wikipedia.org/wiki/Algorithme_A*#Pseudocode
    List<Vector2Int> calculatePath()
    {
        if (!checkGrid())
            return null;

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        openList.Add(start);


        while(openList.Count > 0)
        {
            int bestIndex = 0;
            for (int i=0; i<openList.Count; i++)
            {
                if (openList[i].getRealCost() < openList[bestIndex].getRealCost())
                    bestIndex = i;
                else if ((openList[i].getRealCost() == openList[bestIndex].getRealCost()) && (openList[i].heuristic < openList[bestIndex].heuristic))
                        bestIndex = i;
            }

            Node currentBest = openList[bestIndex];

            if(end != null && currentBest == end)
            {
                return reconstructPath(currentBest);
            }

            openList.Remove(currentBest);
            closedList.Add(currentBest);

            for(int i=0; i < currentBest.neighboors.Count; i++)
            {
                Node currentNeighboor = currentBest.neighboors[i];
                if (!closedList.Contains(currentNeighboor) && currentNeighboor.z < 1)
                {
                    int cost = currentBest.cost + 1;
                    bool nPath = false;
                    if (openList.Contains(currentNeighboor) && cost < currentNeighboor.cost)
                        nPath = true;
                    else
                    {
                        currentNeighboor.cost = cost;
                        nPath = true;
                        openList.Add(currentNeighboor);
                    }

                    //Si npath on reprend le calcul à partir de ce noeud si, sinon on enchaine avec le voisin suivant.
                    if (nPath)
                    {
                        currentNeighboor.heuristic = calculateHeuristic(currentNeighboor, end);
                        currentNeighboor.prevNode = currentBest;
                    }
                }
            }
        }
        return null;
    }

    List<Vector2Int> reconstructPath(Node node)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        var current = node;
        while(current.prevNode != null)
        {
            path.Add(new Vector2Int(current.x, current.y));
            current = current.prevNode;
        }
        path.Reverse();
        return path;
    }

    //calcule et retourne une heusitique de base (la "distance")
    private int calculateHeuristic(Node a, Node b)
    {
        return 1 * (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
    }

}

public class Node
{
    public int x { get; private set; }
    public int y { get; private set; }

    public int z { get; private set; }
    public int cost { get; set; }
    public int heuristic { get; set; }
    public List<Node> neighboors { get; private set; }

    public Node prevNode = null;

    public Node(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        cost = 0;
        heuristic = 0;
        neighboors = new List<Node>();
    }

    public void addNeighboors(Node[,] grid, int x, int y)
    {
        if (x < grid.GetUpperBound(0))
            neighboors.Add(grid[x + 1, y]);
        if (x > 0)
            neighboors.Add(grid[x - 1, y]);
        if (y < grid.GetUpperBound(1))
            neighboors.Add(grid[x, y + 1]);
        if (y > 0)
            neighboors.Add(grid[x, y - 1]);
    }

    public int getRealCost()
    {
        return cost + heuristic;
    }

    
}

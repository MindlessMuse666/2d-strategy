using System.Collections.Generic;
using UnityEngine;

public static class GraphSearch
{
    public static Dictionary<Vector2Int, Vector2Int?> BreadthFirstSearchAlgorithm(
        MapGrid mapGraph, Vector2Int startPosition, int movePoints)
    {
        Dictionary<Vector2Int, Vector2Int?> visitedNodes = new();
        Dictionary<Vector2Int, int> costSoFar = new();
        Queue<Vector2Int> nodesToVisitQueue = new();

        nodesToVisitQueue.Enqueue(startPosition);
        costSoFar.Add(startPosition, 0);
        visitedNodes.Add(startPosition, null);

        while (nodesToVisitQueue.Count > 0)
        {
            Vector2Int currentNode = nodesToVisitQueue.Dequeue();

            foreach (Vector2Int neighbourPosition in mapGraph.GetNeighboursFor(currentNode))
            {
                if (!mapGraph.IsPositionValid(neighbourPosition))
                    continue;

                int nodeCost = mapGraph.GetMoveCost(neighbourPosition);    
                int currentCost = costSoFar[currentNode];
                int newCost = currentCost + nodeCost;

                if (newCost <= movePoints)
                {
                    if (!visitedNodes.ContainsKey(neighbourPosition))
                    {
                        visitedNodes[neighbourPosition] = currentNode;
                        costSoFar[neighbourPosition] = newCost;
                        nodesToVisitQueue.Enqueue(neighbourPosition);
                    }
                    else if (costSoFar[neighbourPosition] > newCost)
                    {
                        costSoFar[neighbourPosition] = newCost;
                        visitedNodes[neighbourPosition] = currentNode;
                    }
                }
            }
        }
        
        return visitedNodes;
    }
}
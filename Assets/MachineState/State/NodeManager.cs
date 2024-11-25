using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public List<Node> allNodes = new List<Node>();

    // Encuentra el nodo más cercano a una posición específica
    public Node GetClosestNode(Vector3 position)
    {
        Node closestNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (Node node in allNodes)
        {
            float distance = Vector3.Distance(position, node.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    // Devuelve un nodo aleatorio de la lista
    public Node GetRandomNode()
    {
        if (allNodes.Count == 0)
        {
            Debug.LogWarning("NodeManager: No hay nodos en la lista.");
            return null;
        }

        int randomIndex = Random.Range(0, allNodes.Count);
        return allNodes[randomIndex];
    }
}

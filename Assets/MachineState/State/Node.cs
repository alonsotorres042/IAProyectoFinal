using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Lista de nodos conectados
    public List<Node> connectedNodes = new List<Node>();

    // Devuelve un nodo aleatorio de los nodos conectados
    public Node GetRandomConnectedNode()
    {
        if (connectedNodes.Count == 0)
        {
            Debug.LogWarning("No hay nodos conectados a este nodo.");
            return null;
        }

        int randomIndex = Random.Range(0, connectedNodes.Count);
        return connectedNodes[randomIndex];
    }

    // Debug visual para mostrar las conexiones
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var connectedNode in connectedNodes)
        {
            if (connectedNode != null)
                Gizmos.DrawLine(transform.position, connectedNode.transform.position);
        }
    }
}

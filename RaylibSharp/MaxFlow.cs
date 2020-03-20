using System;
using System.Collections.Generic;

public class MaxFlow
{
    /* Returns true if there is a path
	from source 's' to sink 't' in residual
	graph. Also fills parent[] to store the
	path */

    private static bool bfs(int[,] rGraph, int s, int t, int[] parent, int V)
    {
        // Create a visited array and mark
        // all vertices as not visited
        bool[] visited = new bool[V];
        for (int i = 0; i < V; ++i)
            visited[i] = false;

        // Create a queue, enqueue source vertex and mark
        // source vertex as visited
        List<int> queue = new List<int>();
        queue.Add(s);
        visited[s] = true;
        parent[s] = -1;

        // Standard BFS Loop
        while (queue.Count != 0)
        {
            int u = queue[0];
            queue.RemoveAt(0);

            for (int v = 0; v < V; v++)
            {
                if (visited[v] == false && rGraph[u, v] > 0)
                {
                    queue.Add(v);
                    parent[v] = u;
                    visited[v] = true;
                }
            }
        }

        // If we reached sink in BFS
        // starting from source, then
        // return true, else false
        return (visited[t] == true);
    }

    // Returns tne maximum flow
    // from s to t in the given graph
    public static int fordFulkerson(int[,] graph, int source, int target, int count)
    {
        int u, v;
        if (source == target) return 0;
        // Create a residual graph and fill
        // the residual graph with given
        // capacities in the original graph as
        // residual capacities in residual graph

        // Residual graph where rGraph[i,j]
        // indicates residual capacity of
        // edge from i to j (if there is an
        // edge. If rGraph[i,j] is 0, then
        // there is not)
        int[,] rGraph = new int[count, count];

        for (u = 0; u < count; u++)
            for (v = 0; v < count; v++)
                rGraph[u, v] = graph[u, v];

        // This array is filled by BFS and to store path
        int[] parent = new int[count];

        int max_flow = 0; // There is no flow initially

        // Augment the flow while tere is path from source
        // to sink
        while (bfs(rGraph, source, target, parent, count))
        {
            // Find minimum residual capacity of the edhes
            // along the path filled by BFS. Or we can say
            // find the maximum flow through the path found.
            int path_flow = int.MaxValue;
            for (v = target; v != source; v = parent[v])
            {
                u = parent[v];
                path_flow = Math.Min(path_flow, rGraph[u, v]);
            }

            // update residual capacities of the edges and
            // reverse edges along the path
            for (v = target; v != source; v = parent[v])
            {
                u = parent[v];
                rGraph[u, v] -= path_flow;
                rGraph[v, u] += path_flow;
            }

            // Add path flow to overall flow
            max_flow += path_flow;
        }

        // Return the overall flow
        return max_flow;
    }
}
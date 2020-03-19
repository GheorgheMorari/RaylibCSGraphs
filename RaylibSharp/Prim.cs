using RaylibSharp.Raylib.Types;
using System.Collections.Generic;

namespace RaylibSharp
{
    internal static class Prim
    {
        private static bool Contains(bool[] array, bool node) //check if it contains true or false
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i] == node) return true;
            }
            return false;
        }

        private static int ArrMinIndex(int[] array, bool[] queue) //returns the index of the minimal value
        {
            int temp = int.MaxValue;
            int index = -1;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i] < temp && queue[i] == true)
                {
                    temp = array[i];
                    index = i;
                }
            }
            return index;
        }

        private static int[] CheckAdjacent(int[,] matrix, int thisVert) //returns an array of adjacent vertices
        {
            int[] adj = new int[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[thisVert, i] > 0)
                    adj[i] = i;
                else
                    adj[i] = -1;
            }
            return adj;
        }

        private static int[,] GetMatrix(int[,] matrix, List<Vector2> path) //Transform list of vectors into matrix
        {
            int[,] tempMatrix = new int[matrix.GetLength(0), matrix.GetLength(0)];
            foreach (Vector2 tempPath in path)
            {
                tempMatrix[(int)tempPath.x, (int)tempPath.y] = matrix[(int)tempPath.x, (int)tempPath.y];
                tempMatrix[(int)tempPath.y, (int)tempPath.x] = matrix[(int)tempPath.y, (int)tempPath.x];
            }
            return tempMatrix;
        }

        public static int[,] PrimAlgo(int[,] matrix, int rootNode) //the Prim's Algorithm
        {
            int[] key = new int[matrix.GetLength(0)]; //the array of distances
            int[] parent = new int[matrix.GetLength(0)]; //the array of parents
            bool[] queue = new bool[matrix.GetLength(0)]; //the queue, if it is true then it has not been searched
            List<Vector2> path = new List<Vector2>();

            for (int i = 0; i < key.GetLength(0); i++) //Initialise arrays
            {
                key[i] = int.MaxValue; //use int.MaxValue as infinity because no numbers can bigger than it
                parent[i] = -1;
                queue[i] = true;
            }
            key[rootNode] = 0;

            while (Contains(queue, true)) //while a vertex has not been searched
            {
                int thisVert = ArrMinIndex(key, queue); //the index of the minimum distance

                if (thisVert < 0) break; //check if not broken

                queue[thisVert] = false; //set this vertex as searched

                if (parent[thisVert] >= 0) //if it has parents, then make a path
                {
                    Vector2 tempVec = new Vector2(); //use vectors as edges, where x is node1, and y is node2
                    tempVec.x = parent[thisVert];
                    tempVec.y = thisVert;
                    path.Add(tempVec); //add to the list
                }
                int[] adj = CheckAdjacent(matrix, thisVert); //Array of adjacent vertices
                foreach (int vert in adj)
                {   //check if the vertex has adjacent vertices, and if those vertices haven't been searched yet
                    if (vert >= 0 && matrix[thisVert, vert] < key[vert] && queue[vert])
                    {
                        key[vert] = matrix[thisVert, vert];
                        parent[vert] = thisVert;
                    }
                }
            }
            return GetMatrix(matrix, path); //return treeMatrix
        }
    }
}
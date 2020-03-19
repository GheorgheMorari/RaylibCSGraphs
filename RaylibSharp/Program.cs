using RaylibSharp.Raylib.Types;
using System;
using System.Collections.Generic;
using static RaylibSharp.Raylib.Raylib;
using System.IO;

namespace RaylibSharp
{
    internal class Program
    {
        public static int[,] ResizeArray(int[,] original, int size)
        {
            var newArray = new int[size, size];
            int oSize = Math.Min(original.GetLength(0), size);
            for (int i = 0; i < oSize; i++)
                for (int j = 0; j < oSize; j++)
                    newArray[i, j] = original[i, j];
            return newArray;
        }

        public static void ShowConnections(int[,] matrix, List<Nodes> node, Color connectionColor, float thickness) //show all connections
        {
            int len = matrix.GetLength(0);
            for (int i = 0; i < len; i++)
                for (int j = i; j < len; j++)
                    if (matrix[i, j] > 0)
                    {
                        DrawLineEx(node[i].pos, node[j].pos, thickness, connectionColor);
                        DrawText(matrix[i, j].ToString(), (int)(node[i].pos.x + node[j].pos.x) / 2 - 10, (int)(node[i].pos.y + node[j].pos.y) / 2 - 10, 20, Color.BLACK);
                    }
        }

        public static void PrintMatrix(int[,] matrix)
        {
            Console.Write('\n');
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                    Console.Write(matrix[i, j]);
                Console.Write('\n');
            }
        }

        public static void DeleteConnections(int[,] matrix, int index)
        {
            int limit = matrix.GetLength(0);
            for (int i = index; i < limit - 1; i++) //shift the colls left
                for (int j = 0; j < limit; j++)
                    matrix[i, j] = matrix[i + 1, j];

            for (int j = 0; j < limit; j++) //delete the remaining weights after shifting
                matrix[limit - 1, j] = 0;

            for (int i = index; i < limit - 1; i++) //shift the rows up
                for (int j = 0; j < limit; j++)
                    matrix[j, i] = matrix[j, i + 1];

            for (int j = 0; j < limit; j++)//delete the remaining weights after shifting
                matrix[j, limit - 1] = 0;
        }

        public static void AddConnection(int[,] matrix, Nodes node1, Nodes node2, int weight) //add connection to the matrix
        {
            if (node1 == node2) return;
            matrix[node1.index, node2.index] = weight;
            matrix[node2.index, node1.index] = weight;
        }

        public static void GetSolution(List<Nodes> node, int[,] matrix, int rootNode) //get the distance from the rootNode
        {
            int[] dist = GFG.dijkstra(matrix, rootNode, matrix.GetLength(0));
            int V = dist.Length;

            for (int i = 0; i < V; i++)
                node[i].dist = dist[i];
        }

        public static void GetMST(int[,] matrix, int rootNode, out int[,] treeMatrix)
        {
            treeMatrix = Prim.PrimAlgo(matrix, rootNode);
        }

        public static void Main()
        {
            InitWindow(1800, 980, "Dijkstra algorithm");
            SetTargetFPS(24);

            //The main list of nodes
            List<Nodes> node = new List<Nodes>();
            int rootNode = 0;

            //temporary nodes
            Nodes tempNode0 = new Nodes();
            Nodes tempNode1 = new Nodes();
            Nodes tempNode2 = new Nodes();

            //Boolean flags
            bool colision = false;
            bool firstChoosen = false;
            bool inputDistance = false;
            bool showDistance = false;
            bool showMST = false; //minimal spanning tree
            bool showConnections = true;

            //Distance input stuff
            int letterCount = 0;
            char[] val = new char[9];
            val[0] = '0';

            //Adjacency matrix
            int[,] matrix = new int[2, 2];
            int[,] treeMatrix = new int[2, 2];

            //Colors and thickness
            Color regularConnection = new Color(200, 200, 200);
            float regularThickness = 3F;
            Color MSTConnection = new Color(255, 10, 10);
            float MSTThickness = 2F;

            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.WHITE);

                if (IsMouseButtonPressed(0) && !inputDistance) //Click to add new node
                {
                    foreach (Nodes thisNode in node)
                        if (CheckCollisionPointCircle(GetMousePosition(), thisNode.pos, thisNode.radius))
                        { //check if mouse is over all points, if yes then colision is set to true
                            colision = true;
                            tempNode0 = thisNode;
                            break;
                        }

                    if (colision) // if there is a colision
                    {
                        if (!firstChoosen) //choose the first node
                        {
                            firstChoosen = true;
                            tempNode1 = tempNode0; // set the tempNode1 to be the node that was collided
                        }
                        else //choose the second node
                        {
                            firstChoosen = false;
                            tempNode2 = tempNode0; //set the second node
                            if (tempNode2 != tempNode1) //No distance to itself
                            {
                                inputDistance = true; //initiate the inputting of distance
                            }
                            else //Choose the new root node
                            {
                                rootNode = tempNode0.index;
                                tempNode2 = null;
                                tempNode0 = null;
                                GetSolution(node, matrix, rootNode);
                                GetMST(matrix, rootNode, out treeMatrix);
                            }
                        }
                        colision = false;
                    }
                    else
                    {
                        node.Add(new Nodes(GetMousePosition(), node.Count)); //if there is no colision add new node to the list
                        matrix = ResizeArray(matrix, node.Count);
                    }
                }

                if (inputDistance) // if two nodes are selected, then input the weight
                {
                    int key = GetKeyPressed();

                    while (key > 0)
                    {
                        // NOTE: Only allow keys in range [48..57]
                        if ((key >= 48) && (key <= 57) && (letterCount < 9))
                        {
                            val[letterCount] = (char)key;
                            letterCount++;
                        }

                        key = 0;  // Check next character in the queue
                    }

                    if (IsKeyPressed(Raylib.KeyboardKey.KeyBackspace)) //delete characters
                    {
                        letterCount--;
                        if (letterCount < 0)
                        {
                            letterCount = 1;
                            val[0] = '0';
                        }
                        else
                            val[letterCount] = '\0';
                    }

                    if (IsKeyPressed(Raylib.KeyboardKey.KeyEnter)) //accept distance
                    {
                        AddConnection(matrix, tempNode1, tempNode2, Int32.Parse(String.Concat(val)));
                        inputDistance = false;

                        if (showDistance)
                            GetSolution(node, matrix, rootNode);

                        if (showMST)
                            GetMST(matrix, rootNode, out treeMatrix);
                    }
                    int x = (int)(tempNode1.pos.x + tempNode2.pos.x) / 2;
                    int y = (int)(tempNode1.pos.y + tempNode2.pos.y) / 2;

                    DrawText(String.Concat(val), x - 10, y - 10, 20, new Color(128, 0, 0));
                    DrawText("Type the weight, and then ENTER-key", 10, 10, 20, Color.BLACK);
                }
                else
                {
                    DrawText("F - Dijkstra Distance, C - Connections, R - Random, V - Prim's Minimal Spanning Tree", 10, 10, 20, Color.BLACK);
                    DrawText("Press W to export graph via text file", 10, 690, 20, Color.BLACK);
                    DrawText("Press I to import graph via text file (1-line number of vertices,2-nd line starts matrix represenation)", 10, 670, 20, Color.BLACK);

                    if (!firstChoosen)
                        DrawText("Click on a node to select it", 10, 30, 20, Color.BLACK);
                    else
                    {
                        DrawText("Press Delete to Delete this Node", 10, 30, 20, Color.BLACK);
                        DrawText("Or click on another Node to Make a connection", 10, 50, 20, Color.BLACK);
                        DrawText("Click again on it to make it the ROOT", 10, 70, 20, Color.BLACK);
                    }
                }

                //Show all the connections
                if (showConnections)
                    ShowConnections(matrix, node, regularConnection, regularThickness);

                if (showMST)
                    ShowConnections(treeMatrix, node, MSTConnection, MSTThickness);

                //Show all the nodes
                foreach (Nodes thisNode in node)
                {
                    thisNode.Show(showDistance, rootNode);
                }
                //Highlight selected node
                if (tempNode0 == tempNode1)
                {
                    tempNode0.Highlight();
                }

                //Highlight both nodes when inputting distance
                if (inputDistance)
                {
                    tempNode1.Highlight();
                    tempNode2.Highlight();
                }

                //Delete node
                if (IsKeyPressed(Raylib.KeyboardKey.KeyDelete) && tempNode0 == tempNode1)
                {
                    int removeIndex = tempNode0.index;

                    if (removeIndex == rootNode) //change root if root is deleted
                        rootNode = 0;

                    node.RemoveAt(removeIndex); //remove from list

                    firstChoosen = false; //deselect node

                    DeleteConnections(matrix, removeIndex);//remove connections from main array
                    DeleteConnections(treeMatrix, removeIndex);//remove connections from the MST

                    matrix = ResizeArray(matrix, node.Count);//resize array

                    for (int i = 0; i < node.Count; i++) //redo all indexes
                        node[i].index = i;

                    if (rootNode >= node.Count) //fix out of bounds error
                        rootNode--;

                    if (rootNode < 0) rootNode = 0; //fix deletion of all nodes

                    if (node.Count > 1) //calculate distance if there are more than 2 nodes
                    {
                        GetSolution(node, matrix, rootNode);
                        GetMST(matrix, rootNode, out treeMatrix);
                    }
                    if (showMST)
                    { //rebuild the MST
                        GetMST(matrix, rootNode, out treeMatrix);
                        GetSolution(node, treeMatrix, rootNode);
                    }
                    tempNode0 = null;
                }

                //Show distance from node 0
                if (IsKeyPressed(Raylib.KeyboardKey.KeyF) && node.Count > 0)
                {
                    showDistance = !showDistance;
                    if (!showMST)
                        GetSolution(node, matrix, rootNode);
                    else
                        GetSolution(node, treeMatrix, rootNode);
                }

                if (IsKeyPressed(Raylib.KeyboardKey.KeyV) && node.Count > 0)//view MST
                {
                    showMST = !showMST;
                    GetMST(matrix, rootNode, out treeMatrix);
                    GetSolution(node, treeMatrix, rootNode);
                }

                if (IsKeyPressed(Raylib.KeyboardKey.KeyC) && node.Count > 0)//toggle connections
                {
                    showConnections = !showConnections;
                    if (!showConnections)
                        GetSolution(node, treeMatrix, rootNode);
                    else
                        GetSolution(node, matrix, rootNode);
                }
                //Import the matrix by pressing I
                if (IsKeyPressed(Raylib.KeyboardKey.KeyI))
                {
                    matrix = ReadingTxt.ReadingWriting.ImportMatrix(node);
                }
                //Export the matrix by pressing W
                if (IsKeyPressed(Raylib.KeyboardKey.KeyW))
                {
                    //Export matrix
                    ReadingTxt.ReadingWriting.ExportMatrix(matrix);
                }

                if (IsKeyPressed(Raylib.KeyboardKey.KeyR)) //randomise connections
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                        for (int j = 0; j < matrix.GetLength(0); j++)
                        {
                            int rnd = GetRandomValue(-(node.Count * 10), 100);
                            if (rnd < 0) rnd = 0;
                            matrix[i, j] = rnd;
                            matrix[j, i] = rnd;
                        }
                    GetSolution(node, matrix, rootNode);
                    GetMST(matrix, rootNode, out treeMatrix);
                }

                EndDrawing();
            }

            CloseWindow();
        }
    }
}
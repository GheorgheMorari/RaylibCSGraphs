using RaylibSharp.Raylib.Types;
using System;
using System.Collections.Generic;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public class Program
    {
        //The main list of nodes
        static public List<Nodes> node = new List<Nodes>();

        //Temporary nodes
        static public Nodes tempNode0 = new Nodes();

        static public Nodes tempNode1 = new Nodes();
        static public Nodes tempNode2;
        static public Nodes centerNode = new Nodes(new Vector2(0, 0), 0);

        //Colors and thickness
        static public Color regularConnection = new Color(200, 200, 200);

        static public float regularThickness = 3F;

        //Boolean flags

        public static bool colision = false;
        public static bool firstChoosen = false;
        public static bool showConnections = true;
        public static bool showText = true;
        public static bool showCircles = true;
        public static bool edit = true;

        //Adjacency matrix
        public static int[,] matrix = new int[2, 2];

        //Tranformations
        public static float[,] TMatrix = new float[2, 2] { { 1, 0 }, { 0, 1 } };

        public static float angle = 0;

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
                        DrawLineEx(node[i].modpos, node[j].modpos, thickness, connectionColor);
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

        public static void Main()
        {
            InitWindow(1800, 980, "Dijkstra algorithm");
            SetTargetFPS(24);
            centerNode = new Nodes(new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2), 0);
            centerNode = Nodes.makeCenter(centerNode);
            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.WHITE);

                if (IsMouseButtonPressed(0)) //Click to add new node
                {
                    foreach (Nodes thisNode in node)
                        if (CheckCollisionPointCircle(GetMousePosition(), thisNode.modpos, thisNode.radius))
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
                                AddConnection(matrix, tempNode1, tempNode2, 1);
                            }
                            else //Choose the new root node
                            {
                                centerNode = Nodes.makeCenter(tempNode1);
                                foreach (Nodes thisNode in node)
                                {
                                    if (thisNode != centerNode)
                                    {
                                        thisNode.setCenter(centerNode);
                                        thisNode.transform(TMatrix, centerNode, angle);
                                    }
                                }
                                tempNode2 = null;
                                tempNode0 = null;
                            }
                        }
                        colision = false;
                    }
                    else
                    {
                        node.Add(new Nodes(GetMousePosition(), node.Count, centerNode)); //if there is no colision add new node to the list
                        matrix = ResizeArray(matrix, node.Count);
                    }
                }

                if (showText)
                {
                    string buf = "";
                    DrawText("Press I to import graph from file or press buttons 1 to 7", 10, GetScreenHeight() - 30, 20, Color.BLACK);
                    DrawText("Press W to export graph", 10, GetScreenHeight() - 50, 20, Color.BLACK);
                    DrawText("Toggle: C-Connections, V-Nodes, G-text, T-resetMatrix, F-resetAngle", 10, 10, 20, Color.BLACK);
                    buf = (1 - TMatrix[0, 0] + (float)Math.Cos(angle)).ToString();
                    DrawText(buf, 30, 30, 20, Color.BLACK);
                    buf = (TMatrix[0, 1] - (float)Math.Sin(angle)).ToString();
                    DrawText(buf, 170, 30, 20, Color.BLACK);
                    buf = (TMatrix[1, 0] + (float)Math.Sin(angle)).ToString();
                    DrawText(buf, 30, 60, 20, Color.BLACK);
                    buf = (1 - TMatrix[1, 1] + (float)Math.Cos(angle)).ToString();
                    DrawText(buf, 170, 60, 20, Color.BLACK);
                    buf = (angle / (float)3.14 * 180).ToString();
                    DrawText("Angle in Degrees:" + buf, 30, 90, 20, Color.BLACK);
                }

                //Show all the connections
                if (showConnections)
                    ShowConnections(matrix, node, regularConnection, regularThickness);

                //Show all the nodes
                if (showCircles)
                    foreach (Nodes thisNode in node)
                    {
                        thisNode.Show();
                    }
                //Highlight selected node
                if (tempNode0 == tempNode1)
                {
                    tempNode0.Highlight();
                }

                //Delete node
                if (IsKeyPressed(Raylib.KeyboardKey.KeyDelete) && tempNode0 == tempNode1)
                {
                    int removeIndex = tempNode0.index;

                    node.RemoveAt(removeIndex); //remove from list

                    firstChoosen = false; //deselect node

                    DeleteConnections(matrix, removeIndex);//remove connections from main array

                    matrix = ResizeArray(matrix, node.Count);//resize array

                    for (int i = 0; i < node.Count; i++) //redo all indexes
                        node[i].index = i;

                    tempNode0 = null;
                }

                keyboardInteraction.keyboardInteractions();
                EndDrawing();
            }

            CloseWindow();
        }
    }
}
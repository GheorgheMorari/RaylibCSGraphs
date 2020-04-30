using RaylibSharp.Raylib.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public static class Program
    {
        public static List<NodeClass> NodeList = new List<NodeClass>();

        //Temporary nodes
        public static NodeClass NodeHitByMouse = new NodeClass();

        public static NodeClass NodeSelected = new NodeClass();
        public static NodeClass NodeToConnect;

        public static NodeClass CenterNode;

        public static Color ConnectionColor = new Color(200, 200, 200);
        public const float ConnectionThickness = 3F;

        private static bool Colision = false;
        private static bool IsSelected = false;

        public static bool showConnections = true;
        public static bool ShowText = true;
        public static bool ShowNodes = true;
        public static bool edit = true;

        //Adjacency AdjacencyMatrix
        public static int[,] AdjacencyMatrix = new int[2, 2];

        //Tranformations
        public static float[,] TransformationMatrix = new float[2, 2] { { 1, 0 }, { 0, 1 } };

        public static float TransformationAngle = 0;

        public static int[,] ResizeArray(int[,] Array, int NewSize)
        {
            var newArray = new int[NewSize, NewSize];
            int oSize = Math.Min(Array.GetLength(0), NewSize);
            for (int i = 0; i < oSize; i++)
                for (int j = 0; j < oSize; j++)
                    newArray[i, j] = Array[i, j];
            return newArray;
        }

        public static void DrawConnections(int[,] AdjacencyMatrix, List<NodeClass> NodeList, Color ConnectionColor, float thickness)
        {
            int len = AdjacencyMatrix.GetLength(0);
            for (int i = 0; i < len; i++)
                for (int j = i; j < len; j++)
                    if (AdjacencyMatrix[i, j] > 0)
                    {
                        DrawLineEx(NodeList[i].TransformedPos, NodeList[j].TransformedPos, thickness, ConnectionColor);
                    }
        }

        public static void DeleteConnections(int[,] AdjacencyMatrix, int index)
        {
            int limit = AdjacencyMatrix.GetLength(0);
            for (int i = index; i < limit - 1; i++) //shift the colls left
                for (int j = 0; j < limit; j++)
                    AdjacencyMatrix[i, j] = AdjacencyMatrix[i + 1, j];

            for (int j = 0; j < limit; j++) //delete the remaining weights after shifting
                AdjacencyMatrix[limit - 1, j] = 0;

            for (int i = index; i < limit - 1; i++) //shift the rows up
                for (int j = 0; j < limit; j++)
                    AdjacencyMatrix[j, i] = AdjacencyMatrix[j, i + 1];

            for (int j = 0; j < limit; j++)//delete the remaining weights after shifting
                AdjacencyMatrix[j, limit - 1] = 0;
        }

        public static void Main()
        {
            const int TargetFPS = 60;
            InitWindow(1800, 980, "PBL Matrix Transformations");
            SetTargetFPS(TargetFPS);

            CenterNode = new NodeClass(new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2), 0);
            CenterNode = NodeClass.MakeCenter(CenterNode);

            Stopwatch stopwatch = new Stopwatch();
            while (!WindowShouldClose())
            {
                stopwatch.Start();
                BeginDrawing();
                ClearBackground(Color.WHITE);
                if (IsMouseButtonPressed(0)) //Click to add new node
                {
                    Vector2 MousePos = GetMousePosition();
                    foreach (NodeClass ThisNode in NodeList)
                        if (ThisNode.CheckIfHitBy(MousePos))
                        {
                            Colision = true;
                            NodeHitByMouse = ThisNode;
                            break;
                        }

                    if (Colision)
                    {
                        if (!IsSelected) //choose the first node
                        {
                            IsSelected = true;
                            NodeSelected = NodeHitByMouse;
                        }
                        else //choose the second node
                        {
                            IsSelected = false;
                            NodeToConnect = NodeHitByMouse;
                            if (NodeToConnect != NodeSelected) //No distance to itself
                            {
                                AdjacencyMatrix[NodeToConnect.NodeIndex, NodeSelected.NodeIndex] = 1;
                                AdjacencyMatrix[NodeSelected.NodeIndex, NodeToConnect.NodeIndex] = 1;
                            }
                            else //If NodeSelected is the NodeHitByMouse then change CenterNode
                            {
                                CenterNode = NodeClass.MakeCenter(NodeSelected);
                                foreach (NodeClass ThisNode in NodeList)
                                {
                                    if (ThisNode != CenterNode)
                                    {
                                        ThisNode.OffsetNodeToCenter(CenterNode);
                                        ThisNode.Transform(TransformationMatrix, CenterNode, TransformationAngle);
                                    }
                                }
                                NodeToConnect = null;
                                NodeHitByMouse = null;
                            }
                        }
                        Colision = false;
                    }
                    else //Add new node
                    {
                        var NewNode = new NodeClass(GetMousePosition(), NodeList.Count, CenterNode);
                        NodeList.Add(NewNode); //if there is no Colision add new node to the list
                        AdjacencyMatrix = ResizeArray(AdjacencyMatrix, NodeList.Count);
                        NewNode.Transform(TransformationMatrix, CenterNode, TransformationAngle);
                    }
                }

                if (ShowText)
                {
                    DrawText("Press I to import graph from file or press buttons 1 to 7", 10, GetScreenHeight() - 30, 20, Color.BLACK);
                    DrawText("Press O to export graph, Insert - Reset", 10, GetScreenHeight() - 50, 20, Color.BLACK);
                    DrawText("Toggle: C-Connections, V-Nodes, G-text, R-resetMatrix, F-resetAngle", 10, 10, 20, Color.BLACK);

                    string buf = (1 - TransformationMatrix[0, 0] + (float)Math.Cos(TransformationAngle)).ToString();
                    DrawText(buf, 30, 30, 20, Color.BLACK);
                    buf = (TransformationMatrix[0, 1] - (float)Math.Sin(TransformationAngle)).ToString();
                    DrawText(buf, 170, 30, 20, Color.BLACK);
                    buf = (TransformationMatrix[1, 0] + (float)Math.Sin(TransformationAngle)).ToString();
                    DrawText(buf, 30, 60, 20, Color.BLACK);
                    buf = (1 - TransformationMatrix[1, 1] + (float)Math.Cos(TransformationAngle)).ToString();
                    DrawText(buf, 170, 60, 20, Color.BLACK);

                    buf = (TransformationAngle / (float)3.14 * 180).ToString();
                    DrawText("Angle in Degrees:" + buf, 30, 90, 20, Color.BLACK);
                }

                //Show all the connections
                if (showConnections)
                    DrawConnections(AdjacencyMatrix, NodeList, ConnectionColor, ConnectionThickness);

                //Show all the nodes
                if (ShowNodes)
                    foreach (NodeClass ThisNode in NodeList)
                    {
                        ThisNode.DisplayNode();
                    }

                //Highlight selected node
                if (NodeHitByMouse == NodeSelected)
                {
                    NodeHitByMouse.Highlight();
                }

                //Delete node
                if (IsKeyPressed(Raylib.KeyboardKey.KeyDelete) && NodeHitByMouse == NodeSelected)
                {
                    int removeIndex = NodeHitByMouse.NodeIndex;
                    NodeList.RemoveAt(removeIndex); //remove from list

                    IsSelected = false; //deselect node
                    DeleteConnections(AdjacencyMatrix, removeIndex);//remove connections from main array
                    AdjacencyMatrix = ResizeArray(AdjacencyMatrix, NodeList.Count);

                    for (int i = 0; i < NodeList.Count; i++) //redo all indexes
                        NodeList[i].NodeIndex = i;
                }

                KeyboardInteraction.KeyboardInteractions();
                stopwatch.Stop();
                DrawText("FPS " + (int)(1000 / (stopwatch.Elapsed.TotalMilliseconds + 0.000001)), 10, GetScreenHeight() - 70, 20, Color.BLACK);
                EndDrawing();
                int kek = (1000 / TargetFPS - stopwatch.ElapsedMilliseconds < 0) ? 0 : 1000 / (int)(TargetFPS - stopwatch.ElapsedMilliseconds);
                stopwatch.Reset();
                Thread.Sleep(kek);
            }

            CloseWindow();
        }
    }
}
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

        public static Color ConnectionColor = new Color(100, 100, 100);
        public static Color ConnectionColorWithNodes = new Color(200, 200, 200);
        public const float ConnectionThickness = 3F;

        private static bool Colision = false;
        public static bool IsSelected = false;

        public static bool showConnections = true;
        public static bool ShowText = true;
        public static bool ShowNodes = true;
        public static bool edit = true;

        public static int[,] AdjacencyMatrix = new int[2, 2];

        //Tranformations
        public static float[,] TransformationMatrix = new float[2, 2] { { 1, 0 }, { 0, 1 } };

        public static float TransformationAngle = 0;
        public static bool ScaleThenRotate = true;

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
                        if (ShowNodes)
                            DrawLineEx(NodeList[i].TransformedPos, NodeList[j].TransformedPos, thickness, ConnectionColorWithNodes);
                        else
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
            CenterNode.TransformedPos.x = CenterNode.OriginalPos.x - CenterNode.TemporaryPos.x;
            CenterNode.TransformedPos.y = CenterNode.OriginalPos.y - CenterNode.TemporaryPos.y;

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
                                //value is the opposite of the current connection
                                //when there is a connection, that connection is removed
                                int value = (AdjacencyMatrix[NodeToConnect.NodeIndex, NodeSelected.NodeIndex] != 0) ? (0) : 1;
                                AdjacencyMatrix[NodeToConnect.NodeIndex, NodeSelected.NodeIndex] = value;
                                AdjacencyMatrix[NodeSelected.NodeIndex, NodeToConnect.NodeIndex] = value;
                            }
                            else //If NodeSelected is the NodeHitByMouse then change CenterNode
                            {
                                CenterNode.TemporaryPos = NodeHitByMouse.TransformedPos;
                                CenterNode.TransformedPos.x = CenterNode.OriginalPos.x - CenterNode.TemporaryPos.x;
                                CenterNode.TransformedPos.y = CenterNode.OriginalPos.y - CenterNode.TemporaryPos.y;
                                KeyboardInteraction.Change();
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
                        KeyboardInteraction.Change();
                    }
                }

                if (ShowText)
                {
                    DrawText("Press I to import graph from file or press buttons 1 to 7", 10, GetScreenHeight() - 30, 20, Color.BLACK);
                    DrawText("Press O to export graph, Insert - Reset", 10, GetScreenHeight() - 50, 20, Color.BLACK);
                    DrawText("Toggle: C-Connections, V-Nodes, G-text, R-resetMatrix, F-resetAngle", 10, 10, 20, Color.BLACK);

                    string buf = (TransformationMatrix[0, 0]).ToString();
                    DrawText("ScaleX= " + buf, 10, 35, 20, Color.BLACK);
                    buf = (TransformationMatrix[1, 1]).ToString();
                    DrawText("ScaleY= " + buf, 10, 60, 20, Color.BLACK);

                    buf = (TransformationMatrix[0, 1]).ToString();
                    DrawText("SkewX= " + buf, 10, 85, 20, Color.BLACK);
                    buf = (TransformationMatrix[1, 0]).ToString();
                    DrawText("SkewY= " + buf, 10, 110, 20, Color.BLACK);

                    buf = (CenterNode.OriginalPos.x - CenterNode.TransformedPos.x).ToString();
                    DrawText("OffsetX= " + buf, 10, 135, 20, Color.BLACK);
                    buf = (CenterNode.OriginalPos.x - CenterNode.TransformedPos.y).ToString();
                    DrawText("OffsetY= " + buf, 10, 160, 20, Color.BLACK);

                    buf = (TransformationAngle / (float)3.14 * 180).ToString();
                    DrawText("Angle in Degrees:" + buf, 10, 185, 20, Color.BLACK);
                }

                //Show all the connections
                if (showConnections)
                    DrawConnections(AdjacencyMatrix, NodeList, ConnectionColor,
                        ConnectionThickness * Math.Max((float)Math.Sqrt(TransformationMatrix[1, 1] *
                        TransformationMatrix[1, 1] + TransformationMatrix[0, 0] * TransformationMatrix[0, 0]), 0.4f));

                //Show all the nodes
                if (ShowNodes)
                {
                    foreach (NodeClass ThisNode in NodeList)
                    {
                        ThisNode.DisplayNode();
                    }
                    CenterNode.DisplayNode();
                }
                //Highlight selected node
                if (IsSelected)
                {
                    NodeSelected.Highlight();
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
                EndDrawing();
                int kek = (1000 / TargetFPS - stopwatch.Elapsed.TotalMilliseconds < 0) ? 0 :
                    (int)(1000 / TargetFPS - stopwatch.Elapsed.TotalMilliseconds);
                if (ShowText)
                    DrawText("FPS " + GetFPS(), 10, GetScreenHeight() - 70, 20, Color.BLACK);
                stopwatch.Reset();
                Thread.Sleep(kek);
            }

            CloseWindow();
        }
    }
}
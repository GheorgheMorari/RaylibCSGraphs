
using RaylibSharp.Raylib.Types;
using System;
using System.Collections.Generic;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    internal class Program
    {
        public static void ShowConnections(int[,] matrix, List<Nodes> node)
        {
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
                for (int j = i; j < Math.Sqrt(matrix.Length); j++)
                    if (matrix[i, j] > 0)
                        Connections.ShowConnection(node[i], node[j], matrix[i, j]);
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
            PrintMatrix(matrix);
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
                if (matrix[index, i] > 0)
                {
                    matrix[index, i] = 0;
                    matrix[i, index] = 0;
                }

            PrintMatrix(matrix);

        }
        public static void AddConnection(int[,] matrix, Nodes node1, Nodes node2, int weight)
        {
            if (node1 == node2) return;
            matrix[node1.index, node2.index] = weight;
            matrix[node2.index, node1.index] = weight;
        }

        public static void GetSolution(List<Nodes> node, int[] dist)
        {
            int V = dist.Length;

            for (int i = 0; i < V; i++)
                node[i].dist = dist[i];
        }

        public static void Main()
        {
            InitWindow(1280, 720, "Dijkstra algorithm");
            SetTargetFPS(24);
            int ballNum = 0;
            List<Nodes> node = new List<Nodes>();

            Nodes tempNode0 = new Nodes(); //temporary nodes to save what node is selected
            Nodes tempNode1 = new Nodes();
            Nodes tempNode2 = new Nodes();
            bool colision = false;
            bool firstChoosen = false;
            bool inputDistance = false;
            bool showDistance = false;
            int letterCount = 0;
            char[] val = new char[9];
            val[0] = '0';
            int[,] matrix = new int[40, 40];
            for (int i = 0; i < 40; i++)
                for (int j = 0; j < 40; j++)
                    matrix[i, j] = 0;

            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.WHITE);

                if (IsMouseButtonPressed(0) && !inputDistance) //Click to add new node
                {
                    if (ballNum > 1) //check if there are less than 2 balls so no connections can be made
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
                                    AddConnection(matrix, tempNode1, tempNode2, 0); // make a connection
                                    inputDistance = true; //initiate the inputting of distance
                                }
                            }
                            colision = false;
                        }
                        else
                        {
                            node.Add(new Nodes(GetMousePosition(), node.Count)); //if there is no colission add new node to the list
                        }
                    }
                    else
                    {
                        ballNum++;
                        node.Add(new Nodes(GetMousePosition(), node.Count)); //if there are fewer than 2 nodes, then add new nodes
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
                            GetSolution(node, GFG.dijkstra(matrix, 0, node.Count));
                    }
                    int x = (int)(tempNode1.pos.x + tempNode2.pos.x) / 2;
                    int y = (int)(tempNode1.pos.y + tempNode2.pos.y) / 2;

                    DrawText(String.Concat(val), x - 10, y - 10, 20, new Color(128, 0, 0));
                    DrawText("Type the weight, and then ENTER-key", 10, 10, 20, Color.BLACK);
                }
                else
                {
                    DrawText("Click to add node, Press F to toggle Distance", 10, 10, 20, Color.BLACK);
                    if (!firstChoosen)
                        DrawText("Click on a node to select it", 10, 30, 20, Color.BLACK);
                    else
                    {
                        DrawText("Press Delete to Delete this Node", 10, 30, 20, Color.BLACK);
                        DrawText("Or click on another Node to Make a connection", 10, 50, 20, Color.BLACK);

                    }

                }

                //Show all the connections
                ShowConnections(matrix, node);

                //Show all the nodes
                foreach (Nodes thisNode in node)
                {
                    thisNode.Show(showDistance);
                }
                //Hightlight selected node
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
                if (IsKeyPressed(Raylib.KeyboardKey.KeyDelete))
                {
                    int removeIndex = tempNode0.index;
                    node.RemoveAt(removeIndex);
                    firstChoosen = false;
                    for (int i = 0; i < node.Count; i++)
                        node[i].index = i;
                    DeleteConnections(matrix, removeIndex);
                    tempNode0 = null;
                }
                //Show distance from node 0
                if (IsKeyPressed(Raylib.KeyboardKey.KeyF))
                {
                    showDistance = !showDistance;
                    GetSolution(node, GFG.dijkstra(matrix, 0, node.Count));

                }

                EndDrawing();
            }

            CloseWindow();

        }

    }

}
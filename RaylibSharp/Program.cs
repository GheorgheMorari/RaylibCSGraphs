
using RaylibSharp.Raylib.Types;
using System;
using System.Collections.Generic;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    internal class Program
    {
        public static void Main()
        {
            InitWindow(1280, 720, "Dijkstra algorithm");
            SetTargetFPS(60);
            int ballNum = 0;
            List<Nodes> node = new List<Nodes>();

            Nodes tempNode0 = new Nodes(); //temporary nodes to save what node is selected
            Nodes tempNode1 = new Nodes();
            Nodes tempNode2 = new Nodes();
            bool firstChoosen = false;
            bool inputDistance = false;
            int letterCount = 0;
            char[] val = new char[9];
            Connections tempConnect;
            int index = 0;

            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.WHITE);



                if (IsMouseButtonPressed(0) && !inputDistance) //Click to add new node
                {
                    if (ballNum > 1) //check if there are less than 2 balls so no connections can be made
                    {
                        bool colision = false;
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
                                tempNode1.AddConnection(tempNode1, tempNode2, -1); // make a connection
                                inputDistance = true; //initiate the inputting of distance
                            }
                        }
                        else
                        {
                            node.Add(new Nodes(GetMousePosition(), index++)); //if there is no colission add new node to the list
                        }
                    }
                    else
                    {
                        ballNum++;
                        node.Add(new Nodes(GetMousePosition(), index++)); //if there are fewer than 2 nodes, then add new nodes
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
                        val[letterCount] = '\0';

                        if (letterCount < 0) letterCount = 0;
                    }

                    if (IsKeyPressed(Raylib.KeyboardKey.KeyEnter)) //accept distance
                    {
                        tempConnect = tempNode1.GetLastConnection();
                        tempConnect.SetWeight(Int32.Parse(String.Concat(val)));
                        inputDistance = false;
                    }
                    int x = (int)(tempNode1.pos.x + tempNode2.pos.x) / 2;
                    int y = (int)(tempNode1.pos.y + tempNode2.pos.y) / 2;

                    DrawText(String.Concat(val), x - 10, y - 10, 20, new Color(128, 0, 0));
                }

                foreach (Nodes thisNode in node)
                {
                    thisNode.Show();
                }

                EndDrawing();
            }

            CloseWindow();

        }

    }

}
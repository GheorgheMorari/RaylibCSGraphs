using RaylibSharp.Raylib.Types;
using System.Collections.Generic;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public class Nodes
    {
        public Vector2 pos;      //the vector of the positions use pos.x and pos.y
        public int index;        //the index of the node
        public float radius = 20;//the radius of the node
        public float border = 2; //border radius + radius

        Color baseColor;
        Color borderColor;

        public float dist = -1; //distance from the starting point

        List<Connections> connections = new List<Connections>(); //the list of connections of this node
        public Connections retur; //the connection that returns to the node with the least distance to the starting point
        public Nodes() // basic initialisation to exclude uninitialised objects used
        {
        }
        public Nodes(Vector2 pos_, int index_)
        {
            pos = pos_;
            baseColor = new Color(255, 0, 0);
            borderColor = new Color(130, 45, 45);
            index = index_;
        }

        public void Show()
        {
            foreach (Connections thisConnection in connections)
            {
                thisConnection.ShowConnection(); //draw all connections first
            }

            DrawCircleV(pos, radius + border, borderColor); //draw border
            DrawCircleV(pos, radius, baseColor); //draw circle


            DrawText(index.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
        }

        public void AddConnection(Nodes node1, Nodes node2, int weight)
        {
            connections.Add(new Connections(node1, node2, weight)); //add new connection to the list
        }

        public Connections GetLastConnection()
        {
            return connections[connections.Count - 1]; //return the last connection
        }

    }
    public class Connections
    {
        public bool traversed = false;
        public int weight = -1;
        public Nodes node1, node2;
        int tx, ty;
        Vector2 firstPos, secondPos;
        Color connectionColor = new Color(200, 200, 200);
        Color textColor = new Color(0, 0, 0);


        public Connections(Nodes node1_, Nodes node2_, int weight_)
        {
            weight = weight_;
            node1 = node1_;
            node2 = node2_;
            firstPos = node1_.pos;
            secondPos = node2_.pos;
            tx = (int)(node1.pos.x + node2.pos.x) / 2;
            ty = (int)(node1.pos.y + node2.pos.y) / 2;


        }

        public void ShowConnection()
        {
            DrawLineEx(firstPos, secondPos, 3, connectionColor);

            if (weight != -1)
                DrawText(weight.ToString(), tx - 10, ty - 10, 20, textColor);
        }

        public void SetWeight(int weight_)
        {
            weight = weight_;
        }

    }
}

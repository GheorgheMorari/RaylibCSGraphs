using RaylibSharp.Raylib.Types;
using System.Collections.Generic;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public class Nodes
    {
        public Vector2 pos;      //the vector of the positions use pos.x and pos.y
        public int index;        //the index of the node
        public int dist;     //the distance from the node 0
        public float radius = 20;//the radius of the node
        public float border = 2; //border radius + radius
        bool IsRoot = false;
        Color baseColor;
        Color borderColor;
        Color highlight;


        public Nodes() // basic initialisation to exclude uninitialised objects used
        {
        }
        public Nodes(Vector2 pos_, int index_)
        {
            pos = pos_;
            baseColor = new Color(255, 0, 0);
            highlight = new Color(255, 215, 0);
            borderColor = new Color(130, 45, 45);
            index = index_;
            if (index == 0) IsRoot = true;
        }

        public void Show(bool showDistance = false)
        {

            DrawCircleV(pos, radius + border, borderColor); //draw border
            DrawCircleV(pos, radius, baseColor); //draw circle

            if(!showDistance)
                DrawText(index.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
            else
                if (dist == int.MaxValue)
                    DrawText("INF", (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
                else
                    DrawText(dist.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
        }

        public void Highlight()
        {

            DrawCircleV(pos, radius + border, borderColor); //draw border
            DrawCircleV(pos, radius, highlight); //draw circle

            DrawText(index.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
        }

    }
    public static class Connections
    {
        static Color connectionColor = new Color(200, 200, 200);
        static Color textColor = new Color(0, 0, 0);

        public static void ShowConnection(Nodes node1, Nodes node2, int weight)
        {
            DrawLineEx(node1.pos, node2.pos, 3, connectionColor);
            float x1 = node1.pos.x;
            float x2 = node2.pos.x;
            float y1 = node1.pos.y;
            float y2 = node2.pos.y;

            if (weight != -1)
                DrawText(weight.ToString(), (int)(x1 + x2) / 2 - 10, (int)(y1 + y2) / 2 - 10, 20, textColor);
        }

    }
}

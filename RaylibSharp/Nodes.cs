using RaylibSharp.Raylib.Types;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public class Nodes
    {
        public Vector2 pos;         //the vector of the positions use pos.x and pos.y
        public int index;           //the index of the node
        public int dist;            //the distance from the rootNode
        public int flow;            //the maximum possible flow
        public float radius = 17;   //the radius of the node
        public float border = 2;    //border radius + radius
        private Color baseColor;
        private Color borderColor;
        private Color highlight;
        private Color rootColor;

        public Nodes() // basic initialisation to exclude uninitialised objects used
        {
        }

        public Nodes(Vector2 pos_, int index_)
        {
            pos = pos_;
            baseColor = new Color(255, 0, 0);
            highlight = new Color(255, 215, 0);
            borderColor = new Color(130, 45, 45);
            rootColor = new Color(130, 244, 0);
            index = index_;
        }

        public void Show(bool showDistance, bool showFlow, int currentRoot)
        {
            DrawCircleV(pos, radius + border, borderColor); //draw border
            if (currentRoot == index)
                DrawCircleV(pos, radius, rootColor); //draw circle
            else
                DrawCircleV(pos, radius, baseColor); //draw circle

            if (!showDistance && !showFlow) //show index
                DrawText(index.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
            else
            if (showFlow) //show maxflow
            {
                if (flow == int.MaxValue)
                    DrawText("INF", (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
                else
                    DrawText(flow.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
            }
            else
            if (dist == int.MaxValue) //show distance from root node
                DrawText("INF", (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
            else
                DrawText(dist.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
        }

        public void Highlight() //method that draws the circle in the main color and the border around it
        {
            DrawCircleV(pos, radius + border, borderColor); //draw border
            DrawCircleV(pos, radius, highlight); //draw circle

            DrawText(index.ToString(), (int)(pos.x - radius / 3.5), (int)(pos.y - radius / 2.7), 20, Color.BLACK);
        }
    }
}
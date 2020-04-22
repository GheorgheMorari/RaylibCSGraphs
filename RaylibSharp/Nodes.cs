using RaylibSharp.Raylib.Types;
using System;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public class Nodes
    {
        public Vector2 pos;         //the vector of the positions use pos.x and pos.y
        private Vector2 originalPos; //save the original position so it doesn't get lost
        public Vector2 modpos;      //the transformed position of the node
        public int index;           //the index of the node
        public float radius = 10; //the radius of the node
        public const float border = 1;    //border radius + radius
        private Color baseColor;
        private Color borderColor;
        private Color highlight;
        private Color centerColor;
        private bool high = false;
        public bool center = false;

        public Nodes() // basic initialisation to exclude uninitialised objects used
        {
        }

        public Nodes(Vector2 pos_, int index_, Nodes centerNode)
        {
            baseColor = new Color(255, 0, 0);
            highlight = new Color(255, 215, 0);
            centerColor = new Color(24, 240, 13);
            borderColor = new Color(130, 45, 45);
            index = index_;
            originalPos.x = pos_.x - centerNode.pos.x;
            originalPos.y = pos_.y - centerNode.pos.y;
            pos = originalPos;
            modpos = pos;
        }

        public Nodes(Vector2 pos_, int index_)
        {
            pos = pos_;
            baseColor = new Color(255, 0, 0);
            highlight = new Color(255, 215, 0);
            centerColor = new Color(24, 240, 13);
            borderColor = new Color(130, 45, 45);
            index = index_;
            originalPos = pos;
            modpos = pos;
        }

        public void transform(float[,] TMatrix, Nodes centerNode, float angle = 0)
        {
            modpos.x = pos.x * (1 - TMatrix[0, 0] + (float)Math.Cos(angle)) - pos.y * (TMatrix[0, 1] - (float)Math.Sin(angle)) + centerNode.pos.x;
            modpos.y = -pos.x * (TMatrix[1, 0] + (float)Math.Sin(angle)) + pos.y * (1 - TMatrix[1, 1] + (float)Math.Cos(angle)) + centerNode.pos.y;
        }

        public void setCenter(Nodes centerNode)
        {
            pos.x = originalPos.x - centerNode.originalPos.x;
            pos.y = originalPos.y - centerNode.originalPos.y;
            center = false;
        }

        public static Nodes makeCenter(Nodes centerNode)
        {
            centerNode.center = true;
            centerNode.pos = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2);
            centerNode.modpos = centerNode.pos;
            return centerNode;
        }

        public void Show()
        {
            DrawCircleV(modpos, radius + border, borderColor); //draw border
            if (high)
            {
                DrawCircleV(modpos, radius, highlight); //draw highlight
                high = false;
            }
            else
                if (center)
                DrawCircleV(modpos, radius, centerColor); //draw center
            else
                DrawCircleV(modpos, radius, baseColor); //draw circle
        }

        public void Highlight() //method that draws the circle in the main color and the border around it
        {
            high = true;
        }
    }
}
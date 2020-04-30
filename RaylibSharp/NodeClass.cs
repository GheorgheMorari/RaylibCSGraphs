using RaylibSharp.Raylib.Types;
using System;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public class NodeClass
    {
        public Vector2 TemporaryPos;
        public Vector2 OriginalPos;
        public Vector2 TransformedPos;

        public int NodeIndex;

        public const float NodeRadius = 10;
        public const float NodeBorderWidth = 1;

        private Color BaseColor;
        private Color BorderColor;
        private Color HighlightColor;
        private Color CenterColor;

        private bool IsNodeHighlited = false;
        public bool IsNodeCenter = false;

        public NodeClass() // basic initialisation to exclude uninitialised objects used
        {
        }

        public NodeClass(Vector2 pos_, int index_, NodeClass centerNode)
        {
            BaseColor = new Color(255, 0, 0);
            HighlightColor = new Color(255, 215, 0);
            CenterColor = new Color(24, 240, 13);
            BorderColor = new Color(130, 45, 45);
            NodeIndex = index_;
            OriginalPos.x = pos_.x - centerNode.TemporaryPos.x;
            OriginalPos.y = pos_.y - centerNode.TemporaryPos.y;
            TemporaryPos = OriginalPos;
            TransformedPos = TemporaryPos;
        }

        public NodeClass(Vector2 pos_, int index_)
        {
            TemporaryPos = pos_;
            BaseColor = new Color(255, 0, 0);
            HighlightColor = new Color(255, 215, 0);
            CenterColor = new Color(24, 240, 13);
            BorderColor = new Color(130, 45, 45);
            NodeIndex = index_;
            OriginalPos = TemporaryPos;
            TransformedPos = TemporaryPos;
        }

        public void Transform(float[,] TMatrix, NodeClass centerNode, float angle = 0)
        {
            TransformedPos.x = TemporaryPos.x * (1 - TMatrix[0, 0] + (float)Math.Cos(angle)) - TemporaryPos.y * (TMatrix[0, 1] - (float)Math.Sin(angle)) + centerNode.TemporaryPos.x;
            TransformedPos.y = -TemporaryPos.x * (TMatrix[1, 0] + (float)Math.Sin(angle)) + TemporaryPos.y * (1 - TMatrix[1, 1] + (float)Math.Cos(angle)) + centerNode.TemporaryPos.y;
        }

        public void OffsetNodeToCenter(NodeClass centerNode)
        {
            TemporaryPos.x = OriginalPos.x - centerNode.OriginalPos.x;
            TemporaryPos.y = OriginalPos.y - centerNode.OriginalPos.y;
            IsNodeCenter = false;
        }

        public bool CheckIfHitBy(Vector2 MousePosition)
        {
            return CheckCollisionPointCircle(MousePosition, TransformedPos, NodeRadius);
        }

        public static NodeClass MakeCenter(NodeClass centerNode)
        {
            centerNode.IsNodeCenter = true;
            centerNode.TemporaryPos = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2);
            centerNode.TransformedPos = centerNode.TemporaryPos;
            return centerNode;
        }

        public void DisplayNode()
        {
            DrawCircleV(TransformedPos, NodeRadius + NodeBorderWidth, BorderColor); //draw border
            if (IsNodeHighlited)
            {
                DrawCircleV(TransformedPos, NodeRadius, HighlightColor); //draw highlight
                IsNodeHighlited = false;
            }
            else
                if (IsNodeCenter)
                DrawCircleV(TransformedPos, NodeRadius, CenterColor); //draw center
            else
                DrawCircleV(TransformedPos, NodeRadius, BaseColor); //draw circle
        }

        public void Highlight() //method that draws the circle in the main color and the border around it
        {
            IsNodeHighlited = true;
        }
    }
}
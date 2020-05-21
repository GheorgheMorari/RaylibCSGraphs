using RaylibSharp.Raylib.Types;
using System;
using static RaylibSharp.Raylib.Raylib;

//This is the NodeClass file, where all operations
//that involve nodes are made.
//The nodes in this program are the the points where a segment end.
//

namespace RaylibSharp
{
    public class NodeClass
    {
        public Vector2 TemporaryPos;
        public Vector2 OriginalPos;
        public Vector2 TransformedPos;

        public const float NodeRadius = 7;
        public const float NodeBorderWidth = 0.5f;

        private Color BaseColor;
        private Color BorderColor;
        private Color HighlightColor;
        private Color CenterColor;

        public int NodeIndex;

        private bool IsNodeHighlited = false;
        public bool IsNodeCenter = false;

        public NodeClass() // basic initialisation to exclude uninitialised objects used
        {
        }

        public NodeClass(Vector2 pos_, int index_, NodeClass centerNode) //create node
        {
            BaseColor = new Color(255, 0, 0);
            HighlightColor = new Color(255, 215, 0);
            CenterColor = new Color(24, 240, 13);
            BorderColor = new Color(130, 45, 45);
            NodeIndex = index_;
            OriginalPos.x = pos_.x - centerNode.TemporaryPos.x;
            OriginalPos.y = pos_.y - centerNode.TemporaryPos.y;
            TemporaryPos = OriginalPos;
            TransformedPos = OriginalPos;
        }

        public NodeClass(Vector2 pos_, int index_) //create center
        {
            TemporaryPos = pos_;
            BaseColor = new Color(255, 0, 0);
            HighlightColor = new Color(255, 215, 0);
            CenterColor = new Color(24, 240, 13);
            BorderColor = new Color(130, 45, 45);
            NodeIndex = index_;
            OriginalPos = pos_;
            TransformedPos = TemporaryPos;
            TransformedPos.x = OriginalPos.x - TemporaryPos.x;
            TransformedPos.y = OriginalPos.y - TemporaryPos.y;
            IsNodeCenter = true;
        }

        public void TransformCoordinates(float[,] TMatrix, NodeClass centerNode,
                              float angle)
        {
            //Scaling
            float scaleX = TMatrix[0, 0];
            float scaleY = TMatrix[1, 1];

            TransformedPos.x = (OriginalPos.x + centerNode.TransformedPos.x) * scaleX;
            TransformedPos.y = (OriginalPos.y + centerNode.TransformedPos.y) * scaleY;

            //Skewing
            float skewX = TransformedPos.y * TMatrix[0, 1];
            float skewY = TransformedPos.x * TMatrix[1, 0];

            TransformedPos.x += skewX;
            TransformedPos.y += skewY;

            //Rotation
            TemporaryPos.x = TransformedPos.x * (float)Math.Cos(angle)
                           - TransformedPos.y * (float)Math.Sin(angle);

            TemporaryPos.y = TransformedPos.x * (float)Math.Sin(angle)
                           + TransformedPos.y * (float)Math.Cos(angle);

            //Final coordinates
            TransformedPos.x = TemporaryPos.x + centerNode.TemporaryPos.x;
            TransformedPos.y = TemporaryPos.y + centerNode.TemporaryPos.y;
        }

        public bool CheckIfHitBy(Vector2 MousePosition) //check if point is within circle
        {
            return CheckCollisionPointCircle(MousePosition, TransformedPos, NodeRadius);
        }

        public void DisplayNode()
        {
            if (IsNodeCenter)
            {
                DrawCircleV(TemporaryPos, NodeRadius - 4, CenterColor); //draw center
            }
            else
            {
                DrawCircleV(TransformedPos, NodeRadius + NodeBorderWidth, BorderColor); //draw border
                if (IsNodeHighlited)
                {
                    DrawCircleV(TransformedPos, NodeRadius, HighlightColor); //draw highlight
                    IsNodeHighlited = false;
                }
                else
                    DrawCircleV(TransformedPos, NodeRadius, BaseColor); //draw circle
            }
        }

        public void Highlight() //method that draws the circle in the main color and the border around it
        {
            IsNodeHighlited = true;
        }
    }
}
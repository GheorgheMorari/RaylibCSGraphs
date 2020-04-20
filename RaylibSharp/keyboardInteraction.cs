using RaylibSharp.Raylib.Types;
using System;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public class keyboardInteraction
    {
        private const float increment = (float)0.1;
        private const float aincrement = (float)3.14 / 180;

        private static void transformations() //Transformation changes
        {
            if (IsKeyDown(Raylib.KeyboardKey.KeyQ)) //decrease angle
                Program.angle -= aincrement;
            if (IsKeyDown(Raylib.KeyboardKey.KeyE)) //increase angle
                Program.angle += aincrement;

            if (IsKeyDown(Raylib.KeyboardKey.KeyW))
                Program.TMatrix[1, 1] -= increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyS))
                Program.TMatrix[1, 1] += increment;

            if (IsKeyDown(Raylib.KeyboardKey.KeyA))
                Program.TMatrix[1, 0] -= increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyD))
                Program.TMatrix[1, 0] += increment;

            if (IsKeyDown(Raylib.KeyboardKey.KeyDown))
                Program.TMatrix[0, 1] -= increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyUp))
                Program.TMatrix[0, 1] += increment;

            if (IsKeyDown(Raylib.KeyboardKey.KeyLeft))
                Program.TMatrix[0, 0] -= increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyRight))
                Program.TMatrix[0, 0] += increment;

            foreach (Nodes thisNode in Program.node)
            {
                if (thisNode != Program.centerNode)
                {
                    thisNode.transform(Program.TMatrix, Program.centerNode, Program.angle);
                }
            }
        }

        public static void keyboardInteractions()
        {
            //DISPLAY TOGGLES
            if (IsKeyPressed(Raylib.KeyboardKey.KeyC))//toggle connections
                Program.showConnections = !Program.showConnections;

            if (IsKeyPressed(Raylib.KeyboardKey.KeyV))//toggle circles
                Program.showCircles = !Program.showCircles;

            if (IsKeyPressed(Raylib.KeyboardKey.KeyG))//toggle text
                Program.showText = !Program.showText;

            transformations();

            //Import the matrix by pressing I
            if (IsKeyPressed(Raylib.KeyboardKey.KeyI))
            {
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "export.txt");
            }
            //Export the matrix by pressing W
            if (IsKeyPressed(Raylib.KeyboardKey.KeyW))
            {
                //Export matrix
                ReadingTxt.ReadingWriting.ExportMatrix(Program.matrix, Program.node);
            }

            if (IsKeyPressed(Raylib.KeyboardKey.KeyT)) //reset TMatrix
            {
                Program.TMatrix = new float[2, 2] { { 1, 0 }, { 0, 1 } };
            }
            if (IsKeyPressed(Raylib.KeyboardKey.KeyF)) //reset angle
            {
                Program.angle = 0;
            }

            if (IsKeyPressed(Raylib.KeyboardKey.KeyZ) && Program.edit) //randomise connections
            {
                for (int i = 0; i < Program.matrix.GetLength(0); i++)
                    for (int j = 0; j < Program.matrix.GetLength(0); j++)
                    {
                        int rnd = GetRandomValue(-(Program.node.Count * 10), 100);
                        if (rnd < 0) rnd = 0;
                        Program.matrix[i, j] = rnd;
                        Program.matrix[j, i] = rnd;
                    }
            }
            if (IsKeyPressed(Raylib.KeyboardKey.KeyOne))
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "1.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyTwo))
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "2.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyThree))
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "3.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyFour))
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "4.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyFive))
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "5.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeySix))
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "6.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeySeven))
                Program.matrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.node, "7.txt");
        }
    }
}
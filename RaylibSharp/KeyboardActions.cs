using RaylibSharp.Raylib.Types;
using System.Collections.Generic;
using static RaylibSharp.Raylib.Raylib;

namespace RaylibSharp
{
    public static class KeyboardInteraction
    {
        private const float increment = (float)0.1;
        private const float aincrement = (float)3.14 / 180;

        private static float[,] CMatrix = { { Program.TransformationMatrix[0, 0], Program.TransformationMatrix[0, 1] },
                                 { Program.TransformationMatrix[1, 0], Program.TransformationMatrix[1, 1] } };

        private static float C_angle = Program.TransformationAngle;

        public static void Change()//Recalculate the coordinates according to the changed matrix
        {
            foreach (var thisNode in Program.NodeList)
            {
                thisNode.TransformCoordinates(Program.TransformationMatrix, Program.CenterNode
                            , Program.TransformationAngle);
            }
        }

        private static void Transformations() //Key presses that change transformation
        {
            if (IsKeyPressed(Raylib.KeyboardKey.KeyR)) //reset TMatrix
            {
                Program.TransformationMatrix = new float[2, 2] { { 1, 0 }, { 0, 1 } };
            }
            if (IsKeyPressed(Raylib.KeyboardKey.KeyF)) //reset angle
            {
                Program.TransformationAngle = 0;
            }
            if (IsKeyPressed(Raylib.KeyboardKey.KeyT)) //reset offset
            {
                Program.CenterNode = new NodeClass(new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2), 0);
                Program.CenterNode.TransformedPos.x = Program.CenterNode.OriginalPos.x - Program.CenterNode.TemporaryPos.x;
                Program.CenterNode.TransformedPos.y = Program.CenterNode.OriginalPos.y - Program.CenterNode.TemporaryPos.y;
            }

            if (IsKeyDown(Raylib.KeyboardKey.KeyQ)) //decrease angle
                Program.TransformationAngle -= aincrement;
            if (IsKeyDown(Raylib.KeyboardKey.KeyE)) //increase angle
                Program.TransformationAngle += aincrement;

            if (IsKeyDown(Raylib.KeyboardKey.KeyW))
                Program.TransformationMatrix[1, 1] += increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyS))
                Program.TransformationMatrix[1, 1] -= increment;

            if (IsKeyDown(Raylib.KeyboardKey.KeyA))
                Program.TransformationMatrix[0, 0] -= increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyD))
                Program.TransformationMatrix[0, 0] += increment;

            if (IsKeyDown(Raylib.KeyboardKey.KeyDown))
                Program.TransformationMatrix[1, 0] -= increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyUp))
                Program.TransformationMatrix[1, 0] += increment;

            if (IsKeyDown(Raylib.KeyboardKey.KeyLeft))
                Program.TransformationMatrix[0, 1] -= increment;
            if (IsKeyDown(Raylib.KeyboardKey.KeyRight))
                Program.TransformationMatrix[0, 1] += increment;

            //Verify if matrix or angle has changed, if yes then recalculate
            bool MatrixChange = (CMatrix[0, 0] != Program.TransformationMatrix[0, 0] ||
                                 CMatrix[0, 1] != Program.TransformationMatrix[0, 1] ||
                                 CMatrix[1, 0] != Program.TransformationMatrix[1, 0] ||
                                 CMatrix[1, 1] != Program.TransformationMatrix[1, 1]);
            if (C_angle != Program.TransformationAngle || MatrixChange)
            {
                Change();
                CMatrix[0, 0] = Program.TransformationMatrix[0, 0];
                CMatrix[0, 1] = Program.TransformationMatrix[0, 1];
                CMatrix[1, 0] = Program.TransformationMatrix[1, 0];
                CMatrix[1, 1] = Program.TransformationMatrix[1, 1];
                C_angle = Program.TransformationAngle;
            }
        }

        public static void KeyboardInteractions()
        {
            //DisplayToggles
            if (IsKeyPressed(Raylib.KeyboardKey.KeyC))//toggle connections
                Program.showConnections = !Program.showConnections;

            if (IsKeyPressed(Raylib.KeyboardKey.KeyV))//toggle circles
                Program.ShowNodes = !Program.ShowNodes;

            if (IsKeyPressed(Raylib.KeyboardKey.KeyG))//toggle text
                Program.ShowText = !Program.ShowText;

            //Transformation related keyboard interactions
            Transformations();

            //Import the matrix by pressing I
            if (IsKeyPressed(Raylib.KeyboardKey.KeyI))
            {
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "export.txt");
            }
            //Export the matrix by pressing O
            if (IsKeyPressed(Raylib.KeyboardKey.KeyO))
            {
                ReadingTxt.ReadingWriting.ExportMatrix(Program.AdjacencyMatrix, Program.NodeList);
            }

            if (IsKeyPressed(Raylib.KeyboardKey.KeyZ) && Program.edit) //randomise connections
            {
                for (int i = 0; i < Program.AdjacencyMatrix.GetLength(0); i++)
                    for (int j = 0; j < Program.AdjacencyMatrix.GetLength(0); j++)
                    {
                        int rnd = GetRandomValue(-(Program.NodeList.Count * 10), 100);
                        if (rnd < 0) rnd = 0;
                        Program.AdjacencyMatrix[i, j] = rnd;
                        Program.AdjacencyMatrix[j, i] = rnd;
                    }
            }

            if (IsKeyPressed(Raylib.KeyboardKey.KeyInsert) && Program.edit) //randomise connections
            {
                Program.NodeList = new List<NodeClass>();
                Program.AdjacencyMatrix = Program.ResizeArray(Program.AdjacencyMatrix, 1);
                Program.CenterNode = new NodeClass(new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2), 0);
                Program.CenterNode.TransformedPos.x = Program.CenterNode.OriginalPos.x - Program.CenterNode.TemporaryPos.x;
                Program.CenterNode.TransformedPos.y = Program.CenterNode.OriginalPos.y - Program.CenterNode.TemporaryPos.y;
                Program.IsSelected = false;
            }
            //Key presses that load the respective images from files
            if (IsKeyPressed(Raylib.KeyboardKey.KeyOne))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "1.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyTwo))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "2.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyThree))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "3.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyFour))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "4.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyFive))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "5.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeySix))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "6.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeySeven))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "7.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyEight))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "8.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyNine))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "9.txt");
            if (IsKeyPressed(Raylib.KeyboardKey.KeyZero))
                Program.AdjacencyMatrix = ReadingTxt.ReadingWriting.ImportMatrix(out Program.NodeList, "0.txt");
        }
    }
}
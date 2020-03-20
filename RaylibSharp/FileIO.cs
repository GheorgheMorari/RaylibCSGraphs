using RaylibSharp.Raylib.Types;
using System;
using System.Collections.Generic;
using System.IO;
using RaylibSharp;

namespace ReadingTxt
{
    public class ReadingWriting
    {
        //importing matrix
        public static int[,] ImportMatrix(out List<Nodes> node , out int[,] matrix, string filename = "import.txt")
        {
            node = new List<Nodes>();
            int n;//size of the matrix
            string line;
            StreamReader file = new StreamReader(@"../../" + filename);

            n = Int32.Parse(file.ReadLine());
            matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                line = file.ReadLine();
                var lines = line.Trim().Split(' ');
                for (int j = 0; j < n; j++)
                {
                    int.TryParse(lines[j].Trim(), out matrix[i, j]);//1-st line number of nodes
                }
            }

            n = Int32.Parse(file.ReadLine());
            for (int k = 0; k < n; k++)
            {
                Vector2 coords = new Vector2();
                coords.x = Int32.Parse(file.ReadLine());
                coords.y = Int32.Parse(file.ReadLine());
                node.Add(new Nodes(coords, node.Count));
            }

            file.Close();
            matrix = Program.ResizeArray(matrix, n);
            return matrix;
        }

        public static void ExportMatrix(int[,] matrix, List<Nodes> node, string filename = "export.txt") //exporting matrix
        {
            int len = matrix.GetLength(0);
            int count = 0;
            using (TextWriter filePath = new StreamWriter(@"../../" + filename))//location of the file
            {
                filePath.WriteLine(len);
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len; j++)
                    {
                        if (matrix[i, j] > 0)
                        {
                            filePath.Write(matrix[i, j] + " ");
                            count++;
                        }
                        else if (matrix[j, i] > 0)
                        {
                            filePath.Write(matrix[j, i] + " ");
                            count++;
                        }
                        else
                            filePath.Write(0 + " ");
                    }
                    filePath.WriteLine();
                }
                filePath.WriteLine(node.Count.ToString());

                for (int i = 0; i < node.Count; i++)
                {
                    filePath.WriteLine(((int)node[i].pos.x).ToString());
                    filePath.WriteLine(((int)node[i].pos.y).ToString());
                }

                filePath.WriteLine("Number of vertices in the graph:" + len);
                filePath.WriteLine("Number of edges in the graph:" + (count / 2));
            }
        }
    }
}
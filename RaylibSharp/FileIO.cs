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
        public static int[,] ImportMatrix(List<Nodes> node)
        {
            Random rnd = new Random(); //random location for nodes
            float x = 650, y = 360;
            int[,] matrix1 = new int[10, 10];
            int n = 0;//size of the matrix
            int counter = 0;
            string line;
            StreamReader file = new StreamReader(@"D:\import.txt");//IMPORTANT!! to not forget to change location of the IMPORT file if it needed
            int i = -1, j;
            while ((line = file.ReadLine()) != null)
            {
                j = 0;
                foreach (var line1 in line.Trim().Split(' '))
                {
                    if (counter == 0) //reading size of the input matrix
                    {
                        int.TryParse(line1.Trim(), out n);//1-st line number of nodes
                        counter++;
                        for (int m = 0; m < n; m++)
                        {
                            Vector2 pos1 = new Vector2(x, y);
                            node.Add(new Nodes(pos1, node.Count)); //placing randomly node
                            x = rnd.Next(60, 1200); //generating random variable of position x
                            y = rnd.Next(70, 700); //generating random variable of position y
                        }
                    }
                    else
                    {
                        int.TryParse(line1.Trim(), out matrix1[i, j]);//weight of an edge
                        if (j < n)
                            j++;
                    }
                }
                if (i < n)
                    i++;
            }
            file.Close();
            matrix1 = Program.ResizeArray(matrix1, n);
            return matrix1;
        }
        public static void ExportMatrix(int[,] matrix) //exporting matrix
        {

            int len = matrix.GetLength(0);
            int count = 0;
            using (TextWriter filePath = new StreamWriter(@"D:\export.txt"))//location of the file
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
                filePath.WriteLine("Number of vertices in the graph:" + len);
                filePath.WriteLine("Number of edges in the graph:" + (count / 2));
            }
        }


    }

}      
     
  


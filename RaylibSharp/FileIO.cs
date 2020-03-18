using System;
using System.IO;

namespace ReadingTxt
{
    class ReadingWriting
    {
        
       
        public static void ReadingT(string txtName)
        {
            int[,] matrix1 = new int[100, 100];
            String input = File.ReadAllText(txtName);
            int i = -1, j = 0;
            int n = 0;//size of the matrix
            int counter = 0;           
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\Uni\some.txt");
            while ((line = file.ReadLine()) != null)
            {
                j = 0;
                foreach (var line1 in line.Trim().Split(' '))
                {
                    if (counter == 0) //Reading size of the input matrix
                    {
                        int.TryParse(line1.Trim(), out n);//first row number of rows
                        counter++;
                    }
                    else
                    {
                        int.TryParse(line1.Trim(), out matrix1[i, j]);
                        if (j < n) 
                            j++;
                    }
                }
                if (i < n)
                    i++;
            }
            file.Close();
            WritingTxt(matrix1, @"D:\Uni\some1.txt");

        }
             public static void WritingTxt(int [,]matrix,string txtName)
             { 
                    using (TextWriter filePath = new StreamWriter(txtName))
                    {
                        for (int  i = 0; i < matrix.GetLength(0); i++)
                        {
                            for (int j = 0; j < matrix.GetLength(0); j++)
                            {
                                filePath.Write(matrix[i, j] + " ");
                       
                            }
                            filePath.WriteLine();
                        }
                
            }
            
             }
        /*public static void Main()
        {
            ReadingT(@"D:\Uni\some.txt");
            
        }*/
    }
          
}      
     
  


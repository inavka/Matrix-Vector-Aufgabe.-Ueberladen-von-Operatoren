using System;

namespace Praktikum_01
{
    
    class MyMatrix
    {
        private int[,] data = new int[3, 3];

        public MyMatrix(bool unity = false)
        { 
            //Eimheitsmatrix
            if (unity)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        if (i == j)
                            data[i, j] = 1;
                    }
                }
            } else //Nullmatrix
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                            data[i, j] = 0;
                    }
                }
            }
        }
        public MyMatrix(int[,] data)
        {
            if (data.GetLength(0) == this.data.GetLength(0) && data.GetLength(1) == this.data.GetLength(1))
                this.data = data;
            else
                throw new ArgumentException("Die Matrize entspricht dem 3x3 Format nicht");
        }

        //Property, damit class Vector auf komplette Matrix.data zugreifen kann
        public int[,] Data 
        {
            get => data;
        }

        //Indexer-Property
        public int this[int index1, int index2] 
        {
            get
            {
                return data[index1, index2];
            }
            private set
            {
                data[index1, index2] = value;
            }
        }
        #region Überladen von Operatoren
        
        //Matrix-Matrix-Addition
        public static MyMatrix operator +(MyMatrix a, MyMatrix b) 
        {
            MyMatrix c = new MyMatrix();
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }

        //Matrix-Matrix-Subtraktion
        public static MyMatrix operator -(MyMatrix a, MyMatrix b)
        {
            MyMatrix c = new MyMatrix();
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }
            return c;
        }
        
        //Skalar-Matrix-Multiplikation
        public static MyMatrix operator *(int number, MyMatrix a)
        {
            MyMatrix c = new MyMatrix();
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    c[i, j] = number * a[i, j];
                }
            }
            return c;
        }

        //Matrix-Matrix-Multiplikation
        public static MyMatrix operator *(MyMatrix a, MyMatrix b)
        {
            MyMatrix c = new MyMatrix();
            for (int i = 0; i < c.data.GetLength(0); i++)
            {
                for (int j = 0; j < c.data.GetLength(1); j++) 
                {
                    for (int m = 0; m < a.data.GetLength(0); m++)
                    {
                        c[i, j] += b[m, j] * a[i, m];
                    }
                }
            }
            return c;
        }

        //Um Warnings loszuwerden, hier auskommentieren
        //public override bool Equals(object o) => true;
        //public override int GetHashCode() => 0;

        //Matrix-Matrix-Gleichheit
        public static bool operator ==(MyMatrix a, MyMatrix b)
        {
            bool isEqual = true;
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                        return !isEqual;
                }
            }
            return isEqual;
        }

        //Matrix-Matrix-Ungleichheit
        public static bool operator !=(MyMatrix a, MyMatrix b)
        {
            bool isEqual = false;
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                        return !isEqual;
                }
            }
            return isEqual;
        }

        //Implizite string-Konvertierung
        public static implicit operator string(MyMatrix a)
        {
            string matrix = "";
            int counter = 0;
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    ++counter;
                    if (counter < 3)
                        matrix += $"{a[i, j],-3}";
                    else
                    {
                        matrix += $"{a[i, j],-3}\n";
                        counter = 0;
                    }
                } 
            }
            return matrix;
        }

        //Explizite double-Konvertierung
        public static explicit operator double(MyMatrix a)
        {
            double sum = 0;
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    sum += Math.Pow(a[i, j], 2);
                }
            }
            return Math.Sqrt(sum);
        }
        #endregion
    }

    class MyVector
    {
        int[,] data = new int[3, 1];

        public MyVector(int[] data)
        {
            if (data.Length == this.data.GetLength(0))
            {
                for (int i = 0; i < this.data.GetLength(0); i++)
                {
                    for (int j = 0; j < this.data.GetLength(1); j++)
                    {
                        this.data[i, j] = data[i];
                    }
                }
            }
            else
                throw new ArgumentException("Der Vector ist nicht 3-dim.");
        }

        public MyVector()
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = 0;
                }
            }
        }

        //Indexer-Property
        public int this[int index1, int index2]
        {
            get
            {
                return data[index1, index2];
            }
            private set
            {
                data[index1, index2] = value;
            }
        }

        //Matrix-Vektor-Multiplikation
        public static MyVector operator *(MyMatrix a, MyVector b)
        {
            MyVector c = new MyVector();
            for (int i = 0; i < a.Data.GetLength(0); i++)
            {
                for (int j = 0; j < a.Data.GetLength(1); j++)
                {
                    c[i, 0] += b[j, 0] * a[i, j];
                }
            }
            return c;
        }

        //Implizite string-Konvertierung
        public static implicit operator string(MyVector a)
        {
            string vector = "";
            for (int i = 0; i < a.data.GetLength(0); i++)
            {
                for (int j = 0; j < a.data.GetLength(1); j++)
                {
                    vector += $"{a[i, j]}\n";
                }
            }
            return vector;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyMatrix unity = new MyMatrix(true);
            MyMatrix matA = new MyMatrix(
            new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            MyMatrix matB = new MyMatrix(
            new int[,] { { 1, 2, 3 }, { 1, 8, 1 }, { 1, 1, 1 } });
            Console.WriteLine("Output: ");
            Console.WriteLine(unity);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(matA[i, j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(matB);
            Console.WriteLine("Matrizenrechnungen:");
            Console.WriteLine(matA - matB);
            Console.WriteLine(matA + matB);
            Console.WriteLine(5 * matA);
            Console.WriteLine(matA * matB);
            Console.WriteLine((double)matA);
            Console.WriteLine((matA != matB));
            Console.WriteLine((matA == matB));
            Console.WriteLine("\nMatrix-Vector:");
            MyVector vec = new MyVector(new int[] { 3, 4, 7 });
            Console.WriteLine(matA * vec);
        }
    }
}


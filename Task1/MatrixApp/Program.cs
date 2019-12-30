using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MatrixApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var array1 = new double[,]
            {
                {1,5,6,7 },
                {2,6,1,10 },
                {42, 2, 2, 4 },
                {1, 32, 11, 23 }
            };
            var array2 = new double[,]
            {
                { 2, 7 },
                {2, 3 },
                {45, 12 },
                {20, 8 }
            };
            var array3 = new double[,]
            {
                {1,2,6,7 },
                {2,6,41,10 },
                {42, 23, 2, 4 },
                {1, 32, 11, 43 }
            };

            Matrix matrix = new Matrix(array1);
            Matrix matrix2 = new Matrix(array2);
            Matrix matrix3 = new Matrix(array3);


            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("matrix.dat", FileMode.OpenOrCreate))
            {
                // сериализуем весь массив people
                formatter.Serialize(fs, matrix);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация
            using (FileStream fs = new FileStream("matrix.dat", FileMode.OpenOrCreate))
            {
                Matrix deserilizePeople = (Matrix)formatter.Deserialize(fs);

                Console.WriteLine("Наша матрица");
                deserilizePeople.Show();
            }
            Console.ReadLine();
        }
    }
}

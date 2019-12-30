using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterArray;
using Tree;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {/*
            var ikari = new Student("Ikari", "Math", DateTime.Now, 100);
            var rey = new Student("Rey", "Psychology", new DateTime(1995, 7, 23), 60);
            var sinji = new Student("Sinji", "Psychology", new DateTime(1995, 8, 1), 400);
            var asuka = new Student("Asuka", "Psychology", new DateTime(1995, 4, 5), 10);
            var eva = new Student("Eva", "Biology", new DateTime(2015, 1, 1), 87);

            var students = new Student[]{ ikari, rey, sinji, asuka, eva };

            BinaryTree<Student> tree = new BinaryTree<Student>();
            tree.NodeAdded += new TreeEventHadler(AnnounceStudentAddition);
            tree.NodeRemoved += new TreeEventHadler(AnnounceStudentRemovement);

            foreach(var student in students)
            {
                tree.Add(student);
            }

            var PreOrderStudents = tree.Traverse(Traversal.PreOrder);
            var InOrderStudents = tree.Traverse(Traversal.InOrder);
            var OutOrderStudents = tree.Traverse(Traversal.OutOrder);

            PrintStudents(PreOrderStudents);
            Console.WriteLine();
            PrintStudents(InOrderStudents);
            Console.WriteLine();
            PrintStudents(OutOrderStudents);
            Console.WriteLine();


            tree.Remove(rey);
            tree.Remove(sinji);

            PrintStudents(tree.Traverse(Traversal.InOrder));

            Console.ReadLine();
        }

        static void PrintStudents(IEnumerable<Student> students)
        {
            foreach(var student in students)
            {
                Console.WriteLine(student.Name);
            }
        }

        static void AnnounceStudentAddition(object sender, TreeEventArgs e)
        {
            string value = ((Student)e.Value).Name;
            Console.WriteLine(String.Format("New node {0} was succesfully added", value));
        }

        static void AnnounceStudentRemovement(object sender, TreeEventArgs e)
        {
            string value = ((Student)e.Value).Name;
            Console.WriteLine(String.Format("New node {0} was succesfully removed", value));*/
        }
    }
}

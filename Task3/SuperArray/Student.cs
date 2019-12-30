using System;

namespace BinaryTree
{
    internal class Student
    {
        private string v1;
        private string v2;
        private DateTime now;
        private int v3;

        public Student(string v1, string v2, DateTime now, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.now = now;
            this.v3 = v3;
        }

        public string Name { get; internal set; }
    }
}
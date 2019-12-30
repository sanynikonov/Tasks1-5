using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Node<T>
    {
        public T Value { get; set; }
        public Node<T> LeftChild { get; set; }
        public Node<T> Parent { get; set; }
        public Node<T> RightChild { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    public enum Traversal
    {
        PreOrder,
        InOrder,
        OutOrder
    }

    public class TreeEventArgs : EventArgs
    {
        public object Value { get; set; }
    }

    public delegate void TreeEventHadler(object sender, TreeEventArgs e);

    public class BinaryTree<T> where T : IComparable<T>
    {
        public event TreeEventHadler NodeAdded = null;
        public event TreeEventHadler NodeRemoved = null;

        private Node<T> root;
        private List<T> elements = new List<T>();

        private void RecursiveAdd(Node<T> parentNode, T value)
        {
            if (parentNode.Value.CompareTo(value) > 0)
            {
                if (parentNode.LeftChild == null)
                {
                    parentNode.LeftChild = new Node<T>(value);
                    parentNode.LeftChild.Parent = parentNode;
                }
                else RecursiveAdd(parentNode.LeftChild, value);
            }
            else
            {
                if (parentNode.RightChild == null)
                {
                    parentNode.RightChild = new Node<T>(value);
                    parentNode.RightChild.Parent = parentNode;
                }
                else RecursiveAdd(parentNode.RightChild, value);
            }
        }

        public void Add(T value)
        {
            if (root == null)
                root = new Node<T>(value);
            else RecursiveAdd(root, value);
            if (NodeAdded != null)
            {
                TreeEventArgs e = new TreeEventArgs();
                e.Value = value;
                NodeAdded(this, e);
            }
        }

        private Node<T> MaxNode(Node<T> node)
        {
            if (node == null)
            {
                return node.Parent;
            }
            else
            {
                return MaxNode(node.RightChild);
            }
        }

        private Node<T> MinNode(Node<T> node)
        {
            if (node == null)
            {
                return node.Parent;
            }
            else
            {
                return MinNode(node.LeftChild);
            }
        }

        private Node<T> FindNodeByValue(Node<T> node, T value)
        {
            if (node == null)
                return null;
            else
            {
                int result = node.Value.CompareTo(value);
                if (result < 0)
                {
                    return FindNodeByValue(node.LeftChild, value);
                }
                else if (result == 0)
                {
                    return node;
                }
                else
                {
                    return FindNodeByValue(node.RightChild, value);
                }
            }
        }

        public bool Remove(T value)
        {

            //Node<T> node = FindNodeByValue(root, value);
            Node<T> node = root;

            do
            {
                if (value.CompareTo(node.Value) < 0)
                {
                    node = node.LeftChild;
                }
                else if (value.CompareTo(node.Value) > 0)
                {
                    node = node.RightChild;
                }

                if (node == null)
                {
                    return false;
                }

            } while (value.CompareTo(node.Value) != 0);

            if (node == null)
                return false;
            if (node != null)
            {
                int result = node.Value.CompareTo(node.Parent.Value);
                if (node.LeftChild == null && node.RightChild == null) // перший випадок - нема дітей
                {
                    if (node.Parent.LeftChild == node)
                    {
                        node.Parent.LeftChild = null;
                    }
                    else
                    {
                        node.Parent.RightChild = null;
                    }
                }
                else if ((node.LeftChild == null && node.RightChild != null)
                    || (node.LeftChild != null && node.RightChild != null)) // другий випадок - одна дитина
                {
                    if (node.LeftChild == null)
                    {
                        if (node.Parent.LeftChild == node)
                        {
                            node.Parent.LeftChild = node.RightChild;
                        }
                        else
                        {
                            node.Parent.RightChild = node.RightChild;
                        }
                    }
                    else
                    {
                        if (node.Parent.LeftChild == node)
                        {
                            node.Parent.LeftChild = node.LeftChild;
                        }
                        else
                        {
                            node.Parent.RightChild = node.LeftChild;
                        }
                    }
                }
                else // третій випадок - є дві дитини
                {
                    Node<T> succesor = node.RightChild;
                    while (succesor.LeftChild != null)
                    {
                        succesor = succesor.LeftChild;
                    }

                    node.Value = succesor.Value;
                    if (succesor.Parent.LeftChild == succesor)
                    {
                        succesor.Parent.LeftChild = succesor.RightChild;
                        if (succesor.RightChild != null)
                        {
                            succesor.RightChild.Parent = succesor.Parent;
                        }
                    }
                    else
                    {
                        succesor.Parent.RightChild = succesor.LeftChild;
                        if (succesor.LeftChild != null)
                        {
                            succesor.RightChild.Parent = succesor.Parent;
                        }
                    }
                }
            }
            if (NodeRemoved != null)
            {
                TreeEventArgs e = new TreeEventArgs();
                e.Value = value;
                NodeRemoved(this, e);
            }
            return true;
        }

        public bool Search(T value)
        {
            Node<T> node = root;
            while (node != null)
            {
                int comparison = value.CompareTo(node.Value);
                if (comparison == 0)
                {
                    return true;
                }
                else if (comparison > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    node = node.LeftChild;
                }
            }
            return false;
        }

        private void OutOrderTraverse(Node<T> node)
        {
            if (node != null)
            {
                if (node.LeftChild != null)
                {
                    OutOrderTraverse(node.LeftChild);
                }

                if (node.RightChild != null)
                {
                    OutOrderTraverse(node.RightChild);
                }
                elements.Add(node.Value);
            }
        }

        private void PreOrderTraverse(Node<T> node)
        {
            if (node != null)
            {
                elements.Add(node.Value);
                if (node.LeftChild != null)
                {
                    PreOrderTraverse(node.LeftChild);
                }
                if (node.RightChild != null)
                {
                    PreOrderTraverse(node.RightChild);
                }
            }

        }

        private void InOrderTraverse(Node<T> node)
        {
            if (node != null)
            {
                if (node.LeftChild != null)
                {
                    InOrderTraverse(node.LeftChild);
                }
                elements.Add(node.Value);
                if (node.RightChild != null)
                {
                    InOrderTraverse(node.RightChild);
                }
            }
        }

        public IEnumerable<T> Traverse(Traversal traversal)
        {
            elements.Clear();
            if (traversal == Traversal.OutOrder)
            {
                OutOrderTraverse(root);
            }
            else if (traversal == Traversal.PreOrder)
            {
                PreOrderTraverse(root);
            }
            else if (traversal == Traversal.InOrder)
            {
                InOrderTraverse(root);
            }
            IEnumerable<T> result = elements.ToList();
            return result;
        }
    }
}

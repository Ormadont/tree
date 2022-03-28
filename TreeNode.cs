using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace waGotikaMobile.Utilities
{
    public class TreeNode<T>
    {
        private readonly T _value;
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            _value = value;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode<T> Parent { get; private set; }

        public T Value { get { return _value; } }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }

        public void PrintStrNode(TreeNode<string> node)
        {
            string curValue = node.Value;
            Debug.WriteLine(curValue);
            if (node.Children.Count > 0)
            {
                foreach (TreeNode<string> item in node.Children)
                {
                    PrintStrNode(item);
                }
            }
        }
        public void PrintWSNode(TreeNode<WarehouseSpace> node)
        {
            if (node.Value != null)
            {
                string curValue = $"{node.Value.NodeLevel}: {node.Value.Код}: {node.Value.Наименование}";
                Debug.WriteLine(curValue);
            }
            if (node.Children.Count > 0)
            {
                foreach (TreeNode<WarehouseSpace> item in node.Children)
                {
                    PrintWSNode(item);
                }
            }
        }
    }
}
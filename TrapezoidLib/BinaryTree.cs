using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TrapezoidLib;

public class BinaryTree<T> : IEnumerable<T> where T : class
{
    private readonly IComparer<T> _comparer;
    private Node? _root;

    public BinaryTree() : this(Comparer<T>.Default)
    {
    }

    public BinaryTree(IComparer<T> comparer)
    {
        _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public int Count { get; private set; }

    public void Add(T item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        Node newNode = new Node(item);

        if (_root is null)
        {
            _root = newNode;
            Count++;
            return;
        }

        Node current = _root;
        while (true)
        {
            if (_comparer.Compare(item, current.Value) < 0)
            {
                if (current.Left is null)
                {
                    current.Left = newNode;
                    break;
                }

                current = current.Left;
            }
            else
            {
                if (current.Right is null)
                {
                    current.Right = newNode;
                    break;
                }

                current = current.Right;
            }
        }

        Count++;
    }

    public string GetStructure(Func<T, string> describe)
    {
        if (describe is null)
        {
            throw new ArgumentNullException(nameof(describe));
        }

        if (_root is null)
        {
            return "(дерево порожнє)";
        }

        StringBuilder builder = new StringBuilder();
        AppendStructure(_root, 0, "[корінь] ", describe, builder);
        return builder.ToString().TrimEnd();
    }

    private static void AppendStructure(Node node, int level, string label, Func<T, string> describe, StringBuilder builder)
    {
        builder.Append(new string(' ', level * 4));
        builder.Append(label);
        builder.AppendLine(describe(node.Value));

        if (node.Left is not null)
        {
            AppendStructure(node.Left, level + 1, "[лівий] ", describe, builder);
        }

        if (node.Right is not null)
        {
            AppendStructure(node.Right, level + 1, "[правий] ", describe, builder);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new InorderEnumerator(_root);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private sealed class Node
    {
        public Node(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public Node? Left { get; set; }

        public Node? Right { get; set; }
    }

    private sealed class InorderEnumerator : IEnumerator<T>
    {
        private readonly Node? _root;
        private readonly Stack<Node> _stack = new Stack<Node>();
        private Node? _current;

        public InorderEnumerator(Node? root)
        {
            _root = root;
            PushLeftBranch(root);
        }

        public T Current
        {
            get
            {
                if (_current is null)
                {
                    throw new InvalidOperationException("Ітератор стоїть до першого елемента або після останнього.");
                }

                return _current.Value;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_stack.Count == 0)
            {
                _current = null;
                return false;
            }

            Node node = _stack.Pop();
            _current = node;

            PushLeftBranch(node.Right);
            return true;
        }

        public void Reset()
        {
            _stack.Clear();
            _current = null;
            PushLeftBranch(_root);
        }

        public void Dispose()
        {
        }

        private void PushLeftBranch(Node? node)
        {
            while (node is not null)
            {
                _stack.Push(node);
                node = node.Left;
            }
        }
    }
}
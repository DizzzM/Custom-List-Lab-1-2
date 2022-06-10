using System;
using System.Collections.Generic;
using System.Text;

namespace CustomList.Library
{
    public sealed class CustomNode<T>
    {
        public CustomNode<T> Next { get; set; }
        public T Data { get; }

        public CustomNode() { }
        public CustomNode(T data) => Data = data;
    }
}

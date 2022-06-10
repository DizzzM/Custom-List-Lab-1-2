using System;
using System.Collections.Generic;
using System.Text;

namespace CustomList.Library
{
    public class CustomListEventHandler<T> : EventArgs where T : IComparable<T>
    {
        public CollectionActionType ActionType { get; set; }
        public T Value { get; set; }
    }

    public enum CollectionActionType
    {
        AddNode,
        RemoveNode,
        ClearCustomList
    }
}

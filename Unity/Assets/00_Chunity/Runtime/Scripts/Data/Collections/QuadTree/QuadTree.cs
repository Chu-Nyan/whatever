using System;
using System.Collections.Generic;

namespace Chunity.Data
{
    public class QuadTree<T> where T : IQuadTreeEntity
    {
        public const int MaxCount = 20;
        public const int MaxLevel = 4;

        private readonly QuadTreeNode<T> _root;
        private readonly Dictionary<int, QuadTreeNode<T>> _nodesByID;

        public QuadTree(RectBound bound)
        {
            int capacity = 0;
            for (int i = 0; i <= MaxLevel; i++)
            {
                capacity += (int)Math.Pow(4, i); ;
            }

            _nodesByID = new(capacity);
            _root = new(0, bound, 0);

            _nodesByID.Add(0, _root);
        }

        public void Insert(T entity)
        {
            Remove(entity);

            entity.ClearInsertedNodes();
            _root.Insert(entity);
        }

        public void Remove(T entity)
        {
            foreach (var nodeIndex in entity.InsertedNodesID)
            {
                var node = GetNode(nodeIndex);
                node.Remove(entity);
            }
        }

        public QuadTreeNode<T> GetNode(int index)
        {
            if (_nodesByID.TryGetValue(index, out var value) == false)
            {
                value = GetNode((index - 1) / 4);
                MappingNode(value);
                _nodesByID.TryGetValue(index, out value);
            }

            if (value.Index != index)
                throw new Exception("Index 불일치");
            return value;
        }

        private void MappingNode(QuadTreeNode<T> node)
        {
            var index = node.Index;
            _nodesByID.TryAdd(index, node);
            if (node.IsDivided == true)
            {
                for (var i = 0; i < node.ChildNodes.Length; i++)
                {
                    _nodesByID.TryAdd(node.ChildNodes[i].Index, node.ChildNodes[i]);
                }
            }
        }

        #region Debug
        public string DebugLog()
        {
            return _root.PrintLog();
        }

        public string DebugMapping()
        {
            string title = "쿼드 트리 노드 매핑 검사\n";
            string log = default;
            foreach (var item in _nodesByID)
            {
                if (item.Key != item.Value.Index)
                {
                    log += item.Key + "오류";
                }
            }
            if (log == default)
            {
                log = "정상적으로 매핑됨";
            }

            return title + log;
        }
        #endregion
    }
}

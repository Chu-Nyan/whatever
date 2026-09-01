using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Chunity.Data
{
    public class QuadTreeNode<T> where T : IQuadTreeEntity
    {
        private RectBound _boundary;
        private readonly int _index;
        private readonly int _level;
        private readonly HashSet<T> _entities;

        private bool _isDivided;
        private QuadTreeNode<T>[] _childNodes; // RT, LT, LB, RB

        private readonly float _halfX;
        private readonly float _halfY;

        public int Index
        {
            get => _index;
        }

        public bool IsDivided
        {
            get => _isDivided;
        }

        public QuadTreeNode<T>[] ChildNodes
        {
            get => _childNodes;
        }

        public HashSet<T> Entities
        {
            get => _entities;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIntersection(RectBound target)
        {
            return RectBound.IsIntersecting(_boundary, target);
        }

        public bool IsFullyInside(RectBound target)
        {
            return _boundary.IsFullyInside(target);
        }

        public QuadTreeNode(int level, RectBound area, int index)
        {
            _level = level;
            _boundary = area;
            _index = index;
            _entities = new(_level < QuadTree<T>.MaxLevel ? QuadTree<T>.MaxCount : QuadTree<T>.MaxCount * 2);
            _halfX = _boundary.HalfX;
            _halfY = _boundary.HalfY;
        }

        public void Insert(T handler)
        {
            if (RectBound.IsIntersecting(_boundary, handler.RectBound) == false)
                return;

            if (_isDivided == true)
                InsertChild(handler);
            else
            {
                _entities.Add(handler);
                handler.AddQuadTreeNodeID(_index);

                if (_entities.Count >= QuadTree<T>.MaxCount && _level < QuadTree<T>.MaxLevel)
                    Subdivide();
            }
        }

        public void Remove(T handler)
        {
            _entities.Remove(handler);
        }

        private void Subdivide()
        {
            if (_isDivided == true)
                throw new Exception("트리가 분열 된 상태에서 분열을 시도함");

            var startIndex = _index * 4;
            _childNodes = new QuadTreeNode<T>[4];

            _childNodes[0] = new(_level + 1, new(_halfX, _boundary.MaxX, _halfY, _boundary.MaxY), startIndex + 1);
            _childNodes[1] = new(_level + 1, new(_boundary.MinX, _halfX, _halfY, _boundary.MaxY), startIndex + 2);
            _childNodes[2] = new(_level + 1, new(_boundary.MinX, _halfX, _boundary.MinY, _halfY), startIndex + 3);
            _childNodes[3] = new(_level + 1, new(_halfX, _boundary.MaxX, _boundary.MinY, _halfY), startIndex + 4);

            foreach (var item in _entities)
            {
                item.ClearInsertedNodes();
                InsertChild(item);
            }

            _entities.Clear();
            _isDivided = true;
        }

        private void InsertChild(T entity)
        {
            var bound = entity.RectBound;

            if (bound.MinX >= _halfX && bound.MinY >= _halfY)
                _childNodes[0].Insert(entity);
            else if (bound.MaxX <= _halfX && bound.MinY >= _halfY)
                _childNodes[1].Insert(entity);
            else if (bound.MaxX <= _halfX && bound.MaxY <= _halfY)
                _childNodes[2].Insert(entity);
            else if (bound.MinX >= _halfX && bound.MaxY <= _halfY)
                _childNodes[3].Insert(entity);
            else
            {
                foreach (var item in _childNodes)
                {
                    if (item.IsIntersection(bound))
                    {
                        item.Insert(entity);
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"ID : {_index} Lv : {_level}, {_boundary}\n";
        }

        #region Debug
        public string PrintLog(string log = null)
        {
            log += ToString();
            var index = 0;
            foreach (var entity in _entities)
            {
                log += $"- obj{index} : {entity.RectBound}\n";
                index++;
            }

            if (_isDivided == true)
            {
                foreach (var item in _childNodes)
                {
                    log = item.PrintLog(log) + "\n";
                }
            }

            return log;
        }
        #endregion
    }
}

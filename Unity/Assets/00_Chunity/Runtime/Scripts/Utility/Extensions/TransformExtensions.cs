using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Utility
{
    public static class TransformExtensions
    {
        public static List<T> GetComponentsWithDepth<T>(this Transform root, int maxDepth, bool includeInactive = false)
        {
            List<T> result = new();
            Traverse(root, 0);
            return result;

            void Traverse(Transform current, int depth)
            {
                if (depth > maxDepth)
                    return;

                if (includeInactive == true || current.gameObject.activeInHierarchy)
                {
                    if (current.TryGetComponent<T>(out var comp))
                        result.Add(comp);
                }

                foreach (Transform child in current)
                {
                    Traverse(child, depth + 1);
                }
            }
        }
    }
}

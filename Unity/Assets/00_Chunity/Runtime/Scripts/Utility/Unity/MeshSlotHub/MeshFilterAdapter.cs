using UnityEngine;

namespace Chunity.Utility
{
    public class MeshFilterAdapter : IMeshAdapter
    {
        private readonly MeshFilter _filter;

        public MeshFilterAdapter(MeshFilter meshFilter)
        {
            _filter = meshFilter;
        }

        public void SetMesh(Mesh mesh)
        {
            _filter.sharedMesh = mesh;
        }
    }
}

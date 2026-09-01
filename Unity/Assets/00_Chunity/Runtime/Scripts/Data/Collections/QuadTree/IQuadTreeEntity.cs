using System.Collections.Generic;

namespace Chunity.Data
{
    public interface IQuadTreeEntity
    {
        public RectBound RectBound { get; }
        public HashSet<int> InsertedNodesID { get; }

        public void AddQuadTreeNodeID(int id);
        public void ClearInsertedNodes();
    }
}
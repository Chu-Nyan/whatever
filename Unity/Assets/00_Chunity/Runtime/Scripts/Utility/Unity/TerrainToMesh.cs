using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Chunity.Utility
{
    public class TerrainToMesh : MonoBehaviour
    {
        [SerializeField]
        private Terrain _terrain;
        [SerializeField]
        private Transform _treeRoot;
        [SerializeField]
        private StaticEditorFlags _staticFlags;

        [ContextMenu("오브젝트로 변경")]
        public void Convert()
        {
            TerrainData data = _terrain.terrainData;
            TreeInstance[] treeInstances = data.treeInstances.ToArray();
            TreePrototype[] protoTypes = data.treePrototypes.ToArray();
            Vector3 terrainPos = _terrain.transform.position;

            for (int i = _treeRoot.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(_treeRoot.GetChild(i).gameObject);
            }

            foreach (var tree in treeInstances)
            {
                GameObject prefab = protoTypes[tree.prototypeIndex].prefab;
                Vector3 pos = Vector3.Scale(tree.position, data.size) + terrainPos;
                pos.y = _terrain.SampleHeight(pos) + terrainPos.y;
                Quaternion rot = Quaternion.Euler(0, tree.rotation * Mathf.Rad2Deg, 0);
                GameObject obj = Instantiate(prefab, pos, rot, _treeRoot);
                GameObjectUtility.SetStaticEditorFlags(obj, _staticFlags);
                obj.transform.localScale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale);
            }
        }

        [ContextMenu("뷰 변경")]
        private void ClearTerrainTrees()
        {
            if (_treeRoot.gameObject.activeSelf == true)
            {
                _treeRoot.gameObject.SetActive(false);
                _terrain.treeDistance = 1000;
            }
            else
            {
                _treeRoot.gameObject.SetActive(true);
                _terrain.treeDistance = 0;
            }
        }
    }
}

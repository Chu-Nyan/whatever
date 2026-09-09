using UnityEngine;

namespace Whatever.GameSystems
{
    [CreateAssetMenu(fileName = "WorldProfile", menuName = "SO/WorldProfile")]
    public class WorldProfile : ScriptableObject
    {
        public FactionTable FactionTable;
    }
}

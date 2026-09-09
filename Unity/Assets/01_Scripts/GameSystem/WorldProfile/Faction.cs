using System;
using UnityEngine;

namespace Whatever.GameSystems
{
    [Serializable]
    public class Faction
    {
        [SerializeField]
        private FactionRelation[] _factionRelations;
        [SerializeField]
        private int _hostilityMask;

        public FactionRelation this[int index]
        {
            get => _factionRelations[index];
        }

        public FactionRelation this[FactionType type]
        {
            get => _factionRelations[(int)type];
        }

        public int Count
        {
            get => _factionRelations.Length;
        }

        public int HostilityMask
        {
            get => _hostilityMask;
        }

        public Faction(int factionCount)
        {
            _factionRelations = new FactionRelation[factionCount];
        }

        public void SetRelation(FactionType type, FactionRelation relation)
        {
            _factionRelations[(int)type] = relation;

            int bit = 1 << (int)type;

            if (relation == FactionRelation.Hostile)
                _hostilityMask |= bit;
            else
                _hostilityMask &= ~bit;
        }
    }
}

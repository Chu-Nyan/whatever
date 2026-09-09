using System;
using System.Collections.Generic;
using UnityEngine;

namespace Whatever.GameSystems
{
    [Serializable]
    public class FactionTable
    {
        [SerializeField]
        private Dictionary<FactionType, Faction> _factions;

        public Faction this[FactionType type]
        {
            get => _factions[type];
        }

        public FactionTable()
        {
            _factions = new();
            UpdateFaction();
        }

        public void UpdateFaction()
        {
            var factionList = (FactionType[])Enum.GetValues(typeof(FactionType));

            foreach (var type in factionList)
            {
                if (_factions.TryGetValue(type, out var faction) == false)
                {
                    faction = new(factionList.Length);
                    _factions.Add(type, faction);
                }

                var old = faction;
                faction = new(factionList.Length);

                if (old != null)
                {
                    int index = Math.Min(old.Count, faction.Count);
                    for (int i = 0; i < index; i++)
                    {
                        faction.SetRelation((FactionType)i, old[i]);
                    }
                }
            }
        }

        public void SetRelation(FactionType a, FactionType b, FactionRelation state)
        {
            _factions[a].SetRelation(b, state);
            _factions[b].SetRelation(a, state);
        }
    }
}

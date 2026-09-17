namespace Whatever.GameSystems
{
    // TODO : 데이터 저장용

    //public class InventoryLoadout
    //{
    //    private List<KeyValuePair<int, int>> _itemIdBySlot;
    //    private List<ItemInstancedata> _inst;
    //    private List<ConsumableInstanceData> _consumableInst;
    //    private List<EquipmentInstanceData> _equipmentInst;

    //    public InventoryLoadout()
    //    {
    //        _itemIdBySlot = new();
    //        _inst = new();
    //        _consumableInst = new();
    //        _equipmentInst = new();
    //    }

    //    public void Save(Inventory inventory)
    //    {
    //        _itemIdBySlot.Clear();
    //        _inst.Clear();
    //        _equipmentInst.Clear();
    //        _consumableInst.Clear();

    //        var itemList = inventory.List;
    //        for (int i = 0; i < itemList.Length; i++)
    //        {
    //            if (itemList[i] == null)
    //                continue;

    //            _itemIdBySlot.Add(new(i, itemList[i].Def.ID));
    //            _inst.Add(itemList[i].Inst);
    //            switch (itemList[i].Def.Category)
    //            {
    //                case ItemCategory.Consumable:
    //                    _consumableInst.Add(((Consumable)itemList[i]).ConsumableInst);
    //                    break;
    //                case ItemCategory.Equipment:
    //                    _equipmentInst.Add(((Equipment)itemList[i]).EquipmentInst);
    //                    break;
    //                default:
    //                    throw new System.InvalidCastException();
    //            }
    //        }
    //    }

    //    public void Load(Inventory inventory)
    //    {
    //        inventory.Clear();
    //        int consumableIndex = 0;
    //        int equipmentIndex = 0;

    //        for (int i = 0; i < _itemIdBySlot.Count; i++)
    //        {
    //            var item = ItemFactory.Instance.Create(_itemIdBySlot[i].Value, 1);
    //            item.Inst.Amount = _inst[i].Amount;
    //            if (item.Def.Category == ItemCategory.Consumable)
    //            {
    //                var consumableData = ((Consumable)item).ConsumableInst;
    //                var saveData = _consumableInst[consumableIndex];
    //                consumableIndex++;
    //                // TODO : 컨슘 에이블 데이터 복사
    //            }
    //            else if (item.Def.Category == ItemCategory.Equipment)
    //            {
    //                var equipmentData = ((Equipment)item).EquipmentInst;
    //                var saveData = _equipmentInst[equipmentIndex];
    //                equipmentIndex++;
    //            }

    //            inventory.TryPlaceSlot(item, _itemIdBySlot[i].Key);
    //        }
    //    }
    //}
}

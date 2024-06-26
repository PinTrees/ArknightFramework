using System.Collections.Generic;
using System;

[Serializable]
public class ItemData
{
    public string itemId;
    public string name;
    public string description;
    public string rarity;
    public string iconId;
    public string overrideBkg;
    public string stackIconId;
    public int sortId;
    public string usage;
    public string obtainApproach;
    public bool hideInItemGet;
    public string classifyType;
    public string itemType;
    public List<StageDrop> stageDropList;
    public List<BuildingProduct> buildingProductList;
    public object voucherRelateList;
}

[Serializable]
public class StageDrop
{
    public string stageId;
    public string occPer;
}

[Serializable]
public class BuildingProduct
{
    // Define properties if needed
}
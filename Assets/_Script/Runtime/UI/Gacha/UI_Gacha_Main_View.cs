using System.Collections.Generic;
using UnityEngine;

public class UI_Gacha_Main_View : UIViewBase
{
    public List<UI_Gacha_Info> gachaClientInfos = new();
    public uint currGachaClientIndex = 0;


    public override void Show()
    {
        base.Show();

        DatabaseManager.Instance.gachaTable.gachaPoolClient.ForEach(gachaClient =>
        {

        });
    }

    public override void Close()
    {
        base.Close();

    }
}

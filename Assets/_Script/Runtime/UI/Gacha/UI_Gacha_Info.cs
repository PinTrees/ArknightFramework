using UnityEngine;

public class UI_Gacha_Info : UIObjectBase
{

    public static UI_Gacha_Info Create()
    {
        // Get object in pool
        return new UI_Gacha_Info(); 
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Close()
    {
        base.Close();
        // Relese object to pool
    }
}

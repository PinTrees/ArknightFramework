using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_StarContainer : UIObjectBase
{
    [Header("Runtime Value")]
    public List<Image> startsImage = new();

    protected override void OnInit()
    {
        base.OnInit();
        startsImage = baseObject.GetComponentsInChildren<Image>().ToList();
    }

    public void Show(int starConunt)
    {
        base.Show();

        startsImage.ForEach(e => e.gameObject.SetActive(false));
        for(int i = 0; i < starConunt; ++i)
        {
            startsImage[i].gameObject.SetActive(true);
        }
    }

    public void Colse()
    {
        base.Close();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Area
{
    public string name;

    public GoodsType goodsType;

    public string goods1Name;
    public int goods1Count;
    public string goods2Name;
    public int goods2Count;
    public string goods3Name;
    public int goods3Count;

    public float nextActiveTime;

    public void AddGoods(int index, string name, int count)
    {
        switch (index)
        {
            case 1:
                goods1Name = name;
                goods1Count = count;
                break;
            case 2:
                goods2Name = name;
                goods2Count = count;
                break;
            case 3:
                goods3Name = name;
                goods3Count = count;
                break;
            default:
                break;
        }
    }
}

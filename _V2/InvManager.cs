using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class InvManager
{
	private static int[] Items = new int[6]; // 0. Gold, 1.T1 Shard, 2. T2 Shard


	//-------------------------End of Variables -----------------------------

	public static int GoldReturn()
	{
		return Items[0];
	}
	public static void GoldAdd(int amount)
	{
		Items[0] += amount;
	}
	public static void GoldRemove(int amount)
	{
		Items[0] -= amount;
	}
	public static int T1ShardAmount()
    {
		return Items[1];
    }
	public static int T1ShardNumCombo()
    {
		int count = 0;
		while ((Items[1] - (count * 50)) > 0) count++;
		return count;
    }
	public static void CombineT1(int NumOfCombo)
    {

    }
}

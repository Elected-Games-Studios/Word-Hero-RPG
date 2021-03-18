using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class InvManager
{
	private static int[] Items = new int[6]; // 0. Gold, 1.T1 Shard, 2. T2 Shard, 3. HP Item, 4. Time Stop, 5. Word Refresh
	//-------------------------End of Variables -----------------------------
    //Gold Methods
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
    //T1 Shard Methods
    public static void T1ShardAdd(int amount)
    {
        Items[1] += amount;
    }
    public static int T1ShardAmount()
    {
		return Items[1];
    }
	public static int T1ShardNumCombo()
    {
		int count = 0;
		while ((Items[1] - ((count+1) * 50)) >= 0) count++;
		return count;
    }
	public static void CombineT1(int NumOfGreen)
    {
        Random rand = new Random(DateTime.Now.Millisecond);
		for(int x = 0; x < NumOfGreen; x++)
        {
			CharectorStats.AddCharecter(1, rand);
		}
        Items[1] -= (NumOfGreen * 50);
    }
    //T2 Shard Methods
    public static void T2ShardAdd(int amount)
    {
        Items[2] += amount;
    }
    public static int T2ShardAmount()
	{
		return Items[2];
	}
	public static int T2ShardNumCombo()//validates 
	{
		int count = 0;
		while ((Items[2] - ((count+1) * 50)) >= 0) count++;
		return count;
	}
	public static void CombineT2(int NumOfPurple)
	{
        Random rand = new Random(DateTime.Now.Millisecond);
        for (int x = 0; x < NumOfPurple; x++)
		{
			CharectorStats.AddCharecter(2, rand);
		}
        Items[2] -= (NumOfPurple * 50);
    }

    //Saving
    public static string SaveManager()
    {
		string sendStr = "";
		for(int x = 0; x < Items.Count(); x++)
        {
			sendStr += Items[x].ToString();
			sendStr += ",";
        }
		return sendStr;
	}
	public static void LoadManager(string input)
    {
		if (input == " ")
		{
            Items =new int[6] { 0, 0, 0, 0, 0, 0};
        }
        else
        {
            for (int x = 0; x < 6; x++)
            {
                Items[x] = Convert.ToInt32(input.Substring(0, (input.IndexOf(','))));
                input = input.Remove(0, input.IndexOf(',') + 1);
            }
        }
	
	}


}

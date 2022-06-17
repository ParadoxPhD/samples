using System;
					
public class Program
{
	public static void Main()
	{
        int[] input = {-1,0,-3,1,1,4,2};
        Array.Sort(input);
        
        int length = input.Length;
        
        if (input[length - 1] <= 0) { print("no pos"); }
        
        for (int i = 0; i < length - 1; i++)
        {
            int x = input[i];
            int y = input[i+1];
            
            if ((y - x > 1) && !(y + x <= y))
            {
                print(input[i]+1);
            }
        }
        
        print(input[length - 1] + 1);
    }
    
    public static void print(String text)
    {
		System.Console.WriteLine(text);
        System.Environment.Exit(0);
    }
    
    public static void print(int num)
    {
        System.Console.WriteLine(num);
        System.Environment.Exit(0);
    }
}

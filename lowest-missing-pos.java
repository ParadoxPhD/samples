import java.util.Arrays;

public class MyClass {
    public static void main(String args[])
    {
        int[] input = {-1,0,-3,1,1,4,2};
        Arrays.sort(input);
        
        int length = input.length;
        
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
        System.out.println(text);
        System.exit(0);
    }
    
    public static void print(int num)
    {
        System.out.println(Integer.toString(num));
        System.exit(0);
    }
}
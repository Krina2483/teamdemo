import java.util.*;
class pro4{
	public static void main(String[] args)
	{
        abc a1 = new abc();
        abc a2 = new abc();

        a1.count();
	}
}

class abc {

	static int a=0;
	abc()
	{
       a++;
	}
	
	void count()
	{
       System.out.print(a);
	}
}
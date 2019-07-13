using System;

class Test{
    private int age;

    //Properties test
    public int Age{
        get{
            return age;
        }
        set{
            if(value >= 0)
                age = value;
        }
    }

    public void LambdaTest(){
        Func<double, int> doubleToInt = x => (int) Math.Round(x);
        Console.WriteLine(doubleToInt(2.6));
    }

    public void ForEachTest(){
        int[] values = new int[]{1, 2, 3, 4, 5};
        Array.ForEach(values, value => Math.Pow(value, 2));
        
    }


}


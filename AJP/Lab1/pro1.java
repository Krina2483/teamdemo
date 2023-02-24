import java.util.*;

class circle{
    double area(double r){
        return 3.14*r*r;
    }
}

class pro1{
    public static void main(String args[]){
        Scanner sc = new Scanner(System.in);
        System.out.println("Enter radius of circle : ");
        double r = sc.nextDouble();
        circle c = new circle();
        double area = c.area(r);
        System.out.println("Area of circle is : "+area);
    }
}
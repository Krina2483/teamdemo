import java.util.*;
class time{
    String time;
    static int hour=0;
    static int minute=0;

    time(String s){
        time=s;
    }
    void time(String s){
        hour = hour+ Integer.parseInt(s.substring(0,2));
        minute = minute+ Integer.parseInt(s.substring(3,5));
        if(minute >= 60){
            hour++;
            minute = minute - 60;
        }
    }
    void display(){
        System.out.print(hour+":"+minute);
    }  
}
class pro2{
    public static void main(String args[]){
        Scanner sc = new Scanner(System.in);

        System.out.println("Enter 1st time : ");
        String t1 = sc.nextLine();
        time time1 = new time(t1);
        time1.time(t1);

        System.out.println("Enter 2nd time : ");
        String t2 = sc.nextLine();
        time time2 = new time(t2);
        time2.time(t2);

        System.out.print("Addition of two times is : ");
        time2.display();

    }
}
import java.io.*;
import java.net.*;
import java.util.*;
public class Pro1 {
    public static void main(String[] args) throws Exception {
    
        Socket s = new Socket("192.168.2.15",1234);
        Scanner sc = new Scanner(System.in);
        DataInputStream dis = new DataInputStream(s.getInputStream());
        DataOutputStream dos = new DataOutputStream(s.getOutputStream());
        while(true)
        {
            System.out.println("Enter message : ");
            String cliStr = sc.nextLine();
            dos.writeUTF(cliStr);
            if(cliStr.equalsIgnoreCase("exit"))
            {
                System.out.println("Client Ending..!!");
                dis.close();
                dos.close();
                s.close();
                sc.close();
            }
            String serStr = dis.readUTF();
            System.out.println("Server : "+ serStr);
            if(serStr.equalsIgnoreCase("Bye"))
            {
                System.out.println("Server Disconnected");
                dos.close();
                dis.close();
                s.close();
                sc.close();
            }
        }
        
    }
}

// if kai response che ke nai?
/*
 {
    if(kai moklvu che ke nai??)
 }
 */


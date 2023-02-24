import java.io.*;
import java.net.*;
import java.util.*;
public class L2P1_Tcp_Client {
    public static void main(String[] args) throws IOException {
       
        
        Socket s = new Socket("localhost",1111);
        DataOutputStream dos = new DataOutputStream(s.getOutputStream());
        Scanner sc = new Scanner(System.in);
        String input = sc.nextLine();
        dos.writeUTF(input);
    }
}

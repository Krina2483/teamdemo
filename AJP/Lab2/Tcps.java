import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.*;
import java.util.Scanner;

public class Tcps{
    public static void main(String[] args) throws IOException {
        Scanner sc = new Scanner(System.in);
        while(true) {


            ServerSocket ss = new ServerSocket(1111);
            Socket s = ss.accept();
            DataInputStream dies = new DataInputStream(s.getInputStream());
            String str = (String) dies.readUTF();
            System.out.println("\t\t\t\t" + str);

            DataOutputStream doss = new DataOutputStream(s.getOutputStream());
            doss.writeUTF(sc.nextLine());

            ss.close();
        }
    }
}
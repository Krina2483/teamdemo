import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.util.*;

public class Tcpc {
    public static void main(String[] args) throws IOException {

        Scanner sc = new Scanner(System.in);
        while(true){


            Socket s = new Socket("192.168.2.15",1234);

            DataOutputStream RDCOMClient = new DataOutputStream(s.getOutputStream());
            RDCOMClient.writeUTF(sc.nextLine());

            DataInputStream disc = new DataInputStream(s.getInputStream());
            String str = (String)disc.readUTF();
            System.out.println("\t\t\t\t"+str);
            s.close();
        }

    }
}
// import java.net.*; //required for InetAddress Class 
// public class Tcpc{  
// 	public static void main(String[] args){  
// 	try {
//  InetAddress localhost=InetAddress.getLocalHost();
//  System.out.println("LocalHost: "+ localhost);
// 	}catch(UnknownHostException e) { 
// 	       System.out.println(e);
// 	}  
//    }  
// }

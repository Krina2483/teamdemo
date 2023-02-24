package jadblab;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

public class lab_3_1 {
	public static void main(String args[]) throws Exception {
		Class.forName("com.mysql.cj.jdbc.Driver");
		Connection conn = DriverManager.getConnection("jdbc:mysql://localhost:3306/jdbc","root","");
		Statement st = conn.createStatement();
		ResultSet rs = st.executeQuery("select * from jdbc_demo");
		while(rs.next()) {
			System.out.println(rs.getInt(1));
			System.out.println(rs.getString(2));
			System.out.println(rs.getInt(3));
			System.out.println(rs.getInt(4));
		}
		rs.close();
		st.close();
		conn.close();
	}
}

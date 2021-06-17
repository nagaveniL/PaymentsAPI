using MySqlConnector;
using System.Data;

namespace PaymentsAPI.Model
{
    public class Data
    {
        public DataTable ExecuteQuery(string strSQL)
        {          
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=Payments;Uid={0};Pwd={1};");        
            connection.Open();           
            MySqlCommand cmd = new MySqlCommand(strSQL, connection);
            DataTable ds = new DataTable();            
            MySqlDataAdapter dt = new MySqlDataAdapter(cmd);
            dt.Fill(ds);
            MySqlDataReader dbread = cmd.ExecuteReader();
            connection.Close();
            return ds;
        }
    }
}
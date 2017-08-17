using System.Data;
using System.Data.SqlClient;

namespace Allen.Core.lib
{
    public class CustomerDAO
    {
        public DataTable GetAllCustomers()
        {
            using (var conn = new SqlConnection(@"Data Source=PRIME\SQLEXPRESS;Initial Catalog=DemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();
                return ExecuteDataTable(conn, CommandType.Text, "Select * from Customer", null);
            }
        }
        public static DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            DataTable dt = new DataTable();
            // just doing this cause dr.load fails
            dt.Columns.Add("id");
            dt.Columns.Add("Name");
            using (var sqlCmd = new SqlCommand(cmdText, conn))
            {
                if (cmdParms != null)
                    sqlCmd.Parameters.AddRange(cmdParms);
                sqlCmd.CommandType = cmdType;

                SqlDataReader dr = sqlCmd.ExecuteReader();

                // as of now dr.Load throws a big nasty exception saying its not supported. wip.
                // dt.Load(dr);
                while (dr.Read())
                {
                    dt.Rows.Add(dr[0], dr[1]);
                }
            }
            return dt;
        }



        public static DataTable ExecuteDataTableSqlDA(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
            da.Fill(dt);
            return dt;
        }
    }
}

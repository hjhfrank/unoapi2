using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;  
using Newtonsoft.Json.Linq;

/*
  RunSQLc針對1條語句，應該是隱含交易功能，不建議使用
  RunSQL_TRAN明確交易處理
*/

namespace apmssql
{
    class MssqlR
    {
        private string connectionString = "Data Source=.\\sqlexpress; Initial Catalog=UNO;Persist Security Info=False;User ID=sa;Password=1234;encrypt=false;";       

        public DataTable RunSQL(string sSQL)
        {          
          SqlConnection ct = new SqlConnection(connectionString);                                    
          ct.Open();              
          SqlCommand command = new SqlCommand(sSQL,ct);            
          SqlDataReader reader = command.ExecuteReader();
          DataTable dt = new DataTable();                                
          dt.Load(reader);                           
          ct.Close();                                   
          return dt;            
        }    

        public int RunSQLc(string sSQL)
        { 
          int i=0;                   
          SqlConnection ct = new SqlConnection(connectionString);                                    
          ct.Open();              
          SqlCommand command = new SqlCommand(sSQL,ct);                 
          i=command.ExecuteNonQuery();                      
          ct.Close();                                                     
          return i;                 
        }    

        public string RunSQL_TRAN(List<string> sSQL)
		    {
          //
          string msg="ok";
          SqlConnection ct = new SqlConnection(connectionString);                        
          SqlCommand sqlWrite =ct.CreateCommand();            
          ct.Open();
          SqlTransaction trans = ct.BeginTransaction();
          sqlWrite.Transaction = trans;               
          try
          {            
            foreach (string s1 in sSQL)
            {
              sqlWrite.CommandText =s1;
              sqlWrite.ExecuteNonQuery();
            }
            trans.Commit();            
          }
          catch (Exception)
            {
            trans.Rollback();                 
            msg="ng";                    
          } 
          finally
          {
            ct.Close();
            sqlWrite.Dispose();
            ct.Dispose();
            trans.Dispose();                 
          } 
          return msg;
        }
    }
}    
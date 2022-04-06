using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITempo
{
    
    public class Conexao
    {
        NpgsqlConnection con = new NpgsqlConnection();

        //construtor
        public Conexao()
        {
            con.ConnectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=12345678";
        }

        //metodo Conectar
        public NpgsqlConnection conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)//verificando se a conexão esta fechada, se estiver, abra
            {
                con.Open();
            } 
            return con;//retornamos a conexão
        }

        public void desconcetar()
        {
            if(con.State == System.Data.ConnectionState.Open)//verificando se esta aberto, se estiver, feche
            {
                con.Close();
            }
        }

    }
}

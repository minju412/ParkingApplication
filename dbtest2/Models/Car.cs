using dbtest2.Context;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace dbtest2.Models
{
    public class Car
    {
        //[Key]
        public int Car_ID { get; set; }

        //[DisplayName("차번호")]
        //[Required(ErrorMessage = "차량 번호를 입력하세요.")]
        public string CarNum { get; set; }

        //[DisplayName("입차시각")]
        public DateTime InTime { get; set; }

        //[DisplayName("출차시각")]
        public DateTime OutTime { get; set; }

        public static List<Car> GetList(string search)
        {
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();
                string sql = "SELECT * FROM c_table ORDER BY car_id ASC"; ;

                return Dapper.SqlMapper.Query<Car>(conn, sql, new { search = search }).ToList();
            }
        }

        void CheckContents()
        {
            if (string.IsNullOrWhiteSpace(this.CarNum))
            {
                throw new Exception("차량번호가 없습니다.");
            }
            if (string.IsNullOrWhiteSpace(this.InTime.ToString())) //tostring??
            {
                throw new Exception("입차시각이 없습니다.");
            }
           
        }

        public int Insert()
        {
            CheckContents();

            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();
                string sql = "INSERT INTO c_table (car_id,carnum,intime) VALUES (C_TABLE_SEQ.NEXTVAL,:carnum,SYSDATE)";

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }








    }
}
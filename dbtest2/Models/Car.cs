using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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

        public string Owner_Name { get; set; }

        public int Owner { get; set; }

        public int Parking_Fee { get; set; }

        public string Flag { get; set; }


        public static Car Get(string carnum)
        {
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();
                string sql = "SELECT * FROM c_table WHERE carnum=:carnum AND flag='y'";

                //return conn.QuerySingle<Car>(sql, new { carnum = carnum });
                //return Dapper.SqlMapper.QuerySingle<Car>(conn, sql, new { carnum = carnum });
                return Dapper.SqlMapper.QuerySingleOrDefault<Car>(conn, sql, new { carnum = carnum });
            }
        }

        public static List<Car> GetList(string search)
        {
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();
                string sql = "SELECT * FROM c_table WHERE flag='y' ORDER BY car_id ASC"; ;

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
                string sql = "INSERT INTO c_table (car_id,carnum,intime,owner_name,flag) VALUES (C_TABLE_SEQ.NEXTVAL,:carnum,SYSDATE,:owner_name,'y')";

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }

        public int UpdateOutTime() // 출차 시각 update
        {
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();
                string sql = "UPDATE c_table SET outtime=SYSDATE WHERE carnum=:carnum";

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }

        public int UpdateFee(int fee) // 주차 요금 계산 -> 매개변수 fee로 업데이트 예정..
        {
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();
                string sql = "UPDATE c_table SET parking_fee=1000 WHERE carnum=:carnum AND flag='y'";

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }

        public int Delete() // 출차
        {
            string _strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=ann;Password=111111;";
            using (var conn = new OracleConnection(_strConn))
            {
                conn.Open();
                //string sql = "DELETE FROM c_table WHERE carnum=:carnum";
                string sql = "UPDATE c_table SET flag='n' WHERE carnum=:carnum AND flag='y'";

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }

    }
}
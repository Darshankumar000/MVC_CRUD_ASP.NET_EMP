using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace DEMO.Models
{
    public class EmployeeModel
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\2MCA\.NET\MVC\DEMO\DEMO\App_Data\Database1.mdf;Integrated Security=True");


        public int id { get; set; }
       
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Dept")]
        public string Department { get; set; }

        [Required(ErrorMessage = "please enter salary")]
        [Range(5000, 50000, ErrorMessage = "Value should be between 5k to 50k")]
        public int Salary { get; set; }



        public List<EmployeeModel> getData()
        {
            List<EmployeeModel> lstEmp = new List<EmployeeModel>();
            SqlDataAdapter apt = new SqlDataAdapter("select * from emp", con);
            DataSet ds = new DataSet();
            apt.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstEmp.Add(new EmployeeModel
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    Name = dr["Name"].ToString(),
                    Department = dr["Dept"].ToString(),
                    Salary =
               Convert.ToInt32(dr["Salary"].ToString())
                });
            }
            return lstEmp;
        }


        public EmployeeModel getData(string Id)
        {
            EmployeeModel emp = new EmployeeModel();
            SqlCommand cmd = new SqlCommand("select * from emp where id='" + Id +"'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emp.id = Convert.ToInt32(dr["id"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Department = dr["Dept"].ToString();
                    emp.Salary = Convert.ToInt32(dr["Salary"].ToString());
                }
            }
            con.Close();
            return emp;
        }



        public bool insert(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("insert into emp values(@name, @dept, @salary)", con);
           
            cmd.Parameters.AddWithValue("@name", Emp.Name);
            cmd.Parameters.AddWithValue("@dept", Emp.Department);
            cmd.Parameters.AddWithValue("@salary", Emp.Salary);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

        public bool update(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("update emp set Name=@name, Dept = @dept, Salary = @salary where id = @id", con);
           
            cmd.Parameters.AddWithValue("@name", Emp.Name);
            cmd.Parameters.AddWithValue("@dept", Emp.Department);
            cmd.Parameters.AddWithValue("@salary", Emp.Salary);
            cmd.Parameters.AddWithValue("@id", Emp.id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        //delete a record from a database table
        public bool delete(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("delete emp where id = @id", con);
            cmd.Parameters.AddWithValue("@id", Emp.id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        
    }
}

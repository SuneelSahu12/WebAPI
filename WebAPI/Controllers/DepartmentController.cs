using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"SELECT DepartmentID,DepartmentName
                            FROM DBO.Departments WITH(NOLOCK)
                            ";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var sa = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        sa.Fill(table);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Deparment deparment)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                INSERT INTO DBO.Departments VALUES ('"+ deparment.DepartmentName + @"')";

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var sa = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            sa.Fill(table);
                        }
                    }
                }
                return "Added Successfully";
            }
            catch (Exception)
            {

                return "Failed to add";
            }
        }

        public string Put(Deparment deparment)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                UPDATE DBO.Departments 
                                SET DepartmentName = '" + deparment.DepartmentName + @"'
                                WHERE DepartmentID = " + deparment.DepartmentID + @"
                                ";

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var sa = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            sa.Fill(table);
                        }
                    }
                }
                return "Updated Successfully";
            }
            catch (Exception)
            {

                return "Failed to add";
            }
        }

        public string Delete(int  ID)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"Delete from DBO.Departments WHERE DepartmentID = " + ID;

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var sa = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            sa.Fill(table);
                        }
                    }
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {

                return "Failed to Delete";
            }
        }
    }
}

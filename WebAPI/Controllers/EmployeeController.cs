using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"SELECT [EmployeeID], [EmployeeName], [Department], [MailID], 
                                CONVERT(VARCHAR(10), [DOJ], 120) AS [DOJ]
                            FROM [dbo].[Employees] WITH(NOLOCK)
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

        public string Post(Employee employee)
        {
            try
            {
                string doj = employee.DOJ.ToString().Split(' ')[0];
                DataTable table = new DataTable();
                string query = @"
                                INSERT INTO [dbo].[Employees]
                                        (EmployeeName, Department, MailID,DOJ)
                                VALUES 
                                        (
                                            '" + employee.EmployeeName + @"'
                                           ,'" + employee.Department + @"'  
                                           ,'" + employee.MailID + @"'   
                                           ,'" + doj + @"'   
                                          
                                        )";

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
            catch (Exception ex)
            {

                return "Failed to add" + ex.ToString();
            }
        }
    }
}

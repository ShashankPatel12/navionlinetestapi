using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NichiOnlineTest.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NichiOnlineTest.Core.Logic
{
    public class HomeCoreLogic
    {
        private readonly VinayTestDBContext _context;

        public HomeCoreLogic()
        {
            _context = new VinayTestDBContext();
        }

        public string GetSummaryReportByCategoryId(Guid categoryId)
        {
            try
            {
                DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "STP_DASHBOARDSUMMARY";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CATEGORYID", SqlDbType.VarChar) { Value = categoryId.ToString() });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                var reader = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(reader);
                decimal TotalMarks = 0;
                string UserMarksColumn;
                decimal userMarks = 0;

                TableData data = new TableData();
                data.Headers = new List<string>();

                foreach (DataColumn column in dt.Columns)
                {
                    data.Headers.Add(column.ColumnName);
                    if (Regex.IsMatch(column.ColumnName, @"\d"))
                    {
                        TotalMarks += Convert.ToDecimal(Regex.Match(column.ColumnName, @"\d+").Value);
                    }
                }

                UserMarksColumn = "Total Marks" + " (" + TotalMarks + ")";
                data.Data = new List<dynamic>();
                if (dt.Columns.Count > 0)
                {
                    data.Headers.Add(UserMarksColumn);
                    dt.Columns.Add(UserMarksColumn);
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    userMarks = 0;
                    dynamic dyn = new ExpandoObject();
                    data.Data.Add(dyn);
                    foreach (DataColumn column in dt.Columns)
                    {
                        var dic = (IDictionary<string, object>)dyn;
                        dic[column.ColumnName] = dataRow[column];
                        if (column.Caption != "Mobile Number" && column.Caption != "Candidate Name" && column.Caption != UserMarksColumn)
                        {
                            userMarks += dataRow[column] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow[column]);
                        }
                        if (column.Caption == UserMarksColumn)
                        {
                            dic[column.ColumnName] = userMarks;
                        }
                    }
                }

                data.Data = data.Data
                    .OrderByDescending(x => ((IDictionary<string, object>) x)[data.Headers[data.Headers.Count - 1]])
                    .ToList();

                string json = JsonConvert.SerializeObject(data, new KeyValuePairConverter());

                return json;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }

    public class TableData
    {
        public List<string> Headers { get; set; }
        public List<dynamic> Data { get; set; }

    }

}

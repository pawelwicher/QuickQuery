using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace QuickQuery
{
    public class QuickQuery
    {
        public string ConnectionString { get; set; }

        public List<T> Query<T>(string cmdText, T type)
        {
            var list = new List<T>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(cmdText, connection) { CommandType = CommandType.Text })
                {
                    var reader = command.ExecuteReader();
                    var t = type.GetType();
                    var constructorInfo_array = t.GetConstructors();
                    var parameterInfo_array = constructorInfo_array[0].GetParameters();
                    var propertyInfo_array = t.GetProperties();
                    var parameters = new object[propertyInfo_array.Length];
                    var schemaTable = reader.GetSchemaTable();
                    var columns = new List<string>();
                    var t_object = default(T);

                    foreach (DataRow row in schemaTable.Rows)
                    {
                        columns.Add(row["ColumnName"].ToString());
                    }

                    while (reader.Read())
                    {
                        for (int i = 0; i < propertyInfo_array.Length; i++)
                        {
                            if (columns.Contains(propertyInfo_array[i].Name))
                            {
                                parameters[i] = reader[propertyInfo_array[i].Name];
                            }
                        }
                        t_object = (T)(constructorInfo_array[0].Invoke(parameters));
                        list.Add(t_object);
                    }
                }
                connection.Close();
                return list;
            }
        }
    }
}
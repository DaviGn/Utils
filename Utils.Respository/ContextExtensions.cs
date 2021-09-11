using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Utils.Repository
{
    public static class ContextExtensions
    {
        public static T Find<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate) where T : class
        {
            var local = dbSet.Local.FirstOrDefault(predicate.Compile());

            return local != null ? local : dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public static async Task<T> FindAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate) where T : class
        {
            var local = dbSet.Local.FirstOrDefault(predicate.Compile());

            return local != null ? local : await dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public static async Task<IList<T>> ExecuteCommandSingletonRsult<T>(this DbContext context, string command,
                                                                           IEnumerable<SqlParameter> parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = commandType;

                // set some parameters of the stored procedure
                if (parameters is not null)
                {
                    foreach (var parameter in parameters)
                    {
                        parameter.Value = parameter.Value ?? DBNull.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = await cmd.ExecuteReaderAsync())
                {
                    List<T> result = new List<T>();

                    while (dataReader.Read())
                    {
                        var value = (T)Convert.ChangeType(dataReader.GetValue(0), typeof(T));
                        result.Add(value);
                    }

                    return result;
                }
            }
        }

        public static async Task<IList<T>> ExecuteCommand<T>(this DbContext context, string command,
                                                             IEnumerable<SqlParameter> parameters = null, CommandType commandType = CommandType.StoredProcedure)
            where T : class, new()
        {
            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = commandType;

                // set some parameters of the stored procedure
                if (parameters is not null)
                {
                    foreach (var parameter in parameters)
                    {
                        parameter.Value = parameter.Value ?? DBNull.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = await cmd.ExecuteReaderAsync())
                {
                    var test = DataReaderMapToList<T>(dataReader);
                    return test;
                }
            }
        }

        private static IList<T> DataReaderMapToList<T>(DbDataReader dr) where T : class, new()
        {
            List<T> list = new List<T>();

            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();

                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string column = dr.GetName(i);

                    PropertyInfo propertyInfo = obj.GetType().GetProperty(column);

                    if (propertyInfo is null || dr.GetValue(i) is DBNull)
                        continue;

                    if (propertyInfo.PropertyType.IsEnum)
                        propertyInfo.SetValue(obj, Enum.ToObject(propertyInfo.PropertyType, dr.GetValue(i)), null);
                    else
                        propertyInfo.SetValue(obj, Convert.ChangeType(dr.GetValue(i), propertyInfo.PropertyType), null);
                }

                list.Add(obj);
            }

            return list;
        }
    }
}
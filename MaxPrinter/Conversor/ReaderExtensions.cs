using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apolíneo
{
    public static class ReaderExtensions
    {
        public static T GetSafeValue<T>(this DbDataReader reader, String fieldName)
        {

            int ordinal = reader.GetOrdinal(fieldName);

            if (reader.HasRows == false)
                return default(T);

            Boolean isNull = false;
            try
            {
                isNull = reader.IsDBNull(ordinal);
            }
            catch (Exception) { }

            if (isNull)
                return default(T);

            if (typeof(T) == typeof(String))
            {
                return (T)Convert.ChangeType(reader.GetString(ordinal), typeof(String));
            }
            else if (typeof(T) == typeof(DateTime?) || typeof(T) == typeof(DateTime))
            {
                return (T)Convert.ChangeType(reader.GetDateTime(ordinal), typeof(DateTime));
            }
            else if (typeof(T) == typeof(Decimal?) || typeof(T) == typeof(Decimal))
            {
                return (T)Convert.ChangeType(reader.GetDecimal(ordinal), typeof(Decimal));
            }
            else if (typeof(T) == typeof(Int32?) || typeof(T) == typeof(Int32))
            {
                return (T)Convert.ChangeType(reader.GetDecimal(ordinal), typeof(Int32));
            }
            else if (typeof(T) == typeof(Int64?) || typeof(T) == typeof(Int64))
            {
                return (T)Convert.ChangeType(reader.GetDecimal(ordinal), typeof(Int64));
            }
            else if (typeof(T) == typeof(Double?) || typeof(T) == typeof(Double))
            {
                return (T)Convert.ChangeType(reader.GetDecimal(ordinal), typeof(Double));
            }
            else if (typeof(T) == typeof(Object))
            {
                return (T)reader.GetValue(ordinal);
            }


            throw new NotImplementedException();
        }

        public static Object GetSafeValue(this DbDataReader reader, String fieldName, Type dataType)
        {
            int ordinal = reader.GetOrdinal(fieldName);
            if (reader.HasRows == false)
                return GetDefault(dataType);

            Boolean isNull = false;
            try
            {
                isNull = reader.IsDBNull(ordinal);
            }
            catch (Exception) { }



            if (isNull)
                return GetDefault(dataType);

            if (dataType == typeof(String))
            {
                return Convert.ChangeType(reader.GetString(ordinal), typeof(String));
            }
            else if (dataType == typeof(DateTime?) || dataType == typeof(DateTime))
            {
                return Convert.ChangeType(reader.GetDateTime(ordinal), typeof(DateTime));
            }
            else if (dataType == typeof(Decimal?) || dataType == typeof(Decimal))
            {
                return Convert.ChangeType(reader.GetDecimal(ordinal), typeof(Decimal));
            }
            else if (dataType == typeof(Int32?) || dataType == typeof(Int32))
            {
                Object retorno = null;
                try
                {
                    retorno = Convert.ChangeType(reader.GetDecimal(ordinal), typeof(Int32));
                    return retorno;
                }
                catch
                {

                }


                try
                {
                    retorno = Convert.ChangeType(reader.GetInt32(ordinal), typeof(Int32));
                    return retorno;
                }
                catch
                {

                }
            }
            else if (dataType == typeof(Int64?) || dataType == typeof(Int64))
            {
                return Convert.ChangeType(reader.GetDecimal(ordinal), typeof(Int64));
            }
            else if (dataType == typeof(Object))
            {
                return reader.GetValue(ordinal);
            }

            throw new NotImplementedException();
        }

        private static object GetDefault(Type t)
        {
            return t.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(t, null);
        }
    }
}


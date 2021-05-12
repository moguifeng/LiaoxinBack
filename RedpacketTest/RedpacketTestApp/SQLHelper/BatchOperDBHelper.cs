using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RedpacketTestApp.SQLHelper
{
    /// <summary>
    /// 用于批量操作数据库
    /// </summary>
    public static class BatchOperDBHelper
    {
        /// <summary> 
        /// 大批量插入数据(2000每批次) 
        /// 已采用整体事物控制 
        /// </summary> 
        /// <param name="connString">数据库链接字符串</param> 
        /// <param name="dt">含有和目标数据库表结构完全一致(所包含的字段名完全一致即可)的DataTable(需要赋TableName)</param> 
        /// <param name="timeOutSecond">超时秒数</param> 
        /// <param name="batchSize">每次执行更新数量</param> 
        public static bool BulkCopy(string connString, DataTable dt, int timeOutSecond = 3600, int batchSize = 2000)
        {
            bool result = true;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.BatchSize = batchSize;
                        bulkCopy.BulkCopyTimeout = timeOutSecond;
                        bulkCopy.DestinationTableName = dt.TableName;
                        try
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            bulkCopy.WriteToServer(dt);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            result = false;
                            transaction.Rollback();
                            throw ex;
                         
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary> 
        /// 批量更新数据(每批次5000) 
        /// </summary> 
        /// <param name="connString">数据库链接字符串</param> 
        /// <param name="table">含有和目标数据库表结构完全一致(所包含的字段名完全一致即可)(需要赋TableName)</param> 
        /// <param name="timeOutSecond">超时秒数</param> 
        /// <param name="batchSize">每次执行更新数量</param> 
        public static bool Update(string connString, DataTable table, int timeOutSecond = 3600, int batchSize = 2000)
        {
            bool result = true;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand comm = conn.CreateCommand();
            comm.CommandTimeout = timeOutSecond;
            comm.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            SqlCommandBuilder commandBulider = new SqlCommandBuilder(adapter);
            commandBulider.ConflictOption = ConflictOption.OverwriteChanges;
            try
            {
                conn.Open();
                //设置批量更新的每次处理条数 
                adapter.UpdateBatchSize = batchSize;
                adapter.SelectCommand.Transaction = conn.BeginTransaction();/////////////////开始事务 
                if (table.ExtendedProperties["SQL"] != null)
                {
                    adapter.SelectCommand.CommandText = table.ExtendedProperties["SQL"].ToString();
                }
                adapter.Update(table);
                adapter.SelectCommand.Transaction.Commit();/////提交事务 
            }
            catch (Exception ex)
            {
                result = false;
                if (adapter.SelectCommand != null && adapter.SelectCommand.Transaction != null)
                {
                    adapter.SelectCommand.Transaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        } 
    }
}

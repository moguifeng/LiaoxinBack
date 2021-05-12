using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RedpacketTestApp.SQLHelper
{
    public interface ISQLHelper
    {

        #region 实例方法

        #region ExecuteNonQuery

        /// <summary>
        /// 执行SQL语句,返回影响的行数
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(string commandText, params DbParameter[] parms);

        /// <summary>
        /// 执行SQL语句,返回影响的行数
        /// </summary>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] parms);

        int ExecuteSqlTran(string sql);
        int ExecuteSqlTran(List<String> sqlStringList);
        #endregion ExecuteNonQuery

        #region ExecuteScalar

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        T ExecuteScalar<T>(string commandText, params DbParameter[] parms);

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        object ExecuteScalar(string commandText, params DbParameter[] parms);

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] parms);

        #endregion ExecuteScalar


        #region ExecuteDataRow

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行</returns>
         DataRow ExecuteDataRow(string commandText, params DbParameter[] parms);

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行
        /// </summary>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行</returns>
        DataRow ExecuteDataRow(CommandType commandType, string commandText, params DbParameter[] parms);

        #endregion ExecuteDataRow

        #region ExecuteDataTable

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一个数据表
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一个数据表</returns>
        DataTable ExecuteDataTable(string commandText, params DbParameter[] parms);

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一个数据表
        /// </summary>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一个数据表</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, params DbParameter[] parms);

        ///// <summary>
        /////  执行SQL语句,返回结果集中的第一个数据表
        ///// </summary>
        ///// <param name="sql">SQL语句</param>
        ///// <param name="order">排序SQL,如"ORDER BY ID DESC"</param>
        ///// <param name="pageSize">每页记录数</param>
        ///// <param name="pageIndex">页索引</param>
        ///// <param name="parms">查询参数</param>
        ///// <param name="query">查询SQL</param>        
        ///// <returns></returns>
        //DataTable ExecutePageDataTable(string sql, string order, int pageSize, int pageIndex, DbParameter[] parms = null, string query = null, string cte = null);
        #endregion ExecuteDataTable

        #region ExecuteDataSet

        /// <summary>
        /// 执行SQL语句,返回结果集
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集</returns>
        DataSet ExecuteDataSet(string commandText, params DbParameter[] parms);

        /// <summary>
        /// 执行SQL语句,返回结果集
        /// </summary>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集</returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText, params DbParameter[] parms);

        #endregion ExecuteDataSet

        #region 批量操作

        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="table">数据表</param>
        int BulkInsert(DataTable table, string beforeSql="");

        int BatchUpdate(DataTable table);

        ///// <summary>
        ///// 使用MySqlDataAdapter批量更新数据
        ///// </summary>
        ///// <param name="table">数据表</param>
        //int BatchUpdate(DataTable table);

        #endregion 批量操作

        #endregion 实例方法
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedpacketTestApp.SQLHelper
{
    /// <summary>
    /// Êý¾Ý¿âÌõ¼þ
    /// </summary>
    public class SqlCondition
    {
        /// <summary>
        /// ÁÐÃû
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// sqlÔËËã·û
        /// </summary>
        public string SqlComparison { get; set; }
        /// <summary>
        /// ±È½ÏµÄÊý¾Ý
        /// </summary>
        public string Data { get; set; }
    }

    /// <summary>
    /// sqlÔËËã·ûÃ¶¾Ù
    /// </summary>
    public enum EnumSqlComparison
    {
        /// <summary>
        /// µÈÓÚ
        /// </summary>
        Equal,
        /// <summary>
        /// ´óÓÚ
        /// </summary>
        Greater,
        /// <summary>
        /// Ð¡ÓÚ
        /// </summary>
        Less,
        /// <summary>
        /// ´óÓÚµÈÓÚ
        /// </summary>
        GreaterEqual,
        /// <summary>
        /// Ð¡ÓÚµÈÓÚ
        /// </summary>
        LessEqual,
        /// <summary>
        /// ²»µÈÓÚ
        /// </summary>
        NotEqual,
        /// <summary>
        /// like
        /// </summary>
        Like,
        /// <summary>
        /// in
        /// </summary>
        In,
        /// <summary>
        /// notIn
        /// </summary>
        NotIn,
        /// <summary>
        /// is
        /// </summary>
        Is,
        /// <summary>
        /// isnot
        /// </summary>
        IsNot

    }
}

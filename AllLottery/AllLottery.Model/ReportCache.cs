using System;
using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    public class ReportCache : BaseModel
    {
        public ReportCache()
        {
        }

        public ReportCache(int playerId, DateTime queryTime, decimal value, string type)
        {
            PlayerId = playerId;
            QueryTime = queryTime;
            Value = value;
            Type = type;
        }

        public int ReportCacheId { get; set; }

        [ZzbIndex]
        public decimal Value { get; set; }

        [ZzbIndex("ReportCache", 1, IsUnique = true)]
        public DateTime QueryTime { get; set; }

        [ZzbIndex("ReportCache", 2, IsUnique = true)]
        public int PlayerId { get; set; }

        [MaxLength(200)]
        [ZzbIndex("ReportCache", 3, IsUnique = true)]
        public string Type { get; set; }
    }
}
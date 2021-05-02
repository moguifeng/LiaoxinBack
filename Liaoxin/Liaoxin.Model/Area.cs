using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 地区表
    /// </summary>
    public class Area : BaseModel
    {
        public Area()
        {
        }
        public Guid AreaId { get; set; } = Guid.NewGuid();
 
        [ZzbIndex(IsUnique =true)]
        public string Code { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string LongCode { get; set; }

        public string ParentCode { get; set; }


        [ZzbIndex("areaLevel")]
        public int Level { get; set; }
        

    
    }

 
}
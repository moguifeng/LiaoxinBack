using System;

namespace Zzb.EF
{
    public class BaseModel
    {



        [ZzbIndex]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [ZzbIndex]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        [ZzbIndex]
        public bool IsEnable { get; set; } = true;

        public void Update()
        {
            UpdateTime=DateTime.Now;;
        }
    }
}
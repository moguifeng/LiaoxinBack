using System.ComponentModel;
using Zzb.EF;

namespace AllLottery.Model
{
    public class Platform : BaseModel
    {
        public Platform()
        {
        }

        public Platform(string name, string value, string description, PlatformEnum type, int sortIndex, Affix affix)
        {
            Name = name;
            Value = value;
            Description = description;
            Type = type;
            SortIndex = sortIndex;
            Affix = affix;
        }

        public int PlatformId { get; set; }

        public string Name { get; set; }

        [ZzbIndex(IsUnique = true)]
        public string Value { get; set; }

        public string Description { get; set; }

        public PlatformEnum Type { get; set; }

        public int SortIndex { get; set; }

        public int AffixId { get; set; }

        public virtual Affix Affix { get; set; }
    }

    public enum PlatformEnum
    {
        [Description("真人视讯")]
        LiveVideo,
        [Description("体育竞技")]
        SportsCompetition,
        [Description("棋牌大厅")]
        Chess,
        [Description("电子游戏")]
        ElectronicGames
    }
}
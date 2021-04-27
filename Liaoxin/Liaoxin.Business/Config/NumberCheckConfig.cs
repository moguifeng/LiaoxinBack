namespace Liaoxin.Business.Config
{
    public abstract class NumberCheckConfig : BaseConfig
    {
        protected virtual decimal Min => decimal.MinValue;

        protected virtual decimal Max => decimal.MaxValue;

        protected override bool CheckValue(string value)
        {
            if (!decimal.TryParse(value, out var number))
            {
                return false;
            }
            if (number < Min || number > Max)
            {
                return false;
            }
            return true;
        }
    }
}
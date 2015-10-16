
namespace Noise.Modules
{
    public class Const : Module
    {
        public Const() : this(DefaultValue)
        {
            
        }

        public Const(double value)
        {
            Value = value;
        }

        public override double GetValue(double x, double y, double z)
        {
            return Value;
        }

        public override int ModuleCount
        {
            get { return 0; }
        }

        public double Value { get; set; }

        readonly static double DefaultValue = 0.0;
    }
}

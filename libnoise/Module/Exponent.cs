using System;
using System.Diagnostics;

namespace Noise.Modules
{
    public class Exponent : Module
    {

		#region ~ Constructors ~ 

        public Exponent(double exponent)
        {
            Value = exponent;
        }

        public Exponent() : this(DefaultExponent) { }

		#endregion ~ Constructors ~ 

		#region ~ Methods ~ 


		// Public Methods 

        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            double value = _modules[0].GetValue(x, y, z);
            return Math.Pow(Math.Abs((value + 1.0) / 2.0),Value) * 2.0 - 1.0;
        }


		#endregion ~ Methods ~ 

		#region ~ Properties ~ 

        public double Value { get; set; }

        public override int ModuleCount
        {
            get { return 1; }
        }

		#endregion ~ Properties ~ 

		#region ~ Fields ~ 

        static readonly double DefaultExponent = 1.0;

		#endregion ~ Fields ~ 

    }
}

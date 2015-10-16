using System.Diagnostics;

namespace Noise.Modules
{
    public class Displace : Module
    {

		#region ~ Methods ~ 


		// Public Methods 

        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);
            Debug.Assert(_modules[1] != null);
            Debug.Assert(_modules[2] != null);
            Debug.Assert(_modules[3] != null);

            double xDisplace = x + (_modules[1].GetValue(x, y, z));
            double yDisplace = y + (_modules[2].GetValue(x, y, z));
            double zDisplace = z + (_modules[3].GetValue(x, y, z));

            return _modules[0].GetValue(xDisplace, yDisplace, zDisplace);
        }


		#endregion ~ Methods ~ 

		#region ~ Properties ~ 

        public override int ModuleCount
        {
            get { return 4; }
        }

		#endregion ~ Properties ~ 

    }
}

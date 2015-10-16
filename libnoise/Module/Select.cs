using System.Diagnostics;

namespace Noise.Modules
{
    public class Select : Module
    {
        #region ~ Construction ~
        
        public Select() : this(DefaultEdgeFalloff, DefaultUpperBound, DefaultLowerBound) { }

        public Select(double edgeFalloff, double upperBound, double lowerBound)
        {
            EdgeFalloff = edgeFalloff;
            UpperBound = upperBound;
            LowerBound = lowerBound;
        }           

        #endregion

        #region ~ Methods ~


        // Public Methods 

        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);
            Debug.Assert(_modules[1] != null);
            Debug.Assert(_modules[2] != null);

            double controlValue = _modules[2].GetValue(x, y, z);
            double alpha = 0.0;

            if (EdgeFalloff > 0.0)
            {
                if (controlValue < (LowerBound - EdgeFalloff))
                {
                    return _modules[0].GetValue(x, y, z);
                }
                else if (controlValue < (LowerBound + EdgeFalloff))
                {
                    double lowerCurve = (LowerBound - EdgeFalloff);
                    double upperCurve = (LowerBound + EdgeFalloff);

                    alpha = Interpolation.SCurve3((controlValue - lowerCurve) / (upperCurve - lowerCurve));

                    return Interpolation.LinearInterpolate(
                        _modules[0].GetValue(x, y, z),
                        _modules[1].GetValue(x, y, z),
                        alpha);
                }
                else if (controlValue < (UpperBound - EdgeFalloff))
                {
                    return _modules[1].GetValue(x, y, z);
                }
                else if (controlValue < (UpperBound + EdgeFalloff))
                {
                    double lowerCurve = (UpperBound - EdgeFalloff);
                    double upperCurve = (UpperBound + EdgeFalloff);

                    alpha = Interpolation.SCurve3((controlValue - lowerCurve) / (upperCurve - lowerCurve));

                    return Interpolation.LinearInterpolate(_modules[1].GetValue(x, y, z), _modules[0].GetValue(x, y, z), alpha);
                }
                else
                {
                    return _modules[0].GetValue(x, y, z);
                }
            }
            else
            {
                if (controlValue < LowerBound || controlValue > UpperBound)
                {
                    return _modules[0].GetValue(x, y, z);
                }
                else
                {
                    return _modules[1].GetValue(x, y, z);
                }
            }
        }


		#endregion ~ Methods ~ 

		#region ~ Properties ~ 

        public double EdgeFalloff { get; set; }

        public double LowerBound { get; set; }

        public override int ModuleCount
        {
            get { return 3; }
        }

        public double UpperBound { get; set; }

		#endregion ~ Properties ~ 

		#region ~ Fields ~ 

        static readonly double DefaultEdgeFalloff = 0.0;
        static readonly double DefaultLowerBound = -1.0;
        static readonly double DefaultUpperBound = 1.0;

		#endregion ~ Fields ~ 

    }
}

using System.Diagnostics;

namespace Noise.Modules
{
    public class ScalePoint : Module
    {
        #region ~ Construction ~

        public ScalePoint() : this(DefaultScaleX, DefaultScaleY, DefaultScaleZ) { }

        public ScalePoint(double x, double y, double z)
        {
            SetScale(x, y, z);
        }

        #endregion

        #region ~ Methods ~

        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            return _modules[0].GetValue(x * ScaleX, y * ScaleY, z * ScaleZ);
        }

        public void SetScale(double x, double y, double z)
        {
            ScaleX = x;
            ScaleY = y;
            ScaleZ = z;
        }

        #endregion
 
        #region ~ Properties ~
        
        public override int ModuleCount
        {
            get { return 1; }
        }       

        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double ScaleZ { get; set; }

        #endregion

        #region ~ Defaults ~

        static readonly double DefaultScaleX = 1.0;
        static readonly double DefaultScaleY = 1.0;
        static readonly double DefaultScaleZ = 1.0;

        #endregion
    }
}

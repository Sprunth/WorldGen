using System.Diagnostics;

namespace Noise.Modules
{
    public class TranslatePoint : Module
    {
        #region ~ Construction ~

        public TranslatePoint() : this(DefaultTranslateX, DefaultTranslateY, DefaultTranslateZ) { }

        public TranslatePoint(double x, double y, double z)
        {
            SetTranslation(x, y, z);
        }

        #endregion

        #region ~ Methods ~
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            return _modules[0].GetValue(x + XTranslation, y + YTranslation, z + ZTranslation);
        }

        public void SetTranslation(double x, double y, double z)
        {
            XTranslation = x;
            YTranslation = y;
            ZTranslation = z;
        }

        #endregion

        #region ~ Properties ~
        public override int ModuleCount
        {
            get { return 1; }
        }

        public double XTranslation { get; set; }
        public double YTranslation { get; set; }
        public double ZTranslation { get; set; }
            

        #endregion

        #region ~ Defaults ~

        static readonly double DefaultTranslateX = 0.0;
        static readonly double DefaultTranslateY = 0.0;
        static readonly double DefaultTranslateZ = 0.0;

        #endregion
    }
}

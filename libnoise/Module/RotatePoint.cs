using System;
using System.Diagnostics;

namespace Noise.Modules
{
    public class RotatePoint : Module
    {
        #region ~ Construction ~

        public RotatePoint() : this(DefaultXRotation, DefaultYRotation, DefaultZRotation) { }

        public RotatePoint(double x, double y, double z)
        {
            SetRotation(x, y, z);
        }

        #endregion

        #region ~ Methods ~
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            double nx = (_x1Matrix * x) + (_y1Matrix * y) + (_z1Matrix * z);
            double ny = (_x2Matrix * x) + (_y2Matrix * y) + (_z2Matrix * z);
            double nz = (_x3Matrix * x) + (_y3Matrix * y) + (_z3Matrix * z);

            return _modules[0].GetValue(nx, ny, nz);
        }

        public void SetRotation(double x, double y, double z)
        {
            double xCos, yCos, zCos, xSin, ySin, zSin;
            xCos = Math.Cos(x * MathsUtils.DEG_TO_RAD);
            yCos = Math.Cos(y * MathsUtils.DEG_TO_RAD);
            zCos = Math.Cos(z * MathsUtils.DEG_TO_RAD);
            xSin = Math.Sin(x * MathsUtils.DEG_TO_RAD);
            ySin = Math.Sin(y * MathsUtils.DEG_TO_RAD);
            zSin = Math.Sin(z * MathsUtils.DEG_TO_RAD);

            _x1Matrix = ySin * xSin * zSin + yCos * zCos;
            _y1Matrix = xCos * zSin;
            _z1Matrix = ySin * zCos - yCos * xSin * zSin;
            _x2Matrix = ySin * xSin * zCos - yCos * zSin;
            _y2Matrix = xCos * zCos;
            _z2Matrix = -yCos * xSin * zCos - ySin * zSin;
            _x3Matrix = -ySin * xCos;
            _y3Matrix = xSin;
            _z3Matrix = yCos * xCos;

            _xRotation = x;
            _yRotation = y;
            _zRotation = z;
        }

        #endregion

        #region ~ Properties ~

        public override int ModuleCount
        {
            get { return 1; }
        }       

        #endregion

        #region ~ Fields ~

        double _xRotation;
        double _yRotation;
        double _zRotation;

        double _x1Matrix;
        double _y1Matrix;
        double _z1Matrix;

        double _x2Matrix;
        double _y2Matrix;
        double _z2Matrix;

        double _x3Matrix;
        double _y3Matrix;
        double _z3Matrix;


        #endregion

        #region ~ Defaults ~

        readonly static double DefaultXRotation = 0.0;
        readonly static double DefaultYRotation = 0.0;
        readonly static double DefaultZRotation = 0.0;

        #endregion
    }
}

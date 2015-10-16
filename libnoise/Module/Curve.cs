using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Noise.Modules
{
    public class Curve : Module
    {
		#region ~ Methods ~ 


		// Public Methods 

        public void AddControlPoint(double input, double output)
        {
            int position = FindInsertPosition(input);
            _points.Insert(position, new ControlPoint(input, output));
        }

        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);
            Debug.Assert(_points.Count > 3);

            double sourceValue = _modules[0].GetValue(x, y, z);

            int index;
            for (index = 0; index < _points.Count; ++index)
            {
                if (sourceValue < _points[index].InputValue) break;
            }

            int index0 = MathsUtils.Clamp(index - 2, 0, _points.Count - 1);
            int index1 = MathsUtils.Clamp(index - 1, 0, _points.Count - 1);
            int index2 = MathsUtils.Clamp(index - 0, 0, _points.Count - 1);
            int index3 = MathsUtils.Clamp(index + 1, 0, _points.Count - 1);

            if (index1 == index2)
            {
                return _points[index1].OutputValue;
            }

            double input1 = _points[index1].InputValue;
            double input2 = _points[index2].InputValue;
            double alpha = (sourceValue - input1) / (input1 - input2);

            return Interpolation.CubicInterpolate(
                _points[index0].OutputValue,
                _points[index1].OutputValue,
                _points[index2].OutputValue,
                _points[index3].OutputValue,
                alpha);
        }



		// Private Methods 

        private int FindInsertPosition(double input)
        {
            int position;
            for (position = 0; position < _points.Count; position++)
            {
                if (input < _points[position].InputValue)
                {
                    break;
                }
                else if (input == _points[position].InputValue)
                {
                    throw new ArgumentException("Input value already defined");
                }
            }

            return position;
        }


		#endregion ~ Methods ~ 

		#region ~ Properties ~ 

        public override int ModuleCount
        {
            get { return 1; }
        }

		#endregion ~ Properties ~ 

		#region ~ Fields ~ 

        List<ControlPoint> _points = new List<ControlPoint>();

		#endregion ~ Fields ~ 

		#region ~ Nested Classes ~ 


        class ControlPoint
        {

		#region ~ Constructors ~ 

            public ControlPoint(double input,double output)
            {
                InputValue = input;
                OutputValue = output;
            }

		#endregion ~ Constructors ~ 

		#region ~ Properties ~ 

            public double InputValue { get; set; }

            public double OutputValue { get; set; }

		#endregion ~ Properties ~ 

        }
		#endregion ~ Nested Classes ~ 

    }
}


namespace Noise.Modules
{
    public abstract class Module
    {
        /// <summary>
        /// Gets the output value for this module using the given parameters.
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        /// <param name="z">The z-coordinate</param>
        /// <returns>The noise value at the given point</returns>
        public abstract double GetValue( double x , double y , double z );

        /// <summary>
        /// Sets the source module at a particular index
        /// </summary>
        /// <param name="index">The index at which to add the source module</param>
        /// <param name="module">The source module to add</param>
        public void SetSourceModule(int index, Module module)
        {
            _modules[index] = module;
        }

        /// <summary>
        /// The number of source modules required for this module to operate
        /// </summary>
        public abstract int ModuleCount { get; }

        public Module[] Modules
        {
            get
            {
                return _modules;
            }
        }


        /// <summary>
        /// Constructor which sets up the module array
        /// </summary>
        protected Module()
        {
            if (ModuleCount > 0)
            {
                _modules = new Module[ModuleCount];
            }
        }

        protected Module[] _modules;
    }
}

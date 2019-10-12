using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using IBI_toolkit.Properties;
using Rhino.Geometry;

namespace IBI_toolkit
{
    public class IBItoolkitComponent2 : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the IBItoolkitComponent2 class.
        /// </summary>
        public IBItoolkitComponent2()
          : base("Component1", "IBIquitous",
              "A set of design Tools for IBI",
              "IBItoolkit", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                return Properties.Resources.IBI_ICON01;
                //return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ee41724f-2ea8-49d5-8ff5-34444fd22e16"); }
        }
    }
}
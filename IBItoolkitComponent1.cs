using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace IBI_toolkit
{
    public class IBItoolkitComponent1 : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public IBItoolkitComponent1()
          : base("Component01", "IBIquitous",
              "A dummy component that calculats the average of two numbers",
              "IBItoolkit", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("number01", "number01", "number01", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("number02", "number02", "number02", GH_ParamAccess.item, 1.0); 
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Average Value", "Average", "The Average value of two numbers", GH_ParamAccess.item);
            pManager.AddTextParameter("info", "info", "Info about your compon", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double no1 = double.NaN;
            double no2 = double.NaN;

            bool success1 = DA.GetData(0, ref no1);
            bool success2 = DA.GetData(1, ref no2);

            if (success1 && success2)
            {
                double average = 0.5*(no1+ no2);
                DA.SetData(0, average);
            }
            else
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Check again your inputs");
            }
 

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IBI_ICON01.png;
                return Properties.Resources.IBI_ICON01;
            }
        }


        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("90ee1703-24c1-4695-9d0a-cd2074eb7aab"); }
        }
    }
}

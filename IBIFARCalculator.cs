using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace IBItoolkit
{
    public class IBIFARCalculator : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the IBItoolkitComponent5 class.
        /// </summary>
        public IBIFARCalculator()
          : base("FAR Calculator", "FAR",
              "A component that calculates the FAR of building",
              "IBItoolkit", "Building")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("site outline", "s", "a curve that representes the outline of the site", GH_ParamAccess.list);
            pManager.AddCurveParameter("building outline", "b", "a curve that representes the footprint of the building", GH_ParamAccess.list);
            pManager.AddNumberParameter("floor to floor height", "f2f", "the height beween two floor", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
            pManager.AddNumberParameter("Floor Area Ratio", "FAR", "ratio of a building's total floor area to the size of the piece of land upon which it is built ", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Curve siteOutline = null ;
            Curve bldgOutline = null;
            // a parameter for calculating the coverage
            double siteCove = 1;

            //a parameter for the whole 
            double grossArea = 1;
            // a parameter for the FAR
            double FAR = 1; 
            //read in the values from sliders/ other components
            bool success1 = DA.GetData(0, ref siteOutline);
            bool success2 = DA.GetData(0, ref bldgOutline);

           
            if (success1 && success2)
            {
                //calculate the FAR
                if(siteOutline.IsClosed)
                {
                    var siteSrf = Brep.CreatePlanarBreps(siteOutline);
                    Brep mySite = siteSrf[0];

                    double siteArea = mySite.GetArea();
                    FAR = grossArea / siteArea;
                }
                else
                {

                }

                
            }
            else if (success1 == true && success2 == false)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "you have to provide a closed curve for as building outline");
            }
            else if (success1 == true && success2 == false)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "you have to provide a closed curve for as building outline");
            }
               


        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return IBI_toolkit.Properties.Resources.IBI_ICON07;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("33e83c27-9edd-4cec-9043-fc0e6621ec55"); }
        }
    }
}
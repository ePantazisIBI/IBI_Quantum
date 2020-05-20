using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace IBI_toolkit
{
    public class IBIConvexHull : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ConvexHull class.
        /// </summary>
        public IBIConvexHull()
          : base("ConvexHull", "ConvHull",
              "A component that draws a polyline on a plane around a random collection of points",
              "IBItoolkit", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("input Points", "Pts", "A collection of points that you want to draw a polyline around", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Convex hull polyline", "Pl", "Some description of the output", GH_ParamAccess.item);
            pManager.AddTextParameter("putput information", "info", "Some description of the output", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {


            Rhino.Geometry.Point3d pt = Rhino.Geometry.Point3d.Unset;

            //create a variable to store the points
            List<Point3d> inputPts = new List<Point3d>();

            List<Point3d> inputPoints = new List<Point3d>();

            if ((!DA.GetDataList(0, inputPts)))
                return;

            
            



            if (inputPts.Count < 3)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "You need at least 3 points");
            }
            else if( AreThereErrors(inputPoints) ==true)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "ERROR - There are some duplicates");
            }
            else
            {
               
            }

            for (int i = 0; i < inputPts.Count - 1; i++)
            {
                inputPoints.Add(inputPts[i]);
            }

            //if you do not have any errors calculate the polyline
            Polyline resCH = ConvexHull.CalculateCH(inputPoints);

            //create an output messge to inform the user
            String outMessage = "Congrats you have  drawn a polyline around your points " + inputPoints.Count;

            DA.SetData(0, resCH);
            DA.SetData(1, outMessage);


        }

        private bool AreThereErrors(List<Point3d> inputPoints)
        {
            if (AreThereDuplicates(inputPoints))
            {
               
                return true;
            }

            return false;
        }


        private bool AreThereDuplicates(List<Point3d> inputPoints)
        {
            int i, j;

            for (i = 0; i < inputPoints.Count - 1; i++)
            {
                bool duplicateFound = false;

                for (j = i + 1; j < inputPoints.Count; j++)
                {
                    if ((inputPoints[j] - inputPoints[i]).Length < 0.0001)
                    {
                        duplicateFound = true;
                        break;
                    }
                }

                if (duplicateFound)
                {
                    return true;
                }
            }
            return false;
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
                return IBI_toolkit.Properties.Resources.IBI_ICON08;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("dd851961-4438-464f-b53d-e65441ba95b0"); }
        }
    }
}
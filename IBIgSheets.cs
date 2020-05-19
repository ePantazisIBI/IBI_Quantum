using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace IBItoolkit
{
    public class IBIgSheets : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public IBIgSheets()
          : base("Read/Write to Gsheets", "GSheets",
              "A component for reading writing to gsheets",
              "IBItoolkit", "Utilities")
        {
        }

        public IBIgSheets(string name, string nickname, string description, string category, string subCategory) : base(name, nickname, description, category, subCategory)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("gsheet id", "id", "an id that is pointing to a gsheet", GH_ParamAccess.list);
            pManager.AddGenericParameter("Worksheet Name", "Sh", "The name of the worksheet you want to read/write data", GH_ParamAccess.list);
            pManager.AddNumberParameter("Cells", "Res", "Resolution of the output image", GH_ParamAccess.list);
            pManager.AddNumberParameter("Range", "R", "Shell Ranger", GH_ParamAccess.list, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Data out", "Data", "the result of the reading/writing operation", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            String fileId="";
            //use the DA obhject to retrieve the data inside the first input parameter
            DA.GetData("numberOfImages", ref fileId);

            ///
            DA.SetData(0, "doing nothing for now");
            DA.SetData(0, "doing nothing for now");

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
                return IBI_toolkit.Properties.Resources.IBI_ICON04;

            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("29e7c7fd-dee0-4c21-9d38-17bf89a85357"); }
        }
    }
}
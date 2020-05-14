using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace IBItoolkit
{
    public class IBItoolkitComponent3 : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public IBItoolkitComponent3()
          : base("Component4", "IBIquitous",
              "A test component for saving the viewport",
              "IBItoolkit", "Infrastructure")
        {
        }

        public IBItoolkitComponent3(string name, string nickname, string description, string category, string subCategory) : base(name, nickname, description, category, subCategory)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("capture the viewport", "capture", "set to true to save the viewport", GH_ParamAccess.list);
            pManager.AddGenericParameter("filepath", "fp", "a filepath to where to save the image", GH_ParamAccess.list);
            pManager.AddGenericParameter("Viewport Name", "vp", "The name of viewport you want to capture, if not supplied the active viewport will be used", GH_ParamAccess.list);
            pManager.AddNumberParameter("Resolution", "Res", "Resolution of the output image", GH_ParamAccess.list);
            pManager.AddNumberParameter("numberOfImages", "ImgNo", "Number of images you want to create, the defualt number is one", GH_ParamAccess.list, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("output", "out", "the result of the operation", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            int outputImages=0;
            //use the DA obhject to retrieve the data inside the first input parameter
            DA.GetData("numberOfImages", ref outputImages);

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
                return Properties.Resources.IBI_ICON01;
        
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
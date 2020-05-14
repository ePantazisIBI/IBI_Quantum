using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.DocObjects;
using Rhino.Collections;
using Rhino.ApplicationSettings;
using System.Windows.Forms;





// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace IBItoolkit
{


    public class UnitConvTest : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public UnitConvTest()
          : base("UnitConverter", "Unit_Converter",
              "Scales a value from a source unit to a target unit",
              "IBItoolkit", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("inutValue", "V", "provide a value you want to convert. The Defualt value is 1.0", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("source Unit System", "S", "The unit you are want to convert FROM. The Default is the Current Document's Units", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("target Unit System", "T", "The unit you are want to convert TO. The Default is Meters", GH_ParamAccess.item, 1.0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Converted Number", " V", "The number converterd in the desired units", GH_ParamAccess.item);
            pManager.AddTextParameter("info", "i", "This is a message that notifies what conversion you do ", GH_ParamAccess.item);
        }

      




        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        /// 

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double inputValue = 1.0;

            double sourceUnit = 0;
            double targetUnit = 0;

            //what are you meausuring which translates in power 1 = length, 2 = area, 3 = volume
            int unitType = 1;
            String outMessage = "";
            String sourceUnitName = "";
            String targetUnitName = "";
            String unitTypeConvName = "";
            String userUnitName = "";


            Rhino.RhinoDoc doc = Rhino.RhinoDoc.ActiveDoc;
            Rhino.UnitSystem docUnits = doc.ModelUnitSystem;
            Rhino.UnitSystem targetUnits = doc.ModelUnitSystem;




            // this is the number that have to use to convert from the document's units to the desired one) 
            double conversionNumber = 1;

            bool success1 = DA.GetData(0, ref inputValue);
            bool success2 = DA.GetData(1, ref sourceUnit);
            bool success3 = DA.GetData(2, ref targetUnit);

            //a parameter to change between user model units =0 , or from model to user defined units = 1
            double conversionSwitch = 1;




            if (success1 && success2)
            {
               
                if (conversionSwitch == 1)
                {
                   conversionNumber = Rhino.RhinoMath.UnitScale(docUnits, targetUnits);

                }
                //convert from arbitrry units to Model units

                //unitType refers to length,area etc and is set by the drop down
                unitType = 1;//unitTp;
                
                double convertedValue = inputValue * Math.Pow(conversionNumber, unitType);
                
                outMessage = "You are calculating: " + unitTypeConvName + "You have converted FROM: " + sourceUnitName + " TO: " + targetUnitName;

                DA.SetData(0, convertedValue);
                DA.SetData(1, outMessage);



            }
            else
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Check again your inputs");
            }
 

        }


        private bool m_absolute = false;
        public bool Absolute
        {
            get { return m_absolute; }
            set
            {
                m_absolute = value;
                if ((m_absolute))
                {
                    Message = "Absolute";
                }
                else
                {
                    Message = "Standard";
                }
            }
        }

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            // First add our own field.
            writer.SetBoolean("Absolute", Absolute);
            // Then call the base class implementation.
            return base.Write(writer);
        }
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            // First read our own field.
            Absolute = reader.GetBoolean("Absolute");
            // Then call the base class implementation.
            return base.Read(reader);
        }

        protected override void AppendAdditionalComponentMenuItems(System.Windows.Forms.ToolStripDropDown menu)
        {
            // Append the item to the menu, making sure it's always enabled and checked if Absolute is True.
            ToolStripMenuItem item = Menu_AppendItem(menu, "Absolute", Menu_AbsoluteClicked, true, Absolute);
            // Specifically assign a tooltip text to the menu item.
            item.ToolTipText = "When checked, values are made absolute prior to sorting.";
        }

        private void Menu_AbsoluteClicked(object sender, EventArgs e)
        {
            RecordUndoEvent("Absolute");
            Absolute = !Absolute;
            ExpireSolution(true);
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

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace IBI_toolkit
{
    public class IBIUnitConverter : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the IBIUnitConverter class.
        /// </summary>
        public IBIUnitConverter()
          : base("IBIUnitConverter", "UnitConv",
              "Convert A number from model units to user defined units and vice versa",
               "IBItoolkit", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("inutValue", "V", "provide a value you want to convert. The Defualt value is 1.0", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("Target Unit System", "S", "The unit you are want to convert FROM. The Default is the Current Document's Units", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("unitType", "T", "1 = length, 2 = area, 3= volume", GH_ParamAccess.item, 1.0);

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
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double inputValue = 1.0;
            double targetUnit = 0;


            //what are you meausuring which translates in power 1 = length, 2 = area, 3 = volume
            double unitType = 1;


            //NEED TO UPDATE
            // this is the number that have to use to convert from the document's units to the desired one) 
            double conversionNumber = 1;

            bool success1 = DA.GetData(0, ref inputValue);
            bool success2 = DA.GetData(1, ref targetUnit);
            bool success3 = DA.GetData(2, ref unitType);


            String outMessage = "";
            String sourceUnitName = "";
            String targetUnitName = "";
            String unitTypeConvName = "";
            String userUnitName = "";

            unitType = 1;//unitTp;

            

            // Don't worry about where the Absolute property comes from, we'll get to it soon.
            if (ConvType == true)
            {
                //add code here for converting from Model Unit System to User Defined
            }
            else
            {
                //add code here to converting from User Defined to Model Uit

            }

            double convertedValue = inputValue * Math.Pow(conversionNumber, unitType);

            outMessage = "Value is now: " + ConvType;

            DA.SetData(0, convertedValue);
            DA.SetData(1, outMessage);


        }




        private bool m_convert = false;
        public bool ConvType
        {
            get { return m_convert; }
            set
            {
                m_convert = value;
                if (m_convert == true)
                {
                    Message = "Convert TO Model Units";
                }
                else
                {
                    Message = "Convert FROM Model Units";
                }
            }
        }

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            // First add our own field.
            writer.SetBoolean("Convert", ConvType);
            // Then call the base class implementation.
            return base.Write(writer);
        }
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            // First read our own field.
            ConvType = reader.GetBoolean("Convert");
            // Then call the base class implementation.
            return base.Read(reader);
        }

        protected override void AppendAdditionalComponentMenuItems(System.Windows.Forms.ToolStripDropDown menu)
        {
            // Append the item to the menu, making sure it's always enabled and checked if Absolute is True.
            ToolStripMenuItem item = Menu_AppendItem(menu, "Convert From Model Units", Menu_ConversionTypeClicked, true, ConvType);
            // Specifically assign a tooltip text to the menu item.
            item.ToolTipText = "When checked, you are converting from user defined to model system untis.";
        }

        private void Menu_ConversionTypeClicked(object sender, EventArgs e)
        {
            RecordUndoEvent("Convert");
            ConvType = !ConvType;
            ExpireSolution(true);
        }



        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IBI_ICON01.png;
                return IBItoolkit.Properties.Resources.IBI_ICON01;
            }

        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("00772a87-437e-4cd9-b14d-bdc291f3824b"); }
        }
    }
}
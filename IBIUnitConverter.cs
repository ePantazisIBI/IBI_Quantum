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
            pManager.AddNumberParameter("inputNumber", "N", "provide a number you want to convert to a different Unit System. The Default value is 1.0", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("Target Unit System", "S", "The unit System that you want to convert FROM/TO ", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("ConversionType", "T", "0 = From Model units TO User defined ones, 1 = FROM User defined TO Model units", GH_ParamAccess.item, 1.0);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Converted Number", " V", "The number converterd in the desired units", GH_ParamAccess.item);
            pManager.AddTextParameter("info", "i", "This is a message that notifies if the conversion has succeeded  ", GH_ParamAccess.item);

        }


        public enum UnitSystem
        {
            Microns = 1,
            Millimeters = 2,
            Centimeters = 3,
            Meters = 4,
            Kilometers = 5,
            Microinches = 6,
            Mils = 7,
            Inches = 8,
            Feet = 9,
            Miles = 10
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double inputValue = 1.0;
            double targetUnitSys = 0;
            double convType = 0;

            //get the model units from the rhino document
            Rhino.RhinoDoc doc = Rhino.RhinoDoc.ActiveDoc;

            // create variables to hold the input and target unit system
            Rhino.UnitSystem docUnits = doc.ModelUnitSystem;
            Rhino.UnitSystem targetUnits = doc.ModelUnitSystem;


            String outMessage = "";
            String sourceUnitName = docUnits.ToString();
            String targetUnitName = "";
            String unitTypeConvName = "";
            String userUnitName = "";

            double conversionNumber = 1;


            //create a a value for raising the number to an expoenet
            //what are you meausuring which translates in power 1 = length, 2 = area, 3 = volume
            
            //update baseed based on the selection of the menu
            double convExp = conversionExp;


            //read in the values from sliders/ other components
            bool success1 = DA.GetData(0, ref inputValue);
            bool success2 = DA.GetData(1, ref targetUnitSys);
            bool success3 = DA.GetData(2, ref convType);

            //initialize a value
            double convertedValue = 1;



            if (targetUnitSys == 0)
            {

            } else if (targetUnitSys == 1)
            {
                targetUnits = (Rhino.UnitSystem)UnitSystem.Microns;

            } else if (targetUnitSys == 2)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem)UnitSystem.Millimeters;
            }
            else if (targetUnitSys == 3)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem) UnitSystem.Centimeters;
            }
            else if (targetUnitSys == 4)
            {
                //update the unit system
                targetUnits = (Rhino.UnitSystem) UnitSystem.Meters;
            }
            else if (targetUnitSys == 5)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem) UnitSystem.Kilometers;
            }
            else if (targetUnitSys == 6)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem)UnitSystem.Microinches;
            }
            else if (targetUnitSys == 7)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem)UnitSystem.Mils;
            }
            else if (targetUnitSys == 8)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem)UnitSystem.Inches;
            }
            else if (targetUnitSys == 9)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem)UnitSystem.Feet;
            }
            else if (targetUnitSys == 10)
            {
                //update the unit system 
                targetUnits = (Rhino.UnitSystem)UnitSystem.Miles;
            }
            else
            {
                //give an error
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Check again your inputs");
            }

            // get the name of the target conversion system
            targetUnitName = targetUnits.ToString();

            // Don't worry about where the Absolute property comes from, we'll get to it soon.
            if (convType == 0)
            {
                //convert from Model Unit System to User Defined
                conversionNumber = Rhino.RhinoMath.UnitScale(docUnits, targetUnits);
                // write a message
                outMessage = "Conversion Done! The Document's Units System is: " + sourceUnitName + " /You are converting TO: " + targetUnitName + " / The unit conversion number is: " + conversionNumber;
                //convert the value 
                convertedValue = Math.Pow(inputValue * conversionNumber, convExp);

            }
            else
            {
               
                // converting from User Defined to Model Units
                conversionNumber = Rhino.RhinoMath.UnitScale(targetUnits, docUnits);
                // write a message
                outMessage = "Conversion Done! The Document's Units System is: " + sourceUnitName + " /You are converting FROM: " + targetUnitName + " / The unit conversion number is: " + conversionNumber;
                //convert the value 
                convertedValue = Math.Pow(inputValue * conversionNumber, convExp);

            }
           
       

           

            DA.SetData(0, convertedValue);
            DA.SetData(1, outMessage);


        }


    private bool m_convertL = true;
       private bool m_convertA = false;
       private bool m_convertV = false;
       private double conversionExp = 1;



        public bool ConvTypeL
        {
            get
            {
                return m_convertL;
            }
            set
            {
                m_convertL = value;
                if (m_convertL == true)
                {
                    conversionExp = 1;
                    m_convertA = false;
                    m_convertV = false;
                    Message = "Length";
                }
            }
        }


        public bool ConvTypeA
        {
            get 
            { 
                return m_convertA; 
            }
            set
            {
                m_convertA = value;
                if (m_convertA == true)
                {
                    conversionExp = 2;
                    m_convertL = false;
                    m_convertV = false;
                    Message = "Area";
                }
            }
        }


        public bool ConvTypeV
        {
            get
            {
                return m_convertV;
            }
            set
            {
                m_convertV = value;
                if (m_convertV == true)
                {
                    conversionExp = 3;
                    m_convertL = false;
                    m_convertA = false;
                    Message = "Volume";
                }
            }
        }

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            // First add our own field.
            writer.SetBoolean("Length", ConvTypeL);
            writer.SetBoolean("Area", ConvTypeA);
            writer.SetBoolean("Volume", ConvTypeV);
            // Then call the base class implementation.
            return base.Write(writer);
        }
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            // First read our own field.
            ConvTypeL = reader.GetBoolean("Length");
            ConvTypeA = reader.GetBoolean("Area");
            ConvTypeV = reader.GetBoolean("Volume");
            // Then call the base class implementation.
            return base.Read(reader);
        }

        protected override void AppendAdditionalComponentMenuItems(System.Windows.Forms.ToolStripDropDown menu)
        {
            // Append the item to the menu, making sure it's always enabled and checked Which on is True.
            ToolStripMenuItem item1 = Menu_AppendItem(menu, "Length", Menu_ConversionTypeClickedL, true, ConvTypeL);
            ToolStripMenuItem item2 = Menu_AppendItem(menu, "Area", Menu_ConversionTypeClickedA, true, ConvTypeA);
            ToolStripMenuItem item3 = Menu_AppendItem(menu, "Volume", Menu_ConversionTypeClickedV, true, ConvTypeV);
            // Specifically assign a tooltip text to the menu item.
            item1.ToolTipText = "When checked, you are converting Length.";
            item2.ToolTipText = "When checked, you are converting Areas.";
            item3.ToolTipText = "When checked, you are converting Volume.";
        }

        private void Menu_ConversionTypeClickedL(object sender, EventArgs e)
        {
            RecordUndoEvent("Length");
            ConvTypeL = !ConvTypeL;
            ExpireSolution(true);
        }


        private void Menu_ConversionTypeClickedA(object sender, EventArgs e)
        {
            RecordUndoEvent("Area");
            ConvTypeA = !ConvTypeA;
            ExpireSolution(true);
        }


        private void Menu_ConversionTypeClickedV(object sender, EventArgs e)
        {
            RecordUndoEvent("Volume");
            ConvTypeV = !ConvTypeV;
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
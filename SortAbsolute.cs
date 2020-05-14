using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace IBItoolkit
{
    public class SortAbsolute : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public SortAbsolute()
          : base("Sort", "SortAbs",
              "sort a list of numbers and have the custom option to convert those numbers to absolute values",
              "IBItoolkit", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Values", "V", "Values to sort", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Values", "V", "Sorted values", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<double> values = new List<double>();

            if ((!DA.GetDataList(0, values)))
                return;
            if ((values.Count == 0))
                return;

            // Don't worry about where the Absolute property comes from, we'll get to it soon.
            if ((Absolute))
            {
                for (Int32 i = 0; i < values.Count; i++)
                {
                    values[i] = Math.Abs(values[i]);
                }
            }

            values.Sort();
            DA.SetDataList(0, values);
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
            get { return new Guid("3b4cd9e9-85eb-4e6e-a876-d42e07d298b0"); }
        }
    }
}
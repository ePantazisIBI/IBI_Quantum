using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace IBItoolkit
{
    public class IBI_toolkitInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "IBItoolkit";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return IBItoolkit.Properties.Resources.IBI_ICON01;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                String libraryInfo = "This is a collection of tools that have been rendered useful within the practice of IBI Group. The inititative was started by E. Pantazis in May 2020 as a way to compile the design knowledge within the group into a comprehensive library ";
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("89d64f2e-efe8-4de5-b220-d360cba8782b");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Evangelos Pantazis / IBI Quantum";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "evan.pantazis@ibigroup.com";
            }
        }
    }
}

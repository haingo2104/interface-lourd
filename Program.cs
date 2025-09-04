using System;
using System.Windows.Forms;
using MadaTransportConnect.Data;
using MadaTransportConnect.Forms;

namespace MadaTransportConnect
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string conn = "mongodb://localhost:27017"; 
            string dbName = "transport";

            var ctx = new MongoDbContext(conn, dbName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(ctx));
        }
    }
}

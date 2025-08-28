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
            string conn = "mongodb+srv://USER:PWD@CLUSTER.mongodb.net/?retryWrites=true&w=majority"; 
            string dbName = "madatransport";

            var ctx = new MongoDbContext(conn, dbName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(ctx));
        }
    }
}

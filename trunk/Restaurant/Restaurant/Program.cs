using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Restaurant
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalKeyboardHook hook = new GlobalKeyboardHook();
            hook.KeyDown += new KeyEventHandler(hook_KeyDown);
            hook.Hook();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RoomSelectionForm());
        }

        static void hook_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("KEY: " + e.KeyValue);


            if (e.KeyValue == 164 || e.KeyValue == 165 || e.KeyValue == 91)//alt, alt, windowsKey
            {
               // e.SuppressKeyPress = true;
               // e.Handled = true;
            }





            //if (e.KeyValue >= 48 && e.KeyValue <= 57)
            //{
            //}
            //else if(e.KeyValue >= 96 && e.KeyValue <= 105)
            //{
            //}
            //else if (e.KeyValue == 13)
            //{
            //}
            //else
            //{
                
            //}
        }
    }
}

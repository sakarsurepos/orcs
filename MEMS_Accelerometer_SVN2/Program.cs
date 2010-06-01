/////////////////////////////////////////////////////////////
//
//  Filename: Program.cs
//  Author:   Travis
//  Date:     05/12/2006 11:30:36
//  CLR ver:  2.0.50727.42
//  Project:  Accelerometer01
//
//  Copyright © Feirtech 2006
////////////////////////////////////////////////////////////

namespace Accelerometer01
{
    #region Using Statements
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    #endregion

    static class Accelerometer01
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    } // End of Program class
} // End of Accelerometer01 namespace
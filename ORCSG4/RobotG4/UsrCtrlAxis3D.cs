/////////////////////////////////////////////////////////////
//
//  Filename: UsrCtrlAxis3D.cs
//  Author:   Travis Feirtag
//  Date:     05/12/2006 11:30:36
//  CLR ver:  2.0.50727.42
//  Project:  Accelerometer01
//
//  Copyright © 2006 Feirtech Inc.  All rights reserved.
// 
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
////////////////////////////////////////////////////////////

namespace Robot
{
    #region Using Statements
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.DirectX;
    using Microsoft.DirectX.Direct3D;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public sealed class UsrCtrlAxis3D : UserControl
    {
        #region Constants
        private const float DEG_TO_RADIANS = (float)Math.PI / 180;
        private const float PI_HALF = (float)Math.PI / 2;
        #endregion

        #region Fields
        private Device dxDevice = null;
        private Vector3 vtCamera = new Vector3(2, 4, 8);
        private Vector3[] vtrOrigin = new Vector3[8];
        private float fOriginBounds = 3.0f;
        private Mesh boxMesh = null;
        private Material boxMaterial;

        private float fAngleX = 0;
        private float fAngleY = 0;
        private float fAngleZ = 0;

        private float fMinX = 70;
        private float fMaxX = 215;
        private float fMinY = 70;
        private float fMaxY = 215;
        private float fMinZ = 70;
        private float fMaxZ = 215;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public UsrCtrlAxis3D()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint, true);
        }
        #endregion

        #region Dispose Method
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UsrCtrlAxis3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UsrCtrlAxis3D";
            this.Size = new System.Drawing.Size(350, 350);
            this.ResumeLayout(false);

        }
        #endregion

        #region OnLoad Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            InitializeGraphics();
        }
        #endregion

        #region InitializeGraphics Method
        /// <summary>
        /// 
        /// </summary>
        public void InitializeGraphics()
        {
            PresentParameters presentParams = new PresentParameters();
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            presentParams.AutoDepthStencilFormat = DepthFormat.D16;
            presentParams.EnableAutoDepthStencil = true;

            // Create our device
            this.dxDevice = new Device(0, DeviceType.Hardware, this.Handle, CreateFlags.SoftwareVertexProcessing, presentParams);
            this.dxDevice.RenderState.ZBufferEnable = true;
            this.dxDevice.RenderState.ZBufferFunction = Compare.LessEqual;
            this.dxDevice.RenderState.MultiSampleAntiAlias = true;

            this.vtrOrigin[0] = new Vector3(-1.0f * this.fOriginBounds, 0, 0);
            this.vtrOrigin[1] = new Vector3(this.fOriginBounds, 0, 0);
            this.vtrOrigin[2] = new Vector3(0, 0, 0);
            this.vtrOrigin[3] = new Vector3(0, -1.0f * this.fOriginBounds, 0);
            this.vtrOrigin[4] = new Vector3(0, this.fOriginBounds, 0);
            this.vtrOrigin[5] = new Vector3(0, 0, 0);
            this.vtrOrigin[6] = new Vector3(0, 0, -1.0f * this.fOriginBounds);
            this.vtrOrigin[7] = new Vector3(0, 0, this.fOriginBounds);

            this.boxMesh = Mesh.Box(this.dxDevice, 4, 0.7f, 6);
            this.boxMaterial = new Material();
            this.boxMaterial.Emissive = Color.DarkOliveGreen;
            this.boxMaterial.Ambient = Color.WhiteSmoke;
            this.boxMaterial.Diffuse = Color.WhiteSmoke;
        }
        #endregion

        #region SetupCamera Method
        /// <summary>
        /// 
        /// </summary>
        private void SetupCamera()
        {
            this.dxDevice.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1.0f, 100.0f);
            this.dxDevice.Transform.View = Matrix.LookAtLH(this.vtCamera, new Vector3(0, 0, 0), new Vector3(0, 1, 0));

            // First light
            this.dxDevice.Lights[0].Type = LightType.Directional;
            this.dxDevice.Lights[0].Diffuse = Color.White;
            this.dxDevice.Lights[0].Direction = new Vector3(1, -1, -1);
            this.dxDevice.Lights[0].Update();
            this.dxDevice.Lights[0].Enabled = true;

            // Second light
            //this.dxDevice.Lights[1].Type = LightType.Directional;
            //this.dxDevice.Lights[1].Diffuse = Color.White;
            //this.dxDevice.Lights[1].Direction = new Vector3(-1, 1, -1);
            //this.dxDevice.Lights[1].Update();
            //this.dxDevice.Lights[1].Enabled = true;
        }
        #endregion

        #region OnPaint Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paintEventArgs"></param>
        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            this.dxDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.DarkBlue, 1.0f, 0);
            SetupCamera();

            this.dxDevice.BeginScene();

            this.dxDevice.Transform.World = Matrix.RotationX(this.fAngleY) * Matrix.RotationZ(this.fAngleX);
            this.dxDevice.Material = this.boxMaterial;
            this.boxMesh.DrawSubset(0);

            DrawOrigin();

            this.dxDevice.EndScene();
            this.dxDevice.Present();
        }
        #endregion

        #region DrawOrigin Method
        /// <summary>
        /// 
        /// </summary>
        private void DrawOrigin()
        {
            this.dxDevice.Transform.World = Matrix.Translation(0, 0, 0);
            Matrix lineMatrix = this.dxDevice.Transform.World * this.dxDevice.Transform.View * this.dxDevice.Transform.Projection;

            using (Line myLine = new Line(this.dxDevice))
            {
                // Set the width
                myLine.Width = 1;

                // Should they be antialiased?
                myLine.Antialias = true;

                // Draw the line
                myLine.Begin();
                myLine.DrawTransform(this.vtrOrigin, lineMatrix, Color.WhiteSmoke);
                myLine.End();

            } // End of using block
        }
        #endregion

        #region SetAxesValues Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fValueX"></param>
        /// <param name="fValueY"></param>
        public void SetAxesValues(float fValueX, float fValueY, float fValueZ)
        {
            if (fValueX < this.fMinX)
                this.fMinX = fValueX;

            if (fValueX > this.fMaxX)
                this.fMaxX = fValueX;

            if (fValueY < this.fMinY)
                this.fMinY = fValueY;

            if (fValueY > this.fMaxY)
                this.fMaxY = fValueY;

            if (fValueZ < this.fMinZ)
                this.fMinZ = fValueZ;

            if (fValueZ > this.fMaxZ)
                this.fMaxZ = fValueZ;

            // Do some calculations
            // Minimum value will be 0 degrees
            // Maximum value will be 180 degrees
            float fUnitX = (180 / (this.fMaxX - this.fMinX));
            this.fAngleX = (((fValueX - this.fMinX) * fUnitX) * DEG_TO_RADIANS) - PI_HALF;
            Robot1.fAnglex0 = ((fValueX - this.fMinX) * fUnitX); //add x

            //float fUnitY = 1 * (180 / (this.fMaxY - this.fMinY));
            //this.fAngleY = (((fValueY - this.fMinY) * fUnitY) * DEG_TO_RADIANS) - PI_HALF;
            //FrmMain.fAngley0 = ((fValueY - this.fMinY) * fUnitY); //add y

            float fUnitZ = 1 * (180 / (this.fMaxZ - this.fMinZ));
            this.fAngleY = (((fValueZ - this.fMinZ) * fUnitZ) * DEG_TO_RADIANS) - PI_HALF;
            Robot1.fAnglez0 = ((fValueZ - this.fMinZ) * fUnitZ); //add z

            // Refresh
            this.Refresh();
        }
        #endregion
    } // End of UsrCtrlAxis3D class
} // End of namespace
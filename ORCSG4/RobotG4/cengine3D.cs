using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//3D
using MTV3D65; //Truevision3D

namespace Robot
{

    public class cengine3D
    {
        //////////
        //3D MODEL
        public TVEngine tv;
        public TVScene scene;
        public TVInputEngine input;
        public TVGlobals globals;
        public TVMathLibrary maths;
        public TVTextureFactory texturefactory;
        public TVMaterialFactory materialfactory;
        public TVCamera camera;
        public TVViewport viewport;
        public TVLightEngine lights;
        public TVAtmosphere atmosphere;
        public TVPhysics physics;
        //Game Loop
        public bool bDoLoop;
        //Terrain
        public TVLandscape Land;
        public int pbLand;
        //Materials
        public int matLand;
        public int matVehicleBody;
        public int matWindow;
        public int matWheels;
        //Models
        public TVMesh pk_9;
        public TVMesh m_chassis;
        public TVMesh m_fl;
        public TVMesh m_fr;
        public TVMesh m_rl;
        public TVMesh m_rr;
        public int pbi_chassis;
        public int flw;
        public int frw;
        public int rlw;
        public int rrw;
        public int car_ID;
        public float steerAngle;
        public float CarPower = 800;
        public int pmatTerrain;
        public int pmatChassis;
        public bool autrst = false;
        public bool camfol = false;
        public bool camtop = false;
        //3D MODEL
        //////////

        public void Init3D()
        {
            lights = new TVLightEngine();
            globals = new TVGlobals();
            atmosphere = new TVAtmosphere();
            maths = new TVMathLibrary();
            materialfactory = new TVMaterialFactory();
            texturefactory = new TVTextureFactory();
            tv = new TVEngine();
            physics = new TVPhysics();

            //Setup TV
            ////tv.SetDebugMode(true, true);
            ////tv.SetDebugFile(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\debugfile.txt");
            tv.SetAntialiasing(true, CONST_TV_MULTISAMPLE_TYPE.TV_MULTISAMPLE_2_SAMPLES);

            //Enter Your Beta Username And Password Here
            //tv.SetBetaKey("", "");

            tv.SetAngleSystem(CONST_TV_ANGLE.TV_ANGLE_DEGREE);
            //tv.Init3DWindowed(this.Handle, true);
        }

        public void Init3D2()
        {
            tv.GetViewport().SetAutoResize(true);
            tv.DisplayFPS(true);
            tv.SetVSync(true);

            scene = new TVScene();

            input = new TVInputEngine();
            input.Initialize(true, true);

            camera = new TVCamera();
            camera = scene.GetCamera();
            camera.SetViewFrustum(45, 1000, 0.1f);
            camera.SetPosition(0, 5, -20);
            camera.SetLookAt(0, 3, 0);

            viewport = new TVViewport();
        }
        
        public void Init3D3()
        {
            viewport.SetCamera(camera);
            viewport.SetBackgroundColor(Color.Blue.ToArgb());
            bDoLoop = true;

            InitSound();
            InitMaterials();
            InitTextures();
            InitFonts();
            InitShaders();
            InitEnvironment();
            InitPhysics();
            InitLandscape();
            InitObjects();
            InitLights();
            InitPhysicsMaterials();
            Init2DText();

            //tv = null;

            //this.Close();
            //// /3D
        }

        ////3D
        private void InitSound()
        {
            //Add code here
        }

        private void InitGame2DText()
        {
            //Add code here
        }

        private void InitEnvironment()
        {
            //SkyBox
            atmosphere.SkyBox_SetTexture(globals.GetTex("SkyFront"), globals.GetTex("SkyBack"), globals.GetTex("SkyLeft"), globals.GetTex("SkyRight"), globals.GetTex("SkyTop"), globals.GetTex("SkyBottom"));
            atmosphere.SkyBox_Enable(true);
        }

        private void InitFonts()
        {
            //Add code here
        }

        private void InitShaders()
        {
            //Add code here
        }

        private void Init2DText()
        {
            //Add code here
        }

        private void InitLights()
        {
            lights.CreateDirectionalLight(new TV_3DVECTOR(1, -1, -1), 1, 1, 1, "Sun", 1);
            lights.SetLightProperties(globals.GetLight("Sun"), true, true, true);
            lights.SetSpecularLighting(true);
        }

        private void InitPhysics()
        {
            physics.Initialize();
            physics.SetSolverModel(CONST_TV_PHYSICS_SOLVER.TV_SOLVER_EXACT);
            physics.SetFrictionModel(CONST_TV_PHYSICS_FRICTION.TV_FRICTION_EXACT);
            physics.SetGlobalGravity(new TV_3DVECTOR(0, -9.8f, 0));
        }

        public void InitObjects()
        {
            #region Car
            //Building PK9
            pk_9 = scene.CreateMeshBuilder("pk");
            // load the object from an x file
            pk_9.LoadTVM(@"Models\pk8.tvm", false, false);
            // set its position
            pk_9.SetPosition(5.0f, 0.0f, 50.0f);
            // make the table 3x larger
            pk_9.SetScale(3, 3, 3);
            // rotate it 25 degrees around the Y 3D Axis
            pk_9.RotateY(25, true);
            // set the tables texture
            pk_9.SetShadowCast(true, true);
            pk_9.SetTexture(globals.GetTex("pk9tex"), 0);

            //Chassis
            m_chassis = scene.CreateMeshBuilder("mChassis");
            m_chassis.LoadTVM(@"Models\chassis.tvm", false, false);
            m_chassis.SetShadowCast(true, true);
            m_chassis.SetTexture(globals.GetTex("ChassisSTI"), 0);
            //m_chassis.SetTextureEx(0, globals.GetTex("ChassisSTI"), 1);
            //m_chassis.SetTextureEx(1, globals.GetTex("ChassisSTI"), 1);
            m_chassis.SetTexture(globals.GetTex("UnderCarriage"), 2);
            m_chassis.SetMaterial(matWindow, 1);
            m_chassis.SetAlphaTest(true, 0, true, 1);
            m_chassis.SetBlendingMode(CONST_TV_BLENDINGMODE.TV_BLEND_ADD, 1);
            m_chassis.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_chassis.SetShadowCast(true, true);

            //Front Left Wheel
            float scale = 1f;
            m_fl = scene.CreateMeshBuilder("mfl");
            m_fl.LoadTVM(@"Models\wheel_l.tvm", true, true);
            m_fl.SetScale(scale, scale, scale);
            m_fl.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_fl.SetMaterial(matWheels);
            m_fl.SetTexture(globals.GetTex("Wheel"));
            m_fl.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_fl.SetShadowCast(true, true);

            //Front Right Wheel
            m_rl = scene.CreateMeshBuilder("mrl");
            m_rl.LoadTVM(@"Models\wheel_l.tvm", true, true);
            m_rl.SetScale(scale, scale, scale);
            m_rl.SetMaterial(matWheels);
            m_rl.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_rl.SetTexture(globals.GetTex("Wheel"));
            m_rl.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_rl.SetShadowCast(true, false);
            m_rl.SetShadowCast(true, true);

            //Rear Left Wheel
            m_fr = scene.CreateMeshBuilder("mfr");
            m_fr.LoadTVM(@"Models\wheel_r.tvm", true, true);
            m_fr.SetScale(scale, scale, scale);
            m_fr.SetMaterial(matWheels);
            m_fr.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_fr.SetTexture(globals.GetTex("Wheel"));
            m_fr.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_fr.SetShadowCast(true, false);

            //Rear Right Wheel
            m_rr = scene.CreateMeshBuilder("mrr");
            m_rr.LoadTVM(@"Models\wheel_r.tvm", true, true);
            m_rr.SetScale(scale, scale, scale);
            m_rr.SetMaterial(matWheels);
            m_rr.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_rr.SetTexture(globals.GetTex("Wheel"));
            m_rr.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_rr.SetShadowCast(true, false);
            m_rr.SetShadowCast(true, true);

            m_chassis.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_chassis.SetMaterial(matVehicleBody);
            m_chassis.ComputeNormals();
            m_chassis.ComputeBoundings();
            m_chassis.SetScale(scale, scale, scale);
            #endregion

            //Add The Physics to the chassis
            pbi_chassis = physics.CreateMeshBody(1500, m_chassis, CONST_TV_PHYSICSBODY_BOUNDING.TV_BODY_CONVEXHULL); //1500
            physics.SetAutoFreeze(pbi_chassis, false);
            physics.SetBodyPosition(pbi_chassis, 0f, 15, 0f);
            physics.SetBodyRotation(pbi_chassis, 0f, 0f, 0f);

            //Create The Vehicle
            car_ID = physics.CreateVehicle(pbi_chassis);

            //Do Suspention Settings
            float susheight = 1.5f; //distance from chassis to wheel 0.5f
            float susplen = 1.5f; // 10
            float susshock = 40f; //Springiness of suspension 10
            float susspring = 300f; //Stiffness of suspension 400
            flw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), -0.8f * scale, -susheight * scale - 0.1f, 1.25f * scale + 0.5f, 1, 0, 0, susplen, susshock, susspring, m_fl); //fl
            frw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), 0.8f * scale, -susheight * scale - 0.1f, 1.25f * scale + 0.5f, 1, 0, 0, susplen, susshock, susspring, m_fr); //fr
            rlw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), -0.8f * scale, -susheight * scale - 0.1f, -1.425f * scale + 0.2f, 1, 0, 0, susplen, susshock, susspring, m_rl); //rl
            rrw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), 0.8f * scale, -susheight * scale - 0.1f, -1.425f * scale + 0.2f, 1, 0, 0, susplen, susshock, susspring, m_rr); //rr

            //Change the car's center of mass / make it drive better
            physics.SetBodyCenterOfMass(car_ID, new TV_3DVECTOR(0, -1.0f, 10f));

            //Add wheel frictions
            //Note that this code will also stop sliding on slopes
            float sideslip = 0.1f;
            float sideslipcoef = 0f;
            float maxlongslide = 10000f;
            float maxlongslidecoef = 0f;
            physics.SetVehicleWheelParameters(car_ID, flw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
            physics.SetVehicleWheelParameters(car_ID, frw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
            physics.SetVehicleWheelParameters(car_ID, rlw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
            physics.SetVehicleWheelParameters(car_ID, rrw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
        }

        public void InitPhysicsMaterials()
        {
            //TerrainLandscape
            pmatTerrain = physics.CreateMaterialGroup("Terrain");
            physics.SetMaterialInteractionFriction(0, pmatTerrain, 0.9f, 1f);
            physics.SetMaterialInteractionSoftness(0, pmatTerrain, 1f);
            physics.SetMaterialInteractionBounciness(0, pmatTerrain, 0.1f);
            physics.SetBodyMaterialGroup(pbLand, pmatTerrain);

            //Chassis   
            pmatChassis = physics.CreateMaterialGroup("Chassis");
            physics.SetMaterialInteractionFriction(pmatChassis, pmatTerrain, 0.3f, 0.17f);
            physics.SetMaterialInteractionBounciness(pmatChassis, pmatTerrain, 0.1f);
            physics.SetMaterialInteractionSoftness(pmatChassis, pmatTerrain, 1000f);
            physics.SetBodyMaterialGroup(pbi_chassis, pmatChassis);
        }

        public void InitMaterials()
        {
            //Create Materials
            matLand = materialfactory.CreateMaterial("land");
            matWindow = materialfactory.CreateMaterial("CarWindows");
            matVehicleBody = materialfactory.CreateMaterial("matVehicleBody");
            matWheels = materialfactory.CreateMaterial("matWheels");

            //Land
            materialfactory.SetSpecular(matLand, 0.1f, 0.1f, 0.1f, 1f);

            //Car Windows
            materialfactory.SetAmbient(matWindow, 1, 1, 1, 1);
            materialfactory.SetDiffuse(matWindow, 1, 1, 1, 1);
            materialfactory.SetSpecular(matWindow, 0.8f, 0.8f, 0.8f, 1);
            materialfactory.SetPower(matWindow, 10);
            materialfactory.SetOpacity(matWindow, 1f);

            //Vehicle Body
            materialfactory.SetAmbient(matVehicleBody, 0.2f, 0.2f, 0.2f, 1);
            materialfactory.SetDiffuse(matVehicleBody, 0.9f, 0.9f, 0.9f, 1f);
            materialfactory.SetSpecular(matVehicleBody, 1f, 1f, 1f, 1);
            materialfactory.SetPower(matVehicleBody, 100);
            materialfactory.SetEmissive(matVehicleBody, 0, 0, 0.1f, 1);

            //Wheels
            materialfactory.SetAmbient(matWheels, 0.8f, 0.8f, 0.8f, 1);
            materialfactory.SetDiffuse(matWheels, 0.2f, 0.2f, 0.2f, 1f);
            materialfactory.SetSpecular(matWheels, 0.2f, 0.2f, 0.2f, 1);
            materialfactory.SetPower(matWheels, 200);
            materialfactory.SetEmissive(matWheels, 0f, 0f, 0, 1);
        }

        private void InitTextures()
        {
            #region Car
            //Create Very Small Colored Texture For Windows
            int i = texturefactory.CreateTexture(1, 1, true, "TintedWindows");
            texturefactory.SetPixel(i, 0, 0, Color.DarkGray.ToArgb()); //Color.DarkGray.ToArgb()

            //Buildings texture
            texturefactory.LoadTexture(@"Textures\pk9tex.bmp", "pk9tex");

            //Vehicle
            texturefactory.LoadTexture(@"Textures\Windows.bmp", "Windows");
            texturefactory.LoadTexture(@"Textures\UnderCarriage.bmp", "UnderCarriage");
            texturefactory.LoadTexture(@"Textures\ChassisSTI.bmp", "ChassisSTI");
            texturefactory.LoadTexture(@"Textures\Wheel.bmp", "Wheel");
            #endregion

            //Land
            texturefactory.LoadTexture(@"Textures\grass.jpg", "Grass");

            //Sky Box
            texturefactory.LoadTexture(@"Textures\skyup.jpg", "SkyTop");
            texturefactory.LoadTexture(@"Textures\skydown.jpg", "SkyBottom");
            texturefactory.LoadTexture(@"Textures\skyleft.jpg", "SkyLeft");
            texturefactory.LoadTexture(@"Textures\skyright.jpg", "SkyRight");
            texturefactory.LoadTexture(@"Textures\skyfront.jpg", "SkyFront");
            texturefactory.LoadTexture(@"Textures\skyback.jpg", "SkyBack");
        }

        private void InitLandscape()
        {
            Land = scene.CreateLandscape("Land");

            Land.SetAffineLevel(CONST_TV_LANDSCAPE_AFFINE.TV_AFFINE_LOW);
            int twidth = (64 * 8) / 256;
            int theight = (64 * 8) / 256;

            Land.GenerateTerrain(@"Heightmaps\heightmap.bmp", CONST_TV_LANDSCAPE_PRECISION.TV_PRECISION_HIGH, twidth / 2, theight / 2, 0, 0, 0, true);

            Land.SetTexture(globals.GetTex("Grass"));
            Land.SetMaterial(matLand);
            Land.SetTextureScale(0.3f, 0.3f);
            Land.SetPosition(-((twidth * 256) / 2) + 120, 0, -((theight * 256) / 2) + 120);
            pbLand = physics.CreateStaticTerrainBody(Land);
        }

    }
}

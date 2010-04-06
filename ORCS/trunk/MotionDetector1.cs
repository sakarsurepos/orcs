// Motion Detector
//
// Copyright © Andrew Kirillov, 2005-2006
// andrew.kirillov@gmail.com
//
namespace Robot
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;

	using AForge.Imaging;
	using AForge.Imaging.Filters;

	/// <summary>
	/// MotionDetector1
	/// </summary>
	public class MotionDetector1 : IMotionDetector
	{
		private IFilter	grayscaleFilter = new GrayscaleBT709( );
		private Difference differenceFilter = new Difference( );
        private Threshold thresholdFilter = new Threshold( 15 );
		private IFilter erosionFilter = new Erosion( );
		private Merge mergeFilter = new Merge( );

		private IFilter extrachChannel = new ExtractChannel( RGB.R );
		private ReplaceChannel replaceChannel = new ReplaceChannel( RGB.R, null );

		private Bitmap	backgroundFrame;
        private BitmapData bitmapData;

		private bool	calculateMotionLevel = false;
		private int		width;	// image width
		private int		height;	// image height
		private int		pixelsChanged;

		// Motion level calculation - calculate or not motion level
		public bool MotionLevelCalculation
		{
			get { return calculateMotionLevel; }
			set { calculateMotionLevel = value; }
		}
	
		// Motion level - amount of changes in percents
		public double MotionLevel
		{
			get { return (double) pixelsChanged / ( width * height ); }
		}


		// Constructor
		public MotionDetector1( )
		{
		}

		// Reset detector to initial state
		public void Reset( )
		{
			if ( backgroundFrame != null )
			{
				backgroundFrame.Dispose( );
				backgroundFrame = null;
			}
		}

		// Process new frame
		public void ProcessFrame( ref Bitmap image )
		{
			if ( backgroundFrame == null )
			{
				// create initial backgroung image
				backgroundFrame = grayscaleFilter.Apply( image );

				// get image dimension
				width	= image.Width;
				height	= image.Height;

				// just return for the first time
				return;
			}

			Bitmap tmpImage;

			// apply the grayscale file
			tmpImage = grayscaleFilter.Apply( image );

			// set backgroud frame as an overlay for difference filter
			differenceFilter.OverlayImage = backgroundFrame;

            // apply difference filter
            Bitmap tmpImage2 = differenceFilter.Apply( tmpImage );

            // lock the temporary image and apply some filters on the locked data
            bitmapData = tmpImage2.LockBits( new Rectangle( 0, 0, width, height ),
                ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed );

            // threshold filter
            thresholdFilter.ApplyInPlace( bitmapData );
            // erosion filter
            Bitmap tmpImage3 = erosionFilter.Apply( bitmapData );

            // unlock temporary image
            tmpImage2.UnlockBits( bitmapData );
            tmpImage2.Dispose( );


            //moj kod
            Color col = new Color();
            Color col2 = new Color();
            Color col3 = new Color();
            Color col4 = new Color();

            col = image.GetPixel(Robot1.mv1x, Robot1.mv1y);
            col2 = image.GetPixel(105, 200);
            col3 = image.GetPixel(115, 200);
            col4 = image.GetPixel(125, 200);

            Robot1.A1 = (col.A).ToString();
            Robot1.R1 = (col.R).ToString();
            Robot1.G1 = (col.G).ToString();                                                                         
            Robot1.B1 = (col.B).ToString();

            Robot1.Setlabel112 = col2.R;
            Robot1.Setlabel114 = col2.G;
            Robot1.Setlabel116 = col2.B;

            Robot1.Setlabel118 = col3.R;
            Robot1.Setlabel120 = col3.G;
            Robot1.Setlabel119 = col3.B;

            Robot1.Setlabel115 = col4.R;
            Robot1.Setlabel113 = col4.G;
            Robot1.Setlabel111 = col4.B;



            if (col2.R >= 150 && col2.G <= 150 && col2.B <= 150)
            {
                Robot1.vysledok1 = "Detected Color";
            }
            else
            {
                Robot1.vysledok1 = "Undetected Color";
            }

            if (col3.R >= 150 && col3.G <= 150 && col3.B <= 150)
            {
                Robot1.vysledok2 = "Detected Color";
                Robot1.c = true;
            }  
            else
            {
                Robot1.vysledok2 = "Undetected Color";
            }
            if (col4.R >= 150 && col4.G <= 150 && col4.B <= 150)
            {
                Robot1.vysledok3 = "Detected Color";
            }
            else
            {
                Robot1.vysledok3 = "Undetected Color";
            }
            if (col2.R >= 150 && col2.G <= 150 && col2.B <= 150 && col3.R >= 150 && col3.G <= 150 && col3.B <= 150 && col4.R >= 150 && col4.G <= 150 && col4.B <= 150)
            {
                Robot1.c = true;

            }

            int i = 0;
            int x1 = 100;
            int y1 = 100;
            int x2 = 130;
            int y2 = 100;

            while (i < 15)
            {
                image.SetPixel(x1, y1++, Color.Black);
                image.SetPixel(x2, y1++, Color.Black);
                i++;
            }

            int j = 0;
            while (j < 15)
            {
                image.SetPixel(x1++, y1, Color.Black);
                image.SetPixel(x1++, y2, Color.Black);
                j++;
            }
            image.SetPixel(10, 10, Color.White);
            image.SetPixel(10, 11, Color.White);
            image.SetPixel(10, 12, Color.White);
            image.SetPixel(10, 13, Color.White);
            image.SetPixel(10, 14, Color.White);
            image.SetPixel(10, 15, Color.White);
            image.SetPixel(10, 16, Color.White);
            //moj kod

            // calculate amount of changed pixels
			pixelsChanged = ( calculateMotionLevel ) ?
				CalculateWhitePixels( tmpImage3 ) : 0;

			// dispose old background
			backgroundFrame.Dispose( );
			// set backgound to current
			backgroundFrame = tmpImage;

			// extract red channel from the original image
			Bitmap redChannel = extrachChannel.Apply( image );

			//  merge red channel with moving object
			mergeFilter.OverlayImage = tmpImage3;
			Bitmap tmpImage4 = mergeFilter.Apply( redChannel );
			redChannel.Dispose( );
			tmpImage3.Dispose( );

			// replace red channel in the original image
			replaceChannel.ChannelImage = tmpImage4;
			Bitmap tmpImage5 = replaceChannel.Apply( image );
			tmpImage4.Dispose( );

			image.Dispose( );
            if (Robot1.filter == true)
            {
                image = tmpImage5;
                if (Robot1.mot == true)
                {
                    tmpImage.Save("mot.jpg");
                    Robot1.mot = false;
                }
            }
            else 
            { 
                image = tmpImage;
            } //image = tmpImage5;
		}

		// Calculate white pixels
		private int CalculateWhitePixels( Bitmap image )
		{
			int count = 0;

			// lock difference image
			BitmapData data = image.LockBits( new Rectangle( 0, 0, width, height ),
				ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed );

			int offset = data.Stride - width;

			unsafe
			{
				byte * ptr = (byte *) data.Scan0.ToPointer( );

				for ( int y = 0; y < height; y++ )
				{
					for ( int x = 0; x < width; x++, ptr++ )
					{
						count += ( (*ptr) >> 7 );
					}
					ptr += offset;
				}
			}
			// unlock image
			image.UnlockBits( data );

			return count;
		}
	}
}

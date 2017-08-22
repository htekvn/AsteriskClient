// C# TaskbarNotifier Class v1.0
// by John O'Byrne - 02 december 2002
// 01 april 2003 : Small fix in the OnMouseUp handler
// 11 january 2003 : Patrick Vanden Driessche <pvdd@devbrains.be> added a few enhancements
//           Small Enhancements/Bugfix
//           Small bugfix: When Content text measures larger than the corresponding ContentRectangle
//                         the focus rectangle was not correctly drawn. This has been solved.
//           Added KeepVisibleOnMouseOver
//           Added ReShowOnMouseOver
//           Added If the Title or Content are too long to fit in the corresponding Rectangles,
//                 the text is truncateed and the ellipses are appended (StringTrimming).

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LogUtils;

namespace CustomUIControls
{
	/// <summary>
	/// TaskbarNotifier allows to display MSN style/Skinned instant messaging popups
	/// </summary>
	public class TaskbarNotifier : System.Windows.Forms.Form
	{
		#region TaskbarNotifier Protected Members
		protected Bitmap BackgroundBitmap = null;
		protected Bitmap CloseBitmap = null;
		protected Point CloseBitmapLocation;
		protected Size CloseBitmapSize;
		protected Rectangle RealTitleRectangle;
		protected Rectangle RealContentRectangle;
		protected Rectangle WorkAreaRectangle;
		protected Timer timer = new Timer();
		protected TaskbarStates taskbarState = TaskbarStates.hidden;
		protected string topLineText;
		protected string middleLineText;
		protected Color normalTitleColor = Color.FromArgb(255,0,0);
		protected Color hoverTitleColor = Color.FromArgb(255,0,0);
		protected Color normalContentColor = Color.FromArgb(0,0,0);
		protected Color hoverContentColor = Color.FromArgb(0,0,0x66);
		protected Font normalTitleFont = new Font("Arial",12,FontStyle.Regular,GraphicsUnit.Pixel);
        protected Font currentTopFont = null;
		protected Font hoverTitleFont = new Font("Arial",12,FontStyle.Bold,GraphicsUnit.Pixel);
        protected Font normalContentFont = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
		protected Font hoverContentFont = new Font("Arial",11,FontStyle.Bold,GraphicsUnit.Pixel);
		protected int nShowEvents;
		protected int nHideEvents;
		protected int nVisibleEvents;
		protected int nIncrementShow;
		protected int nIncrementHide;
		protected bool bIsMouseOverPopup = false;
		protected bool bIsMouseOverClose = false;
		protected bool bIsMouseOverContent = false;
		protected bool bIsMouseOverTitle = false;
		protected bool bIsMouseDown = false;
		protected bool bKeepVisibleOnMouseOver = true;			// Added Rev 002
		protected bool bReShowOnMouseOver = false;				// Added Rev 002
		#endregion
						
		#region TaskbarNotifier Public Members
		public Rectangle TitleRectangle;
		public Rectangle ContentRectangle;
		public bool TitleClickable = false;
		public bool ContentClickable = true;
		public bool CloseClickable = true;
		public bool EnableSelectionRectangle = true;
		//public event EventHandler CloseClick = null;
		//public event EventHandler TitleClick = null;
		public event EventHandler ContentClick = null;
        public int Index = 0;
        public int SourceTop = 0;
        
        public DateTime BeginTime = DateTime.MinValue;
        public DateTime EndTime = DateTime.MinValue;
        public Timer timerClock = new Timer();

        Asterisk.NET.WinForm.FormMain.Data _Owner;

		#endregion
	
		#region TaskbarNotifier Enums
		/// <summary>
		/// List of the different popup animation status
		/// </summary>
		public enum TaskbarStates
		{
			hidden = 0,
			appearing = 1,
			visible = 2,
			disappearing = 3,
            moving = 4
		}
		#endregion

		#region TaskbarNotifier Constructor
		/// <summary>
		/// The Constructor for TaskbarNotifier
		/// </summary>
		public TaskbarNotifier(int iIndex,  Asterisk.NET.WinForm.FormMain.Data __Owner)
		{
            _Owner = __Owner;
            Index = iIndex;
            // Window Style
			FormBorderStyle = FormBorderStyle.None;
			WindowState = FormWindowState.Minimized;
			base.Show();
			base.Hide();
			WindowState = FormWindowState.Normal;
			ShowInTaskbar = false;
			TopMost = true;
			MaximizeBox = false;
			MinimizeBox = false;
			ControlBox = false;
			
			timer.Enabled = true;
			timer.Tick += new EventHandler(OnTimer);

            timerClock.Enabled = true;
            timerClock.Interval = 1000;
            timerClock.Tick += new EventHandler(OnTimerClock);
            MouseWheel += new MouseEventHandler(Taskbar_MouseWheel);
            
		}
		#endregion

		#region TaskbarNotifier Properties
		/// <summary>
		/// Get the current TaskbarState (hidden, showing, visible, hiding)
		/// </summary>
		public TaskbarStates TaskbarState
		{
			get
			{
				return taskbarState;
			}
		}

		/// <summary>
		/// Get/Set the popup Title Text
		/// </summary>
    /*    public string callername
		{
			get
			{
                return callername;
			}
			set
			{
                callername = value;
				//refresh();
			}
		}

		/// <summary>
		/// get/set the popup content text
		/// </summary>
        public string callernumber
		{
			get
			{
                return callernumber;
			}
			set
			{
                callernumber = value;
				//refresh();
			}
		}*/

		/// <summary>
		/// Get/Set the Normal Title Color
		/// </summary>
		public Color NormalTitleColor
		{
			get
			{
				return normalTitleColor;
			}
			set
			{
				normalTitleColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Get/Set the Hover Title Color
		/// </summary>
		public Color HoverTitleColor
		{
			get
			{
				return hoverTitleColor;
			}
			set
			{
				hoverTitleColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Get/Set the Normal Content Color
		/// </summary>
		public Color NormalContentColor
		{
			get
			{
				return normalContentColor;
			}
			set
			{
				normalContentColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Get/Set the Hover Content Color
		/// </summary>
		public Color HoverContentColor
		{
			get
			{
				return hoverContentColor;
			}
			set
			{
				hoverContentColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Get/Set the Normal Title Font
		/// </summary>
		public Font NormalTitleFont
		{
			get
			{
				return normalTitleFont;
			}
			set
			{
				normalTitleFont = value;
				Refresh();
			}
		}

		/// <summary>
		/// Get/Set the Hover Title Font
		/// </summary>
		public Font HoverTitleFont
		{
			get
			{
				return hoverTitleFont;
			}
			set
			{
				hoverTitleFont = value;
				Refresh();
			}
		}

		/// <summary>
		/// Get/Set the Normal Content Font
		/// </summary>
		public Font NormalContentFont
		{
			get
			{
				return normalContentFont;
			}
			set
			{
				normalContentFont = value;
				Refresh();
			}
		}

		/// <summary>
		/// Get/Set the Hover Content Font
		/// </summary>
		public Font HoverContentFont
		{
			get
			{
				return hoverContentFont;
			}
			set
			{
				hoverContentFont = value;
				Refresh();
			}
		}

		/// <summary>
		/// Indicates if the popup should remain visible when the mouse pointer is over it.
		/// Added Rev 002
		/// </summary>
		public bool KeepVisibleOnMousOver
		{
			get
			{
				return bKeepVisibleOnMouseOver;
			}
			set
			{
				bKeepVisibleOnMouseOver=value;
			}
		}

		/// <summary>
		/// Indicates if the popup should appear again when mouse moves over it while it's disappearing.
		/// Added Rev 002
		/// </summary>
		public bool ReShowOnMouseOver
		{
			get
			{
				return bReShowOnMouseOver;
			}
			set
			{
				bReShowOnMouseOver=value;
			}
		}

		#endregion

		#region TaskbarNotifier Public Methods
		[DllImport("user32.dll")]
		private static extern Boolean ShowWindow(IntPtr hWnd,Int32 nCmdShow);
		/// <summary>
		/// Displays the popup for a certain amount of time
		/// </summary>
		/// <param name="strTitle">The string which will be shown as the title of the popup</param>
		/// <param name="strContent">The string which will be shown as the content of the popup</param>
		/// <param name="nTimeToShow">Duration of the showing animation (in milliseconds)</param>
		/// <param name="nTimeToStay">Duration of the visible state before collapsing (in milliseconds)</param>
		/// <param name="nTimeToHide">Duration of the hiding animation (in milliseconds)</param>
		/// <returns>Nothing</returns>
		public void Show(string strName, string strPhoneNumber, int nTimeToShow=500, int nTimeToStay=500, int nTimeToHide=500)
		{
            if (taskbarState != TaskbarStates.hidden) return;

            WorkAreaRectangle = Screen.GetWorkingArea(WorkAreaRectangle);

            currentTopFont = string.IsNullOrEmpty(strName) ? normalContentFont : hoverContentFont;
            topLineText = string.IsNullOrEmpty(strName) ? Asterisk.NET.WinForm.FormMain.NameGroups[_Owner.GroupId] : strName;
            middleLineText = string.IsNullOrEmpty(strPhoneNumber) ? "unknown number" : strPhoneNumber;

            nVisibleEvents = nTimeToStay;
			CalculateMouseRectangles();
			// We calculate the pixel increment and the timer value for the showing animation
			int nEvents;
			if (nTimeToShow > 10)
			{
				nEvents = Math.Min((nTimeToShow / 10), BackgroundBitmap.Height);
				nShowEvents = nTimeToShow / nEvents;
				nIncrementShow = BackgroundBitmap.Height / nEvents;
			}
			else
			{
				nShowEvents = 10;
				nIncrementShow = BackgroundBitmap.Height;
			}

			// We calculate the pixel increment and the timer value for the hiding animation
			if( nTimeToHide > 10)
			{
				nEvents = Math.Min((nTimeToHide / 10), BackgroundBitmap.Height);
				nHideEvents = nTimeToHide / nEvents;
				nIncrementHide = BackgroundBitmap.Height / nEvents;
			}
			else
			{
				nHideEvents = 10;
				nIncrementHide = BackgroundBitmap.Height;
			}

			switch (taskbarState)
			{
				case TaskbarStates.hidden:
					taskbarState = TaskbarStates.appearing;
                    int CountInCol = (int) (WorkAreaRectangle.Bottom / (BackgroundBitmap.Height));
                    int ColNum =  (Index / CountInCol);
                    int RowNum = Index - ColNum * CountInCol;

					//SetBounds(WorkAreaRectangle.Right-BackgroundBitmap.Width-17, WorkAreaRectangle.Bottom-1, BackgroundBitmap.Width, 0);
                    SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width * (ColNum + 1) - 1,
                        WorkAreaRectangle.Bottom - BackgroundBitmap.Height * (RowNum + 1), 
                        BackgroundBitmap.Width, BackgroundBitmap.Height);
					timer.Interval = nShowEvents;
					timer.Start();
					// We Show the popup without stealing focus
					ShowWindow(this.Handle, 4);
                    Opacity = 0;
					break;

				/*case TaskbarStates.appearing:
					Refresh();
					break;

				case TaskbarStates.visible:
					timer.Stop();
					timer.Interval = nVisibleEvents;
					timer.Start();
					Refresh();
					break;*/

				/*case TaskbarStates.disappearing:
					timer.Stop();
					taskbarState = TaskbarStates.visible;
					SetBounds(WorkAreaRectangle.Right-BackgroundBitmap.Width-17, WorkAreaRectangle.Bottom-BackgroundBitmap.Height-1, BackgroundBitmap.Width, BackgroundBitmap.Height);
					timer.Interval = nVisibleEvents;
					timer.Start();
					Refresh();
					break;*/
			}
		}

        private void Taskbar_MouseWheel(object sender, MouseEventArgs e)
        {
            _Owner.ScrollCalls(e.Delta);
        }

		/// <summary>
		/// Hides the popup
		/// </summary>
		/// <returns>Nothing</returns>
        /// 
        public void MovingDown()
        {
            Index--;
            int CountInCol = (int)(WorkAreaRectangle.Bottom / (BackgroundBitmap.Height));
            int ColNum = (Index / CountInCol);
            int RowNum = Index - ColNum * CountInCol;


            if (ColNum != (Index + 1) / CountInCol) // ���� ������������ �� ���������� �������
            {

                SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width * (ColNum + 1) - 1,
                    WorkAreaRectangle.Bottom - BackgroundBitmap.Height * (RowNum + 1),
                    BackgroundBitmap.Width, BackgroundBitmap.Height);
                //Hide();
                Opacity = 0;
                taskbarState=TaskbarStates.appearing;
                timer.Start();
            }
            else
            {
                if (SourceTop == 0)
                {
                    SourceTop = Top;
                    taskbarState = TaskbarStates.moving;
                    // timer.Interval = 10;
                    timer.Start();
                }
                else
                    SourceTop += Height;
            }
        }
		public void Hidden()
        {
            taskbarState = TaskbarStates.disappearing;
            timer.Start();
        }
        public new void Hide()
		{
			if (taskbarState != TaskbarStates.hidden)
			{
				timer.Stop();
				taskbarState = TaskbarStates.hidden;

				base.Hide();
			}
		}

        public void StartTime()
        {
            EndTime = BeginTime = DateTime.Now;
            timerClock.Start();
        }

        public void StopTime()
        {
            timerClock.Stop();
            //Refresh();
        }

		/// <summary>
		/// Sets the background bitmap and its transparency color
		/// </summary>
		/// <param name="strFilename">Path of the Background Bitmap on the disk</param>
		/// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
		/// <returns>Nothing</returns>
		public void SetBackgroundBitmap(string strFilename, Color transparencyColor)
		{
			BackgroundBitmap = new Bitmap(strFilename);
			Width = BackgroundBitmap.Width;
			Height = BackgroundBitmap.Height;
			Region = BitmapToRegion(BackgroundBitmap, transparencyColor);
		}

		/// <summary>
		/// Sets the background bitmap and its transparency color
		/// </summary>
		/// <param name="image">Image/Bitmap object which represents the Background Bitmap</param>
		/// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
		/// <returns>Nothing</returns>
		public void SetBackgroundBitmap(Image image, Color transparencyColor)
		{
			BackgroundBitmap = new Bitmap(image);
			Width = BackgroundBitmap.Width;
			Height = BackgroundBitmap.Height;
			Region = BitmapToRegion(BackgroundBitmap, transparencyColor);

            //TitleRectangle = new Rectangle(0, 0, BackgroundBitmap.Width, 20);
            ContentRectangle = new Rectangle(BackgroundBitmap.Width / 3, 0, 
                BackgroundBitmap.Width - BackgroundBitmap.Width / 3, BackgroundBitmap.Height);


		}

		/// <summary>
		/// Sets the 3-State Close Button bitmap, its transparency color and its coordinates
		/// </summary>
		/// <param name="strFilename">Path of the 3-state Close button Bitmap on the disk (width must a multiple of 3)</param>
		/// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
		/// <param name="position">Location of the close button on the popup</param>
		/// <returns>Nothing</returns>
		public void SetCloseBitmap(string strFilename, Color transparencyColor, Point position)
		{
			CloseBitmap = new Bitmap(strFilename);
			CloseBitmap.MakeTransparent(transparencyColor);
			CloseBitmapSize = new Size(CloseBitmap.Width/3, CloseBitmap.Height);
			CloseBitmapLocation = position;
		}

		/// <summary>
		/// Sets the 3-State Close Button bitmap, its transparency color and its coordinates
		/// </summary>
		/// <param name="image">Image/Bitmap object which represents the 3-state Close button Bitmap (width must be a multiple of 3)</param>
		/// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
		/// /// <param name="position">Location of the close button on the popup</param>
		/// <returns>Nothing</returns>
		public void SetCloseBitmap(Image image, Color transparencyColor, Point position)
		{
			CloseBitmap = new Bitmap(image);
			CloseBitmap.MakeTransparent(transparencyColor);
			CloseBitmapSize = new Size(CloseBitmap.Width/3, CloseBitmap.Height);
			CloseBitmapLocation = position;
		}
		#endregion

		#region TaskbarNotifier Protected Methods
		protected void DrawCloseButton(Graphics grfx)
		{
			if (CloseBitmap != null)
			{	
				Rectangle rectDest = new Rectangle(CloseBitmapLocation, CloseBitmapSize);
				Rectangle rectSrc;

				if (bIsMouseOverClose)
				{
					if (bIsMouseDown)
						rectSrc = new Rectangle(new Point(CloseBitmapSize.Width*2, 0), CloseBitmapSize);
					else
						rectSrc = new Rectangle(new Point(CloseBitmapSize.Width, 0), CloseBitmapSize);
				}		
				else
					rectSrc = new Rectangle(new Point(0, 0), CloseBitmapSize);
					

				grfx.DrawImage(CloseBitmap, rectDest, rectSrc, GraphicsUnit.Pixel);
			}
		}

		protected void DrawText(Graphics grfx)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            sf.Trimming = StringTrimming.Word;							// Added Rev 002

            if (bIsMouseOverContent)
            {
                //grfx.DrawString(contentText, hoverContentFont, new SolidBrush(hoverContentColor), ContentRectangle, sf);
                if (EnableSelectionRectangle)
                {
                    ControlPaint.DrawBorder3D(grfx, /*RealContentRectangle*/new Rectangle(5, 5, BackgroundBitmap.Width - 9, BackgroundBitmap.Height - 9),
                        Border3DStyle.Etched, Border3DSide.Top | Border3DSide.Bottom | Border3DSide.Left | Border3DSide.Right);
                }

            }

            // draw middle line 
            grfx.DrawString(middleLineText, hoverContentFont, new SolidBrush(_Owner.getGroupFontColor()/*Color.WhiteSmoke/*normalContentColor*/),
                ContentRectangle, sf);

            // draw top line 
            Rectangle r1 = ContentRectangle;
            r1.Offset(0, -20);
            grfx.DrawString(topLineText, currentTopFont, new SolidBrush(_Owner.getGroupFontColor()/*Color.White*/), r1, sf);

            // draw bottom line
            if (EndTime != DateTime.MinValue && BeginTime != DateTime.MinValue)
            {
                Rectangle r = ContentRectangle;
                r.Offset(0, 20);
                TimeSpan ts = EndTime - BeginTime;
                grfx.DrawString(string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds),
                    normalContentFont, new SolidBrush(_Owner.tEndCall == DateTime.MinValue ? Color.Red : _Owner.getGroupFontColor()/*Color.White*/), r, sf);
            }
        }

        protected void CalculateMouseRectangles()
		{
			Graphics grfx = CreateGraphics();
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
			SizeF sizefTitle = grfx.MeasureString("", hoverTitleFont, TitleRectangle.Width, sf);
			SizeF sizefContent = grfx.MeasureString(topLineText.Length > middleLineText.Length ? topLineText : middleLineText,
                hoverContentFont, ContentRectangle.Width, sf);
			grfx.Dispose();

			// Added Rev 002
	        //We should check if the title size really fits inside the pre-defined title rectangle
			if (sizefTitle.Height > TitleRectangle.Height)
			{
				RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, TitleRectangle.Width , TitleRectangle.Height );
			} 
			else
			{
				RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, (int)sizefTitle.Width, (int)sizefTitle.Height);
			}
			RealTitleRectangle.Inflate(0,2);

			// Added Rev 002
			//We should check if the Content size really fits inside the pre-defined Content rectangle
			if (sizefContent.Height > ContentRectangle.Height)
			{
				RealContentRectangle = new Rectangle((ContentRectangle.Width-(int)sizefContent.Width)/2+ContentRectangle.Left, ContentRectangle.Top, (int)sizefContent.Width, ContentRectangle.Height );
			}
			else
			{
				RealContentRectangle = new Rectangle((ContentRectangle.Width-(int)sizefContent.Width)/2+ContentRectangle.Left, (ContentRectangle.Height-(int)sizefContent.Height)/2+ContentRectangle.Top, (int)sizefContent.Width, (int)sizefContent.Height);
			}
			RealContentRectangle.Inflate(0,2);
		}

		protected Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
		{
			if (bitmap == null)
				throw new ArgumentNullException("Bitmap", "Bitmap cannot be null!");

			int height = bitmap.Height;
			int width = bitmap.Width;

			GraphicsPath path = new GraphicsPath();

			for (int j=0; j<height; j++ )
				for (int i=0; i<width; i++)
				{
					if (bitmap.GetPixel(i, j) == transparencyColor)
						continue;

					int x0 = i;

					while ((i < width) && (bitmap.GetPixel(i, j) != transparencyColor))
						i++;

					path.AddRectangle(new Rectangle(x0, j, i-x0, 1));
				}

			Region region = new Region(path);
			path.Dispose();
			return region;
		}
		#endregion

		#region TaskbarNotifier Events Overrides

        protected void OnTimerClock(Object obj, EventArgs ea)
        {
            EndTime = DateTime.Now;
            Refresh();
        }
		protected void OnTimer(Object obj, EventArgs ea)
		{
			switch (taskbarState)
			{
				case TaskbarStates.appearing:
					/*if (Height < BackgroundBitmap.Height*(Index+1)+1)
						SetBounds(Left, Top-nIncrementShow ,Width, Height + nIncrementShow);
					else*/
                    if (Opacity < 1)
                    {
                        Opacity += 0.05;
                        //Refresh();
                    }
                    else
                    {
                        timer.Stop();
                        Height = BackgroundBitmap.Height;
                        timer.Interval = nVisibleEvents;
                        taskbarState = TaskbarStates.visible;
                        timer.Start();
                    }
					break;

				case TaskbarStates.visible:
					timer.Stop();
					timer.Interval = nHideEvents;
					/*// Added Rev 002
					if ((bKeepVisibleOnMouseOver && !bIsMouseOverPopup ) || (!bKeepVisibleOnMouseOver))
					{
						taskbarState = TaskbarStates.disappearing;
					} 
					//taskbarState = TaskbarStates.disappearing;		// Rev 002
					timer.Start();*/
					break;

				case TaskbarStates.disappearing:
					// Added Rev 002
					if (bReShowOnMouseOver && bIsMouseOverPopup) 
					{
						taskbarState = TaskbarStates.appearing;
					} 
					else 
					{
                        /*if (Top < (WorkAreaRectangle.Bottom + BackgroundBitmap.Height * (Index) - 1))
							SetBounds(Left, Top + nIncrementHide, Width, Height - nIncrementHide);
						else*/
                        if (Opacity > 0)
                            Opacity -= 0.05;
                        else
                            Hide();
					}
					break;

                case TaskbarStates.moving:

                    if (Top < (SourceTop + Height))
					    SetBounds(Left, Top + 5, Width, Height);
					else
                    {
                        timer.Stop();
                        timer.Interval = nHideEvents;
                        SourceTop = 0;

                        int CountInCol = (int)(WorkAreaRectangle.Bottom / (BackgroundBitmap.Height));
                        int ColNum = (Index / CountInCol);
                        int RowNum = Index - ColNum * CountInCol;


                        SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width * (ColNum + 1) - 1,
                            WorkAreaRectangle.Bottom - BackgroundBitmap.Height * (RowNum + 1),
                            BackgroundBitmap.Width, BackgroundBitmap.Height);

                        /*SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width - 1,
                        WorkAreaRectangle.Bottom - BackgroundBitmap.Height * (Index + 1),
                        BackgroundBitmap.Width, BackgroundBitmap.Height);*/
                        break;
                    }
                    break;
			}
			
		}

		protected override void OnMouseEnter(EventArgs ea)
		{
            Cursor = Cursors.Hand;
            base.OnMouseEnter(ea);
			bIsMouseOverPopup = true;
			Refresh();
		}

		protected override void OnMouseLeave(EventArgs ea)
		{
            Cursor = Cursors.Arrow;
            base.OnMouseLeave(ea);
			bIsMouseOverPopup = false;
			bIsMouseOverClose = false;
			bIsMouseOverTitle = false;
			bIsMouseOverContent = false;
			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs mea)
		{
            //Cursor = Cursors.Hand;
            base.OnMouseMove(mea);

			/*bool bContentModified = false;
			
			if ( (mea.X > CloseBitmapLocation.X) && (mea.X < CloseBitmapLocation.X + CloseBitmapSize.Width) && (mea.Y > CloseBitmapLocation.Y) && (mea.Y < CloseBitmapLocation.Y + CloseBitmapSize.Height) && CloseClickable )
			{
				if (!bIsMouseOverClose)
				{
					bIsMouseOverClose = true;
					bIsMouseOverTitle = false;
					bIsMouseOverContent = false;
					Cursor = Cursors.Hand;
					bContentModified = true;
				}
			}
			else if (RealContentRectangle.Contains(new Point(mea.X, mea.Y)) && ContentClickable)
			{
				if (!bIsMouseOverContent)
				{
					bIsMouseOverClose = false;
					bIsMouseOverTitle = false;
					bIsMouseOverContent = true;
					Cursor = Cursors.Hand;
					bContentModified = true;
				}
			}
			else if (RealTitleRectangle.Contains(new Point(mea.X, mea.Y)) && TitleClickable)
			{
				if (!bIsMouseOverTitle)
				{
					bIsMouseOverClose = false;
					bIsMouseOverTitle = true;
					bIsMouseOverContent = false;
					Cursor = Cursors.Hand;
					bContentModified = true;
				}
			}
			else
			{
				if (bIsMouseOverClose || bIsMouseOverTitle || bIsMouseOverContent)
					bContentModified = true;

				bIsMouseOverClose = false;
				bIsMouseOverTitle = false;
				bIsMouseOverContent = false;
				Cursor = Cursors.Default;
			}*/


            bIsMouseOverClose = false;
            bIsMouseOverTitle = false;
            bIsMouseOverContent = true;
            Cursor = Cursors.Hand;
            
			//if (bContentModified)
				Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs mea)
		{
			base.OnMouseDown(mea);
			
            bIsMouseDown = true;
			
			if (bIsMouseOverClose)
				Refresh();
		}

		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
			bIsMouseDown = false;

			/*if (bIsMouseOverClose)
			{
				Hide();
							
				if (CloseClick != null)
					CloseClick(this, new EventArgs());
			}
			else if (bIsMouseOverTitle)
			{
				if (TitleClick != null)
					TitleClick(this, new EventArgs());
			}
			else if (bIsMouseOverContent)
			{*/
				if (ContentClick != null && mea.Button == System.Windows.Forms.MouseButtons.Left)
					ContentClick(this, new EventArgs());
			//}
		}

		protected override void OnPaintBackground(PaintEventArgs pea)
		{
			Graphics grfx = pea.Graphics;
			grfx.PageUnit = GraphicsUnit.Pixel;
			
			Graphics offScreenGraphics;
			Bitmap offscreenBitmap;
			
			offscreenBitmap = new Bitmap(BackgroundBitmap.Width, BackgroundBitmap.Height);
			offScreenGraphics = Graphics.FromImage(offscreenBitmap);
			
			if (BackgroundBitmap != null)
			{
				offScreenGraphics.DrawImage(BackgroundBitmap, 0, 0, BackgroundBitmap.Width, BackgroundBitmap.Height);
			}
			
			DrawCloseButton(offScreenGraphics);
			DrawText(offScreenGraphics);

			grfx.DrawImage(offscreenBitmap, 0, 0);
		}
		#endregion
	}
}
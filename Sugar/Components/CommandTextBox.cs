using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sugar.Components
{

    public class CommandTextBox : TextBox
    {

        public CommandTextBox()
        {
            base.AutoSize = false; // so font is not clipped at bottom

			// here we set the style and get a white Background
			// and the overriden methods are accessed 
	//	    this.SetStyle(ControlStyles.UserPaint, true);
			this.Invalidate(true); 
        }

		protected override void OnPaint(PaintEventArgs e) {
		 // your code follows here
		//	base.OnPaint(e);
		}
 
 
		protected override void OnPaintBackground(PaintEventArgs pevent) {
			Graphics g = pevent.Graphics;
            base.OnPaintBackground(pevent);
            string drawString = "Sample Text";
            System.Drawing.Font drawFont = this.Font;
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGray);
            float x = 0F;
            float y = 0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            //drawFont.Dispose();
            drawBrush.Dispose();
            //formGraphics.Dispose();


			// your code follows here
			
		}

    }

    /*
        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        struct RECT
        {
            public int left, top, right, bottom;
        }
        struct NCCALSIZE_PARAMS
        {
            public RECT newWindow;
            public RECT oldWindow;
            public RECT clientWindow;
            IntPtr windowPos;
        }
        int clientPadding = 2;
        int actualBorderWidth = 4;
        Color borderColor = Color.Red;
        protected override void WndProc(ref Message m)
        {
            //We have to change the clientsize to make room for borders
            //if not, the border is limited in how thick it is.
            if (m.Msg == 0x83) //WM_NCCALCSIZE   
            {
                if (m.WParam == IntPtr.Zero)
                {
                    RECT rect = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));
                    rect.left += clientPadding;
                    rect.right -= clientPadding;
                    rect.top += clientPadding;
                    rect.bottom -= clientPadding;
                    Marshal.StructureToPtr(rect, m.LParam, false);
                }
                else
                {
                    NCCALSIZE_PARAMS rects = (NCCALSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALSIZE_PARAMS));
                    rects.newWindow.left += clientPadding;
                    rects.newWindow.right -= clientPadding;
                    rects.newWindow.top += clientPadding;
                    rects.newWindow.bottom -= clientPadding;
                    Marshal.StructureToPtr(rects, m.LParam, false);
                }
            }
            if (m.Msg == 0x85) //WM_NCPAINT    
            {
                IntPtr wDC = GetWindowDC(Handle);
                using (Graphics g = Graphics.FromHdc(wDC))
                {
                    DrawShadowText(g);
            //        ControlPaint.DrawBorder(g, new Rectangle(0, 0, Size.Width, Size.Height), borderColor, actualBorderWidth, ButtonBorderStyle.Solid,
            //      borderColor, actualBorderWidth, ButtonBorderStyle.Solid, borderColor, actualBorderWidth, ButtonBorderStyle.Solid,
            //      borderColor, actualBorderWidth, ButtonBorderStyle.Solid);
                }
                return;
            }
            base.WndProc(ref m);
        }

        private void DrawShadowText(Graphics g)
        {
            //System.Drawing.Graphics formGraphics = this.CreateGraphics();
            string drawString = "Sample Text";
            System.Drawing.Font drawFont = this.Font;
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGray);
            float x = 0F;
            float y = 0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            //drawFont.Dispose();
            drawBrush.Dispose();
            //formGraphics.Dispose();
        }

    }
     * */
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

using Daiz.Library;

namespace Daiz.NES.Reuben
{
    public class GuidePanel : Panel
    {
        public event EventHandler Guide1Changed;
        public event EventHandler Guide2Changed;
        public Guide Guide1 = new Guide(){ Position = 32, Visible = false};
        public Guide Guide2 = new Guide(){ Position = 64, Visible = false};

        public Orientation GuideOrientation { get; set; }
        public GuideType GuideSelected { get; set; }
        private GuideMode _GuideSnapMode = GuideMode.Free;
        public GuideMode GuideSnapMode
        {
            get { return _GuideSnapMode; }
            set
            {
                _GuideSnapMode = value;

                if (GuideOrientation == Orientation.Vertical)
                {
                    switch (value)
                    {
                        case GuideMode.Free:
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpHeight1:
                            Guide2.Position = Guide1.Position + 80;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpHeight2:
                            Guide2.Position = Guide1.Position + 96;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpHeight3:
                            Guide2.Position = Guide1.Position + 112;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpHeight4:
                            Guide2.Position = Guide1.Position + 128;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.Screen:
                            Guide2.Position = Guide1.Position + 17;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;
                    }
                }
                else if (GuideOrientation == Orientation.Horizontal)
                {
                    switch (value)
                    {
                        case GuideMode.Free:
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpLength1:
                            Guide2.Position = Guide1.Position + 80;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpLength2:
                            Guide2.Position = Guide1.Position + 96;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpLength3:
                            Guide2.Position = Guide1.Position + 176;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.JumpLength4:
                            Guide2.Position = Guide1.Position + 192;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;

                        case GuideMode.Screen:
                            Guide2.Position = Guide1.Position + 256;
                            if (Guide1Changed != null)
                            {
                                Guide1Changed(this, null);
                                Guide2Changed(this, null);
                            }
                            Invalidate();
                            break;
                    }
                }
            }
        }

        public GuidePanel()
        {
            BorderStyle = BorderStyle.Fixed3D;
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            switch (GuideSelected)
            {
                case GuideType.Guide1:
                    Guide1.Visible = !Guide1.Visible;
                    if (Guide1Changed != null)
                    {
                        Guide1Changed(this, null);
                    }
                    break;

                case GuideType.Guide2:
                    Guide2.Visible = !Guide2.Visible;
                    if (Guide2Changed != null)
                    {
                        Guide2Changed(this, null);
                    }
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            
            if (GuideSelected == GuideType.None)
            {
                switch (GuideOrientation)
                {
                    case Orientation.Horizontal:
                        if (Math.Abs(Math.Abs(e.X) - Math.Abs(Guide1.Position)) <= 4) Cursor = Cursors.SizeWE;
                        else if (Math.Abs(Math.Abs(e.X) - Math.Abs(Guide2.Position)) <= 4) Cursor = Cursors.SizeWE;
                        else Cursor = Cursors.Default;
                        break;

                    case Orientation.Vertical:
                        if (Math.Abs(Math.Abs(e.Y) - Math.Abs(Guide1.Position)) <= 4) Cursor = Cursors.SizeNS;
                        else if (Math.Abs(Math.Abs(e.Y) - Math.Abs(Guide2.Position)) <= 4) Cursor = Cursors.SizeNS;
                        else Cursor = Cursors.Default;
                        break;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                switch (GuideSelected)
                {
                    case GuideType.Guide1:
                        switch (GuideOrientation)
                        {
                            case Orientation.Horizontal:

                                Guide1.Position = (e.X / 16) * 16;

                                switch (GuideSnapMode)
                                {
                                    case GuideMode.JumpLength1:
                                        Guide2.Position = Guide1.Position + 80;
                                        break;

                                    case GuideMode.JumpLength2:
                                        Guide2.Position = Guide1.Position + 96;
                                        break;

                                    case GuideMode.JumpLength3:
                                        Guide2.Position = Guide1.Position + 176;
                                        break;

                                    case GuideMode.Screen:
                                        Guide2.Position = Guide1.Position + 256;
                                        break;

                                    case GuideMode.JumpLength4:
                                        Guide2.Position = Guide1.Position + 192;
                                        break;
                                }

                                if (Guide1Changed != null)
                                {
                                    Guide1Changed(this, null);
                                }

                                if (GuideSnapMode != GuideMode.Free)
                                {
                                    if (Guide2Changed != null)
                                    {
                                        Guide2Changed(this, null);
                                    }
                                }
                                break;

                            case Orientation.Vertical:

                                Guide1.Position = (e.Y / 16) * 16;

                                switch (GuideSnapMode)
                                {
                                    case GuideMode.JumpHeight1:
                                        Guide2.Position = Guide1.Position + 80;
                                        break;

                                    case GuideMode.JumpHeight2:
                                        Guide2.Position = Guide1.Position + 96;
                                        break;

                                    case GuideMode.JumpHeight3:
                                        Guide2.Position = Guide1.Position + 112;
                                        break;

                                    case GuideMode.Screen:
                                        Guide2.Position = Guide1.Position + 176;
                                        break;

                                    case GuideMode.JumpHeight4:
                                        Guide2.Position = Guide1.Position + 192;
                                        break;
                                }

                                if (Guide1Changed != null)
                                {
                                    Guide1Changed(this, null);
                                }

                                if (GuideSnapMode != GuideMode.Free)
                                {
                                    if (Guide2Changed != null)
                                    {
                                        Guide2Changed(this, null);
                                    }
                                }
                                break;
                        }
                        break;

                    case GuideType.Guide2:
                        switch (GuideOrientation)
                        {
                            case Orientation.Horizontal:

                                Guide2.Position = (e.X / 16) * 16;

                                switch (GuideSnapMode)
                                {
                                    case GuideMode.JumpLength1:
                                        Guide1.Position = Guide2.Position - 80;
                                        break;

                                    case GuideMode.JumpLength2:
                                        Guide1.Position = Guide2.Position - 96;
                                        break;

                                    case GuideMode.JumpLength3:
                                        Guide1.Position = Guide2.Position - 176;
                                        break;

                                    case GuideMode.Screen:
                                        Guide1.Position = Guide2.Position - 256;
                                        break;

                                    case GuideMode.JumpLength4:
                                        Guide1.Position = Guide2.Position - 192;
                                        break;
                                }

                                if (Guide2Changed != null)
                                {
                                    Guide2Changed(this, null);
                                }


                                if (Guide2Changed != null)
                                {
                                    Guide2Changed(this, null);
                                }
                                if (GuideSnapMode != GuideMode.Free)
                                {
                                    if (Guide1Changed != null)
                                    {
                                        Guide1Changed(this, null);
                                    }
                                }
                                break;

                            case Orientation.Vertical:

                                Guide2.Position = (e.Y / 16) * 16;

                                switch (GuideSnapMode)
                                {
                                    case GuideMode.JumpHeight1:
                                        Guide1.Position = Guide2.Position - 80;
                                        break;

                                    case GuideMode.JumpHeight2:
                                        Guide1.Position = Guide2.Position - 96;
                                        break;

                                    case GuideMode.JumpHeight3:
                                        Guide1.Position = Guide2.Position - 112;
                                        break;

                                    case GuideMode.Screen:
                                        Guide1.Position = Guide2.Position - 176;
                                        break;

                                    case GuideMode.JumpHeight4:
                                        Guide1.Position = Guide2.Position + 128;
                                        break;
                                }

                                if (Guide2Changed != null)
                                {
                                    Guide2Changed(this, null);
                                }

                                if (GuideSnapMode != GuideMode.Free)
                                {
                                    if (Guide1Changed != null)
                                    {
                                        Guide1Changed(this, null);
                                    }
                                }
                                break;
                        }
                        break;
                }

                Invalidate();
            }
            else
            {
                GuideSelected = GuideType.None;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (GuideOrientation)
            {
                case Orientation.Horizontal:
                    if (Math.Abs(Guide1.Position - e.X) <= 4) GuideSelected = GuideType.Guide1;
                    else if (Math.Abs(Guide2.Position - e.X) <= 4) GuideSelected = GuideType.Guide2;
                    break;

                case Orientation.Vertical:
                    if (Math.Abs(Guide1.Position - e.Y) <= 4) GuideSelected = GuideType.Guide1;
                    else if (Math.Abs(Guide2.Position - e.Y) <= 4) GuideSelected = GuideType.Guide2;
                    else Cursor = Cursors.Default;
                    break;

                default:
                    GuideSelected = GuideType.None;
                    break;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            GuideSelected = GuideType.None;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            
        }

        public Color GuideColor
        {
            get { return Guide1.Color; }
            set
            {
                Guide1.Color = value;
                Guide2.Color = value;
                Invalidate();
                if(Guide1Changed != null)
                    Guide1Changed(null, null);
                if(Guide2Changed != null)
                    Guide2Changed(null, null);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen p = new Pen(Guide1.Color);
            e.Graphics.Clear(Color.LightGray);
            switch (GuideOrientation)
            {
                case Orientation.Vertical:
                    e.Graphics.DrawLine(p, 0, Guide1.Position- 2, 16, Guide1.Position - 2);
                    e.Graphics.DrawLine(p, 0, Guide2.Position - 2, 16, Guide2.Position - 2);
                    break;

                case Orientation.Horizontal:
                    e.Graphics.DrawLine(p, Guide1.Position - 2, 0, Guide1.Position - 2, 16);
                    e.Graphics.DrawLine(p, Guide2.Position - 2, 0, Guide2.Position - 2, 16);
                    break;
            }
            p.Dispose();
        }
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public enum GuideType
    {
        None,
        Guide1,
        Guide2
    }

    public enum GuideMode
    {
        Free,
        Screen,
        JumpHeight1,
        JumpHeight2,
        JumpHeight3,
        JumpHeight4,
        JumpLength1,
        JumpLength2,
        JumpLength3,
        JumpLength4,
        JumpLength5
    }
}

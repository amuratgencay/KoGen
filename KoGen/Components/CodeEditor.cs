﻿using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoGen.Components
{
    public partial class CodeEditor : ScintillaNET.Scintilla
    {
        public CodeEditor()
        {
            InitializeComponent();
        }

        public CodeEditor(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }


        public void InitColors()
        {

            SetSelectionBackColor(true, IntToColor(0x114D9C));

        }

        public void InitSyntaxColoring()
        {

            // Configure the default style
            StyleResetDefault();
            Styles[Style.Default].Font = "Consolas";
            Styles[Style.Default].Size = 10;
            Styles[Style.Default].BackColor = IntToColor(0x212121);
            Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            StyleClearAll();

            // Configure the CPP (C#) lexer styles
            Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);
            Lexer = Lexer.Cpp;
            SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");


                }
    }
}

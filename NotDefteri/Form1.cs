﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotDefteri
{
    public partial class Form1 : Form
    {
        private int TabCount = 0; // Sekme sayısı
        public Form1()
        {
            InitializeComponent();
        }

        #region Metodlar
        #region Sekmeler
        private void AddTab() //sekme ekleme
        {
            RichTextBox Body = new RichTextBox();
            Body.Name = "Body";
            Body.Dock = DockStyle.Fill;
            Body.ContextMenuStrip = contextMenuStrip1;

            TabPage NewPage = new TabPage();
            TabCount += 1;  // Belge sayısı 1 arttırılmış oldu

            string DocumentText = "Belge " + TabCount; // yeni belge açıldığında belge 1,2... yazdırır 
            NewPage.Name = DocumentText;
            NewPage.Text = DocumentText;
            NewPage.Controls.Add(Body);

            tabControl1.TabPages.Add(NewPage);
        }
        private void RemoveTab() //sekme kaldır
        {
            if (tabControl1.TabPages.Count != 1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            } else
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                AddTab(); // boş bir sekme getirir
            }
        }
        private void RemoveAllTabs() // Tüm Sekmeleri Kaldır
        {
            foreach (TabPage Page in tabControl1.TabPages)
            {
                tabControl1.TabPages.Remove(Page);
            }
            AddTab();
        }
        private void RemoveAllTabsButThis() // Açık olan haricinde sekmeleri kaldır
        {
            foreach (TabPage Page in tabControl1.TabPages)
            {
                if (Page.Name != tabControl1.SelectedTab.Name) //Seçili olan sekmenin ismiyle döngü içerisindeki sekmelerin isimleri aynı değilse kaldır
                {
                    tabControl1.TabPages.Remove(Page);
                }
            }

        }
        #endregion
        #region SaveAndOpen (KaydetveAç)
        private void Save() // Belge kaydetme 
        {
            saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
            saveFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //Ortamdan geçerli klasörü alıp belgelerim içine yerleştiriyoruz
            saveFileDialog1.Filter = "RTF|*.rtf";
            saveFileDialog1.Title = "Save";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Length > 0)
                {
                    GetCurrentDocument.SaveFile
                        (saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    //Dosyaya kaydedilme işlemi tamamlandı
                }
            }
        }
        private void SaveAs() // Belgeyi farklı isimle kaydetmek 
        {
            saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
            saveFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.Filter = "Text Files|*.txt|C# dosyası|*.cs|Tüm Dosyalar|*.*";
            saveFileDialog1.Title = "Farklı Kaydet"; //Penceremizin başlığı
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Length > 0)
                {
                    GetCurrentDocument.SaveFile
                        (saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }
        private void Open() // Belgeyi açma
        {
            openFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "RTF|*.rtf|Text Dosyası|*.txt|Tüm Dosyalar|*.*"; //Açması için işlem 
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog1.FileName.Length > 0)
                {
                    GetCurrentDocument.LoadFile(openFileDialog1.FileName,
                        RichTextBoxStreamType.PlainText);
                }
            }
        }
        #endregion
        #region Özellikler
        private RichTextBox GetCurrentDocument // Aktif belgeyi al 
        {
            get { return (RichTextBox)tabControl1.SelectedTab.Controls["Body"]; }
        }


        #region TextFunctions   // Metinsel fonksiyonlarımızı yazacağımız yer
        private void Undo() // Geri al
        {
            GetCurrentDocument.Undo();
        }
        private void Redo() // Devam et, ileri al
        {
            GetCurrentDocument.Redo();
        }
        private void Cut()
        {
            GetCurrentDocument.Cut();
        }
        private void Copy()
        {
            GetCurrentDocument.Copy();
        }
        private void Paste()
        {
            GetCurrentDocument.Paste();
        }
        private void SelectAll()
        {
            GetCurrentDocument.SelectAll();
        }
        #endregion
        #region Font
        private void GetFontCollection() // Fontların yüklenmesi
        {
            InstalledFontCollection InsFonts = new InstalledFontCollection(); //Bilgisayardaki fontları çağırdık
            foreach (FontFamily item in InsFonts.Families) //Bilgisayarımızda kurulu olan bütün fontları alacak
            {
                toolStripComboBoxFontType.Items.Add(item.Name);
            }
            toolStripComboBoxFontType.SelectedIndex = 0;
        }
        private void PopulateFontSize() // Font ölçeklerini oluşturur
        {
            for(int i=1; i<=75; i++)
            {
                toolStripComboBoxFontSize.Items.Add(i);
            }
            toolStripComboBoxFontSize.SelectedIndex = 12;
        }
        #endregion

        #endregion

        #endregion

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void removeToolStripButton1_Click(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            Font BoldFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold);
            Font RegularFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            if (GetCurrentDocument.SelectionFont.Bold)
            {
                GetCurrentDocument.SelectionFont = RegularFont;
            }
            else
            {
                GetCurrentDocument.SelectionFont = BoldFont;
            }

        }

        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            Font ItalicFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic);
            Font RegularFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            if (GetCurrentDocument.SelectionFont.Italic)
            {
                GetCurrentDocument.SelectionFont = RegularFont;
            }
            else
            {
                GetCurrentDocument.SelectionFont = ItalicFont;
            }

        }

        private void toolStripButtonUnderLine_Click(object sender, EventArgs e)
        {
            Font UnderlineFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline);
            Font RegularFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            if (GetCurrentDocument.SelectionFont.Underline)
            {
                GetCurrentDocument.SelectionFont = RegularFont;
            }
            else
            {
                GetCurrentDocument.SelectionFont = UnderlineFont;
            }

        }

        private void toolStripButtonStrikeout_Click(object sender, EventArgs e)
        {
            Font StrikeoutFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout);
            Font RegularFont = new Font(
                GetCurrentDocument.SelectionFont.FontFamily,
                GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            if (GetCurrentDocument.SelectionFont.Strikeout)
            {
                GetCurrentDocument.SelectionFont = RegularFont;
            }
            else
            {
                GetCurrentDocument.SelectionFont = StrikeoutFont;
            }

        }

        private void toolStripButtonUpper_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToUpper(); //Hepsini büyük harf yapacak
        }

        private void toolStripButtonLower_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToLower(); // Hepsini küçük harfe dönüştürür 
        }

        private void toolStripButtonIncrease_Click(object sender, EventArgs e)
        {
            float NewFontSize = GetCurrentDocument.SelectionFont.SizeInPoints + 2;

            Font NewSize = new Font(GetCurrentDocument.SelectionFont.Name,
                NewFontSize, GetCurrentDocument.SelectionFont.Style);
            GetCurrentDocument.SelectionFont = NewSize;
        }

        private void toolStripButtonDecrease_Click(object sender, EventArgs e)
        {
            float NewFontSize = GetCurrentDocument.SelectionFont.SizeInPoints - 2;

            Font NewSize = new Font(GetCurrentDocument.SelectionFont.Name,
                NewFontSize, GetCurrentDocument.SelectionFont.Style);
            GetCurrentDocument.SelectionFont = NewSize;
        }

        private void toolStripButtonFontColor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetCurrentDocument.SelectionColor = colorDialog1.Color;
            }
        }

        private void toolStripMenuItemGreen_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.Lime;
        }

        private void toolStripMenuItemOrange_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.DarkOrange;
        }

        private void toolStripMenuItemYellow_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.Gold;
        }

        private void toolStripComboBoxFontType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font NewFont = new Font(toolStripComboBoxFontType.SelectedItem.ToString(),
                GetCurrentDocument.SelectionFont.Size,
                GetCurrentDocument.SelectionFont.Style);
            GetCurrentDocument.SelectionFont = NewFont;
        }

        private void toolStripComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            float NewSize;
            float.TryParse(toolStripComboBoxFontSize.SelectedItem.ToString(), out NewSize);
            Font NewFont = new Font(
                GetCurrentDocument.SelectionFont.Name, NewSize, 
                GetCurrentDocument.SelectionFont.Style);
            GetCurrentDocument.SelectionFont = NewFont;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            RemoveAllTabs();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            RemoveAllTabsButThis();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddTab();
            GetFontCollection();
            PopulateFontSize();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(GetCurrentDocument.Text.Length > 0)
            {
                toolStripStatusLabel1.Text = "Toplam Karakte Sayısı = " +
                    GetCurrentDocument.Text.Length.ToString();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButtonBackColor_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_LeftToolStripPanel_Click(object sender, EventArgs e)
        {

        }
    }
}

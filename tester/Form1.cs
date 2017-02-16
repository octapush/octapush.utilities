#region Build Information
// tester : Form1.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-10
// CratedTime  : 20:37
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using octapush.SPProcessor;
using octapush.SPProcessor.Models;
using octapush.Utilities.DbHelper;
using octapush.Utilities.Encryption;
using octapush.Utilities.Enums;
using octapush.Utilities.Extensions;
using octapush.Utilities.Logger;
using octapush.Utilities.Types;

#endregion

namespace tester
{
    public partial class Form1 : Form
    {
        private const string CsOleDb =
            "Provider=SQLNCLI10;Data Source=.;Integrated Security=SSPI;Initial Catalog=E_TAX_MAPPER";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = Enum.GetName(typeof (EnumLogStatus), EnumLogStatus.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = EnumLogStatus.Information.GetDescription();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new LoggerEngine(@"H:\temp\LoggerEngine", "TESTER", true)
                .Write("test");

            tab.SelectTab(1);
            txt.Text = @"DONE";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new LoggerEngine(@"H:\temp\LoggerEngine", "TESTER", true)
                .Write("test", EnumLogStatus.Information);

            tab.SelectTab(1);
            txt.Text = @"DONE";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("test throw");
            }
            catch (Exception ex)
            {
                new LoggerEngine(@"H:\temp\LoggerEngine", "TESTER", true)
                    .Write(ex);
            }
            finally
            {
                MessageBox.Show(@"Done");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = @"MyName is {{name}}, was born at {{bornLocation}} {{bornDate}}, {{name}}"
                .BindData(new Dictionary<string, object>
                {
                    {"name", "Fadhly Permata"},
                    {"bornLocation", "Serang"},
                    {"bornDate", "13-05-1985"}
                });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "Fadhly Permata\n".Repeat(3);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tab.SelectTab(0);
            dgv.DataSource = "SELECT TOP 10 * FROM MST_BUSA WHERE BusACode = '<:BusACode:>'"
                .BindData(new BusaModel
                {
                    BusACode = "L001"
                }, "<:", ":>")
                .ExecQuery(CsOleDb);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "SELECT TOP 10 * FROM MST_BUSA".ExecScalar(CsOleDb).ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "SELECT TOP 2 BusAName FROM MST_BUSA"
                .ExecQuery(CsOleDb)
                .ToJson(new JsonSetting
                {
                    ShowAttribute = true,
                    AttributeTitle = "Schema",
                    DataTitle = "Rows"
                });
        }

        private void button13_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "SELECT TOP 10 * FROM MST_BUSA"
                .ExecQuery(CsOleDb).ToCsv(new CsvSetting
                {
                    ShowHeader = true
                });
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "";

            var tlf = "SELECT TOP 10 * FROM MST_BUSA"
                .ExecQuery(CsOleDb)
                .ToListOf<BusaModel>();

            foreach (var busaModel in tlf)
                txt.Text += string.Format("{0}{1}", (txt.Text.Length > 0 ? "," : ""), busaModel.BusAName);
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "SELECT TOP 10 * FROM MST_BUSA"
                .ExecQuery(CsOleDb)
                .ToXml(new XmlSetting
                {
                    AttributesTitle = "ATTR",
                    DataTitle = "DATA",
                    DataWrapperTitle = "PROPS",
                    RootTitle = "MY_DB_ROOT",
                    ShowAttribute = true,
                    ShowWrapperOnSingleRow = false
                });
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = @"The {{BusAName}} has been bancrupt last years ago.".BindData(new BusaModel
            {
                BusACode = "Code PT",
                BusAName = "PT. ABC"
            });
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);

            try
            {
                throw new Exception("Test error");
            }
            catch (Exception ex)
            {
                txt.Text = ex.ToJson();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            tab.SelectTab(0);
            dgv.DataSource = new List<BusaModel>
            {
                new BusaModel {BusAName = "PT. ABC"},
                new BusaModel {BusAName = "PT. BCD"},
                new BusaModel {BusAName = "PT. CDE"},
            }
                .ToDataTable();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // since we can not see non query. so, we does not do anything.
        }

        private void button18_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "tEsT StRiNg".ChangeCase(EnumStringCase.LowerCase);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "tEsT StRiNg".ChangeCase(EnumStringCase.UpperCase);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "tEsT StRiNg. tEsT StRiNg".ToLower().ChangeCase(EnumStringCase.SentenceCapitalize);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "tEsT StRiNg".ToLower().ChangeCase(EnumStringCase.FirstCharacterUpperCase);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            txt.Text = "tEsT StRiNg".ToLower().ChangeCase(EnumStringCase.TitleCase);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            const string encryptedText = "kU6vEoiCMjHE8QrG2cyhWA==";
            txt.Text = @"
Encrypted : {{encrypted}}
Decrypted: {{decrypted}}
"
                .BindData(new Dictionary<string, object>
                {
                    {"decrypted", encryptedText.Aes256_Decrypt("p@55w0rd")},
                    {"encrypted", encryptedText}
                });
        }

        private void button26_Click(object sender, EventArgs e)
        {
            tab.SelectTab(1);
            const string normalText = "FADHLY PERMATA";
            txt.Text = @"
Normal Text : {{normal}}
Encrypted: {{encrypted}}
"
                .BindData(new Dictionary<string, object>
                {
                    {"normal", normalText},
                    {"encrypted", normalText.Aes256_Encrypt("p@55w0rd")}
                });
        }

        private void button24_Click(object sender, EventArgs e)
        {
            var spApp = new ApplicationProcessor(ConfigurationManager.AppSettings["SPStoragePath"]);
            MessageBox.Show(
                spApp.Insert(new ApplicationsModel
                {
                    Id = new Guid(),
                    Name = "Test App",
                    ConnectionString = "Contoh ConnectionString",
                    IsActive = true
                }).ToString()
                );

            dgv.DataSource = spApp.Gets().ToDataTable();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var spApp = new ApplicationProcessor(ConfigurationManager.AppSettings["SPStoragePath"]);
            MessageBox.Show(
                spApp.Update(spApp.Gets().FirstOrDefault().Id, new ApplicationsModel
                {
                    Name = "Update Test App"
                }).ToString()
                );

            dgv.DataSource = spApp.Gets().ToDataTable();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            var spApp = new ApplicationProcessor(ConfigurationManager.AppSettings["SPStoragePath"]);
            MessageBox.Show(
                spApp.Delete(spApp.Gets().FirstOrDefault().Id).ToString()
                );

            dgv.DataSource = spApp.Gets().ToDataTable();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            dgv.DataSource = new ApplicationProcessor(ConfigurationManager.AppSettings["SPStoragePath"])
                .Gets()
                .ToDataTable();
        }
    }

    public class BusaModel
    {
        public Guid Id { get; set; }
        public string CompanyCode { get; set; }
        public string BusACode { get; set; }
        public string BusAName { get; set; }
    }
}
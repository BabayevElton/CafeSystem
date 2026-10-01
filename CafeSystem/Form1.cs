using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CafeSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // --- ŞƏKİL KLİK FUNKSİYALARI ---

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Tort - 4.00 AZN");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Cola - 2.00 AZN");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Portağal şirəsi - 3.00 AZN");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Pizza - 7.00 AZN");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Sendviç - 4.00 AZN");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Hamburger - 5.00 AZN");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Peçenye - 3.00 AZN");
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Hot-Dog - 4.00 AZN");
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            lstSebet.Items.Add("Keks - 3.00 AZN");
        }


   
        private void btnHesabla_Click(object sender, EventArgs e)
        {
            string hesabMetni = txtHesab.Text.Replace(',', '.').Trim();
            string meblegMetni = txtMebleg.Text.Replace(',', '.').Trim();

            if (string.IsNullOrWhiteSpace(hesabMetni) ||
                !double.TryParse(
                    hesabMetni,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out double hesab))
            {
                MessageBox.Show(
                    "Lütfən əvvəlcə 'Yekun Hesab' düyməsinə basın!",
                    "Xəbərdarlıq",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(meblegMetni) ||
                !double.TryParse(
                    meblegMetni,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out double mebleg) ||
                mebleg < 0)
            {
                MessageBox.Show(
                    "Lütfən 'Məbləğ' xanasına keçərli ədəd daxil edin!",
                    "Xəbərdarlıq",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (mebleg < hesab)
            {
                MessageBox.Show(
                    "Daxil edilən məbləğ hesabdan azdır!",
                    "Xəbərdarlıq",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtQaliq.Clear();
            }
            else
            {
                double qaliq = mebleg - hesab;

                txtQaliq.Text =
                    qaliq.ToString("0.00", CultureInfo.InvariantCulture);
            }
        }


        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtMebleg.Clear();
            txtQaliq.Clear();
        }


        private void btnSebetdenSil_Click(object sender, EventArgs e)
        {
            if (lstSebet.SelectedIndex != -1)
            {
                string silinenYemek =
                    lstSebet.SelectedItem?.ToString() ?? "";

                lstSebet.Items.RemoveAt(lstSebet.SelectedIndex);

                MessageBox.Show(
                    silinenYemek + " səbətdən silindi",
                    "Məlumat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (lstSebet.Items.Count > 0)
            {
                string silinenYemek =
                    lstSebet.Items[lstSebet.Items.Count - 1]?.ToString() ?? "";

                lstSebet.Items.RemoveAt(lstSebet.Items.Count - 1);

                MessageBox.Show(
                    silinenYemek + " səbətdən silindi",
                    "Məlumat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Səbətdə silinəsi yemək yoxdur!",
                    "Xəbərdarlıq",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }


  
        private void btnYenile_Click(object sender, EventArgs e)
        {
            DialogResult cavab = MessageBox.Show(
                "Xanalar sıfırlansınmı?",
                "Təsdiq",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (cavab == DialogResult.Yes)
            {
                lstSebet.Items.Clear();

                txtQaliq.Clear();

                txtMebleg.Clear();

                txtHesab.Clear();

        
            }
        }


     
        private void btnYekunHesab_Click(object sender, EventArgs e)
        {
            var setirler = lstSebet.Items
                .Cast<object>()
                .Select(x => x?.ToString() ?? "")
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            if (setirler.Count == 0)
            {
                MessageBox.Show(
                    "Səbətdə yemək yoxdur!",
                    "Xəbərdarlıq",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                double umumiHesab = 0;

                foreach (var line in setirler)
                {
                    string[] hisseler = line.Split('-');

                    if (hisseler.Length > 1)
                    {
                        string qiymetStr =
                            hisseler[1]
                            .Replace("AZN", "")
                            .Trim()
                            .Replace(',', '.');

                        if (double.TryParse(
                            qiymetStr,
                            NumberStyles.Any,
                            CultureInfo.InvariantCulture,
                            out double qiymet))
                        {
                            umumiHesab += qiymet;
                        }
                    }
                }

                txtHesab.Text =
                    umumiHesab.ToString(
                        "0.00",
                        CultureInfo.InvariantCulture);
            }
        }


       
        private void txtHesab_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
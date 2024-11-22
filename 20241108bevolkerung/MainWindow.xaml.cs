using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20241108bevolkerung
{
    public partial class MainWindow : Window
    {
        List<Allampolgar> lakossag;
        public MainWindow()
        {
            InitializeComponent();
            //2.feladat
            var sr = new StreamReader(path: @"..\..\..\src\bevölkerung.txt", encoding: Encoding.UTF8);
            _ = sr.ReadLine();
            lakossag = new List<Allampolgar>();
            while (!sr.EndOfStream)
            {
                lakossag.Add(new Allampolgar(sr.ReadLine()));
            }
            for (int i = 1; i < 41; i++)
            {
                valasztoCombobox.Items.Add(i + ".");
            }

            MegoldasTeljes.ItemsSource = lakossag;
        }
        private void vezerlokTorlese()
        {
            MegoldasLista.Items.Clear();
            MegoldasMondatos.Content = "";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var methodName = $"Feladat{valasztoCombobox.SelectedIndex}";
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(this, null);
        }

        private void Feladat0()
        {
            vezerlokTorlese();
            MegoldasMondatos.Content = $"Legmagasabb éves nettó jövedelem: {lakossag.Max(e => e.NettoJovedelem)}";
        }

        private void Feladat1()
        {
            vezerlokTorlese();
            MegoldasMondatos.Content = $"Átlagos éves nettó jövedelem: {Math.Round(lakossag.Average(e => e.NettoJovedelem), 2)}";
        }

        private void Feladat2()
        {
            vezerlokTorlese();
            var csoportositas = lakossag.GroupBy(e => e.Tartomany);
            foreach (var csoport in csoportositas)
            {
                MegoldasLista.Items.Add($"Terület: {csoport.Key} | lakossag száma: {csoport.Count()}");
            }
        }

        private void Feladat3()
        {
            vezerlokTorlese();
            var szures = lakossag
                .Where(e => e.Nemzetiseg == "angolai")
                .ToList();
            foreach (var elem in szures)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }
        }

        private void Feladat4()
        {
            vezerlokTorlese();
            var legkisebbEletkor = lakossag.Min(e => e.Eletkor);
            var fiatalok = lakossag
                .Where(e => e.Eletkor == legkisebbEletkor)
                .ToList();
            foreach (var elem in fiatalok)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }
        }

        private void Feladat5()
        {
            vezerlokTorlese();
            var nemDohanyzok = lakossag
                .Where(e => !e.Dohanyzik)
                .ToList();
            foreach (var elem in nemDohanyzok)
            {
                MegoldasLista.Items.Add($"Azonosító: {elem.Id}, Jövedelem: {elem.HaviNetto}");
            }
        }

        private void Feladat6()
        {
            vezerlokTorlese();
            var bajorok = lakossag
                .Where(e => e.Tartomany == "Bajorország" && e.NettoJovedelem > 30000)
                .OrderBy(e => e.IskolaiVegzettseg)
                .ToList();
            foreach (var elem in bajorok)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }
        }

        private void Feladat7()
        {
            vezerlokTorlese();
            var ferfiak = lakossag
                .Where(e => e.Nem == "férfi")
                .ToList();
            foreach (var elem in ferfiak)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }
        }

        private void Feladat8()
        {
            vezerlokTorlese();
            var bajorNok = lakossag
                .Where(e => e.Nem == "nő" && e.Tartomany == "Bajorország")
                .ToList();
            foreach (var elem in bajorNok)
            {
                MegoldasLista.Items.Add(elem.ToString(false));
            }
        }

        private void Feladat9()
        {
            vezerlokTorlese();
            var gazdagNemDohanyzok = lakossag
                .Where(e => !e.Dohanyzik)
                .OrderByDescending(e => e.NettoJovedelem)
                .Take(10)
                .ToList();
            foreach (var elem in gazdagNemDohanyzok)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }
        }


        private void Feladat10()
        {
            vezerlokTorlese();
            var legIdosebbOt = lakossag
                .OrderByDescending(x => x.Eletkor)
                .Take(5)
                .ToList();
            foreach (var elem in legIdosebbOt)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }
        }

        private void Feladat11()
        {
            vezerlokTorlese();
            var nemetCsoportositas = lakossag
                        .Where(x => x.Nemzetiseg == "német")
                        .GroupBy(x => x.Nepcsoport).ToList();
            for (int b = 0; b < nemetCsoportositas.Count; b++)
            {
                MegoldasLista.Items.Add($"{nemetCsoportositas[b].Key}");
                foreach (var elem in nemetCsoportositas[b])
                {
                    MegoldasLista.Items.Add($"\t{(elem.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó")} {elem.PolitikaiNezet}");
                }
            }
        }

        private void Feladat12()
        {
            vezerlokTorlese();
            MegoldasLista.Items.Add($"Éves átlagos sörfogyasztás férfiak körében: {lakossag.Average(x => x.SorFogyasztasEvente)} Liter");
        }

        private void Feladat13()
        {
            vezerlokTorlese();
            var iskolaiVegzettsegSzerint = lakossag.OrderByDescending(x => x.IskolaiVegzettseg).ToList();

            foreach (var elem in iskolaiVegzettsegSzerint)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }
        }

        private void Feladat14()
        {
            vezerlokTorlese();
            var legMagasabbNetto = lakossag
                .OrderByDescending(x => x.NettoJovedelem)
                .Take(3)
                .ToList();

            var legAlacsonyabbNetto = lakossag
                .OrderBy(x => x.NettoJovedelem)
                .Take(3)
                .ToList();

            foreach (var elem in legMagasabbNetto)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }

            foreach (var elem in legAlacsonyabbNetto)
            {
                MegoldasLista.Items.Add(elem.ToString());
            }

        }

        private void Feladat15()
        {
            vezerlokTorlese();
            double hanySzavazo = lakossag.Count(x => x.AktivSzavazo);
            double osszesAllampolgar = lakossag.Count();
            MegoldasLista.Items.Add($"Az állampolgárok {Math.Round((hanySzavazo / osszesAllampolgar) * 100, 2)} százaléka aktív szavazó");
        }

        private void Feladat16()
        {
            vezerlokTorlese();

        }

        private void Feladat17()
        {
            vezerlokTorlese();
        }

        private void Feladat18()
        {
            vezerlokTorlese();
        }
    }
}
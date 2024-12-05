using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            var sr = new StreamReader(path: @"..\..\..\src\bevölkerung.txt", encoding: Encoding.UTF8);
            _ = sr.ReadLine();
            lakossag = new List<Allampolgar>();
            while (!sr.EndOfStream)
            {
                lakossag.Add(new Allampolgar(sr.ReadLine()));
            }
            for (int i = 1; i <= 45; i++)
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
            var methodName = $"Feladat{valasztoCombobox.SelectedIndex + 1}";
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(this, null);
        }

        private void Feladat1()
        {
            vezerlokTorlese();
            MegoldasMondatos.Content = $"Legmagasabb éves nettó jövedelem: {lakossag.Max(e => e.NettoJovedelem)}";
        }

        private void Feladat2()
        {
            vezerlokTorlese();
            MegoldasMondatos.Content = $"Átlagos éves nettó jövedelem: {Math.Round(lakossag.Average(e => e.NettoJovedelem), 2)}";
        }

        private void Feladat3()
        {
            vezerlokTorlese();
            var csoportositas = lakossag.GroupBy(e => e.Tartomany);
            foreach (var csoport in csoportositas)
            {
                MegoldasLista.Items.Add($"Terület: {csoport.Key} | lakossag száma: {csoport.Count()}");
            }
        }

        private void Feladat4()
        {
            vezerlokTorlese();
            var szures = lakossag
                .Where(e => e.Nemzetiseg == "angolai")
                .ToList();
            foreach (var elem in szures)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }
        }

        private void Feladat5()
        {
            vezerlokTorlese();
            var legkisebbEletkor = lakossag.Min(e => e.Eletkor);
            var fiatalok = lakossag
                .Where(e => e.Eletkor == legkisebbEletkor)
                .ToList();
            foreach (var elem in fiatalok)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }
        }

        private void Feladat6()
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

        private void Feladat7()
        {
            vezerlokTorlese();
            var bajorok = lakossag
                .Where(e => e.Tartomany == "Bajorország" && e.NettoJovedelem > 30000)
                .OrderBy(e => e.IskolaiVegzettseg)
                .ToList();
            foreach (var elem in bajorok)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }
        }

        private void Feladat8()
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

        private void Feladat9()
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

        private void Feladat10()
        {
            vezerlokTorlese();
            var gazdagNemDohanyzok = lakossag
                .Where(e => !e.Dohanyzik)
                .OrderByDescending(e => e.NettoJovedelem)
                .Take(10)
                .ToList();
            foreach (var elem in gazdagNemDohanyzok)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }
        }


        private void Feladat11()
        {
            vezerlokTorlese();
            var legIdosebbOt = lakossag
                .OrderByDescending(x => x.Eletkor)
                .Take(5)
                .ToList();
            foreach (var elem in legIdosebbOt)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }
        }

        private void Feladat12()
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

        private void Feladat13()
        {
            vezerlokTorlese();
            MegoldasLista.Items.Add($"Éves átlagos sörfogyasztás férfiak körében: {lakossag.Average(x => x.SorFogyasztasEvente)} Liter");
        }

        private void Feladat14()
        {
            vezerlokTorlese();
            var iskolaiVegzettsegSzerint = lakossag.OrderByDescending(x => x.IskolaiVegzettseg).ToList();

            foreach (var elem in iskolaiVegzettsegSzerint)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }
        }

        private void Feladat15()
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
                MegoldasLista.Items.Add(elem.ToString(true));
            }

            foreach (var elem in legAlacsonyabbNetto)
            {
                MegoldasLista.Items.Add(elem.ToString(true));
            }

        }

        private void Feladat16()
        {
            vezerlokTorlese();
            double hanySzavazo = lakossag.Count(x => x.AktivSzavazo);
            double osszesAllampolgar = lakossag.Count();
            MegoldasLista.Items.Add($"Az állampolgárok {(hanySzavazo / osszesAllampolgar) * 100:F2} százaléka aktív szavazó");
        }

        private void Feladat17()
        {
            vezerlokTorlese();
            var csoportositottAdatok = lakossag
            .Where(allampolgar => allampolgar.AktivSzavazo)
            .GroupBy(allampolgar => allampolgar.Tartomany)
            .OrderBy(group => group.Key);

            foreach (var csoport in csoportositottAdatok)
            {
                MegoldasLista.Items.Add($"Tartomány: {csoport.Key}");

                foreach (var allampolgar in csoport)
                {
                    MegoldasLista.Items.Add(allampolgar.ToString(true));
                }
            }
        }

        private void Feladat18()
        {
            vezerlokTorlese();
            MegoldasLista.Items.Add(lakossag.Average(x => x.Eletkor));
        }

        private void Feladat19()
        {
            vezerlokTorlese();
            var csoportositas = lakossag
            .GroupBy(allampolgar => allampolgar.Tartomany)
            .Select(group => new
            {
                Tartomany = group.Key,
                AtlagJovedelem = group.Average(allampolgar => allampolgar.NettoJovedelem),
                LakossagSzama = group.Count()
            })
            .ToList();

            var legmagasabbAtlag = csoportositas.Max(csoport => csoport.AtlagJovedelem);

            var legjobbak = csoportositas
                .Where(csoport => csoport.AtlagJovedelem == legmagasabbAtlag)
                .OrderByDescending(csoport => csoport.LakossagSzama)
                .First();

            MegoldasLista.Items.Add($"Tartomány: {legjobbak.Tartomany}");
            MegoldasLista.Items.Add($"Átlagos éves nettó jövedelem: {legjobbak.AtlagJovedelem:F2} Ft");
            MegoldasLista.Items.Add($"Lakosság száma: {legjobbak.LakossagSzama}");

        }

        private void Feladat20()
        {
            vezerlokTorlese();
            double atlagSuly = lakossag.Average(allampolgar => allampolgar.Suly);

            var rendezettSulyok = lakossag
                .Select(allampolgar => allampolgar.Suly)
                .OrderBy(suly => suly)
                .ToList();

            double medianSuly;
            int n = rendezettSulyok.Count;
            if (n % 2 == 1)
            {
                medianSuly = rendezettSulyok[n / 2];
            }
            else
            {
                medianSuly = (rendezettSulyok[(n / 2) - 1] + rendezettSulyok[n / 2]) / 2.0;
            }

            MegoldasLista.Items.Add($"Átlagos súly: {atlagSuly:F2} kg");
            MegoldasLista.Items.Add($"Medián súly: {medianSuly:F2} kg");
        }

        private void Feladat21()
        {
            vezerlokTorlese();
            var nemSzavazoSorFogyasztas = lakossag
                .Where(x => x.AktivSzavazo == false)
                .Average(x => x.SorFogyasztasEvente);
            var SzavazoSorFogyasztas = lakossag
                .Where(x => x.AktivSzavazo == true)
                .Average(x => x.SorFogyasztasEvente);
            var dontes = nemSzavazoSorFogyasztas > SzavazoSorFogyasztas ? "Nem szavazó" : "Szavazó";

            MegoldasLista.Items.Add($"Nem szavazó átlag sörfogyasztás: {nemSzavazoSorFogyasztas}, szavazó sörfogyasztás: {SzavazoSorFogyasztas}, tehát {dontes} fogyaszt több sört");

        }

        private void Feladat22()
        {
            vezerlokTorlese();
            var ferfiMagassag = lakossag
                .Where(x => x.Nem == "férfi")
                .Average(x => x.Magassag);

            var noMagassag = lakossag
                .Where(x => x.Nem == "nő")
                .Average(x => x.Magassag);

            MegoldasLista.Items.Add($"Átlagos női magasság: {noMagassag:F0}, átlagos férfi magasság: {ferfiMagassag:F0}");
        }

        private void Feladat23()
        {
            vezerlokTorlese();
            var nepcsoportStatisztika = lakossag
                .GroupBy(x => x.Nepcsoport)
                .Where(group => !string.IsNullOrEmpty(group.Key))
                .Select(group => new
                {
                    Nepcsoport = group.Key,
                    TagokSzama = group.Count(),
                    AtlagEletkor = group.Average(x => x.Eletkor)
                })
                .ToList();
            var nepcsoportLegtobb = nepcsoportStatisztika
                .MaxBy(x => x.TagokSzama);
            var legjobb = nepcsoportStatisztika
                .Where(x => x.TagokSzama == nepcsoportLegtobb.TagokSzama)
                .MaxBy(x => x.AtlagEletkor);
            MegoldasLista.Items.Add($"{legjobb.Nepcsoport} népcsoportba tartoznak a legtöbben, {legjobb.TagokSzama} fővel.");
            MegoldasLista.Items.Add($"Az átlagos életkor: {legjobb.AtlagEletkor:F2} év.");
        }

        private void Feladat24()
        {
            vezerlokTorlese();
            var nemDohanyzoBevetel = lakossag
                .Where(x => x.Dohanyzik == false)
                .Average(x => x.NettoJovedelem);
            var dohanyzoBevetel = lakossag
                .Where(x => x.Dohanyzik == true)
                .Average(x => x.NettoJovedelem);
            MegoldasMondatos.Content = $"Nemdohányzó jövedelem: {nemDohanyzoBevetel:F0}, dohányzó jövedelem: {dohanyzoBevetel:F0}";
        }

        private void Feladat25()
        {
            vezerlokTorlese();
            var atlagosKrumpliFogyasztas = lakossag.Average(x => x.KrumpliFogyasztasEvente);

            var atlagFelettiFogyasztas = lakossag
                .Where(x => x.KrumpliFogyasztasEvente > atlagosKrumpliFogyasztas)
                .Take(15)
                .ToList();

            MegoldasLista.Items.Add($"Átlagos krumpli fogyasztás: {atlagosKrumpliFogyasztas:F0} db évente");
            foreach (var item in atlagFelettiFogyasztas)
            {
                MegoldasLista.Items.Add(item.ToString(true));
            }
        }

        private void Feladat26()
        {
            vezerlokTorlese();
            var tartomanyAtlagok = lakossag
                .GroupBy(x => x.Tartomany)
                .Select(group => new
                {
                    Tartomany = group.Key,
                    AtlagEletkor = group.Average(x => x.Eletkor)
                }
                )
                .ToList();

            foreach (var item in tartomanyAtlagok)
            {
                MegoldasLista.Items.Add($"{item.Tartomany}: {item.AtlagEletkor:F0} év");
            }

        }

        private void Feladat27()
        {
            vezerlokTorlese();
            var idosebbek = lakossag
                .Where(x => x.Eletkor > 50)
                .Select(x => new
                {
                    x.Id,
                    x.Nem,
                    x.SzuletesiEv,
                    x.Suly,
                    x.Magassag
                })
                .ToList();

            foreach (var szemely in idosebbek.Take(5))
            {
                MegoldasLista.Items.Add(szemely);
            }

            MegoldasLista.Items.Add($"Összesen {idosebbek.Count} állampolgár 50 évnél idősebb.");
        }

        private void Feladat28()
        {
            vezerlokTorlese();
            var dohanyzoNok = lakossag
                .Where(x => x.Nem == "nő" && x.Dohanyzik)
                .ToList();

            foreach (var no in dohanyzoNok)
            {
                MegoldasLista.Items.Add(no.ToString(false));
            }

            if (dohanyzoNok.Any())
            {
                var maxJovedelem = dohanyzoNok.Max(x => x.NettoJovedelem);
                MegoldasLista.Items.Add($"A dohányzó nők közül a legmagasabb nettó éves jövedelem: {maxJovedelem} Ft.");
            }
            else
            {
                MegoldasLista.Items.Add("Nincsenek dohányzó nők az adatok között.");
            }
        }

        private void Feladat29()
        {
            vezerlokTorlese();
            var legNagyobbSorFogyaszto = lakossag
                .GroupBy(x => x.Tartomany)
                .Select(group => new
                {
                    Tartomany = group.Key,
                    SorFogyasztoLegjobb = group.MaxBy(sf => sf.SorFogyasztasEvente)
                })
                .ToList();
            foreach (var item in legNagyobbSorFogyaszto)
            {
                MegoldasLista.Items.Add($"Tartomány: {item.Tartomany}, ID: {item.SorFogyasztoLegjobb.Id}, Sörfogyasztás:  {item.SorFogyasztoLegjobb.SorFogyasztasEvente}");
            }
        }

        private void Feladat30()
        {
            vezerlokTorlese();
            var legidosebbNo = lakossag.Where(x => x.Nem == "nő").MaxBy(x => x.Eletkor);
            var legidosebbFerfi = lakossag.Where(x => x.Nem == "férfi").MaxBy(x => x.Eletkor);

            MegoldasLista.Items.Add(legidosebbNo.ToString(true));
            MegoldasLista.Items.Add(legidosebbFerfi.ToString(true));
        }

        private void Feladat31()
        {
            vezerlokTorlese();
            var kulonbozoNemzetisegek = lakossag
                .Select(x => x.Nemzetiseg)
                .Distinct()
                .OrderByDescending(nemzetiseg => nemzetiseg)
                .ToList();

            foreach (var item in kulonbozoNemzetisegek)
            {
                MegoldasLista.Items.Add(item);
            }
        }

        private void Feladat32()
        {
            vezerlokTorlese();
            var tartomanyokLakosokSzerint = lakossag
                .GroupBy(x => x.Tartomany)
                .OrderBy(group => group.Count())
                .Select(group => group.Key)
                .ToList();

            foreach (var item in tartomanyokLakosokSzerint)
            {
                MegoldasLista.Items.Add(item);
            }
        }

        private void Feladat33()
        {
            vezerlokTorlese();
            var haromLegmagasabbJovedelmu = lakossag
                .OrderByDescending(x => x.NettoJovedelem)
                .Take(3)
                .ToList();

            foreach (var item in haromLegmagasabbJovedelmu)
            {
                MegoldasLista.Items.Add($"Azonosító: {item.Id}, Jövedelem: {item.NettoJovedelem}");
            }
        }

        private void Feladat34()
        {
            vezerlokTorlese();
            var atlagosSulyKrumpliAlapjan = lakossag
                .Where(x => x.KrumpliFogyasztasEvente > 55)
                .Average(x => x.Suly);

            MegoldasMondatos.Content = $"{atlagosSulyKrumpliAlapjan:F1} kg átlagosan a súlyuk";
        }

        private void Feladat35()
        {
            vezerlokTorlese();
            var atlagosEletkorTartomany = lakossag
                .GroupBy(x => x.Tartomany)
                .Select(group => new
                {
                    TartomanyNeve = group.Key,
                    MinimalisEletkor = group.Min(x => x.Eletkor)
                })
                .ToList();

            foreach (var item in atlagosEletkorTartomany)
            {
                MegoldasLista.Items.Add($"{item.TartomanyNeve}, {item.MinimalisEletkor}");
            }
        }

        private void Feladat36()
        {
            vezerlokTorlese();
            var nemzetisegTartomanyParok = lakossag
            .Select(x => new { x.Nemzetiseg, x.Tartomany })
            .Distinct()
            .OrderBy(pair => pair.Nemzetiseg)
            .ThenBy(pair => pair.Tartomany)
            .ToList();

            foreach (var item in nemzetisegTartomanyParok)
            {
                MegoldasLista.Items.Add($"{item.Nemzetiseg} - {item.Tartomany}");
            }
        }

        private void Feladat37()
        {
            vezerlokTorlese();
            var atlagJovedelem = lakossag.Average(x => x.NettoJovedelem);
            var atlagFelettiJovedelem = lakossag
                .Where(x => x.NettoJovedelem > atlagJovedelem)
                .ToList();

            MegoldasLista.Items.Add($"Átlag jövedelem: {atlagJovedelem:F0}, leszűrt állampolgárok száma: {atlagFelettiJovedelem.Count()}");
            foreach (var item in atlagFelettiJovedelem)
            {
                MegoldasLista.Items.Add($"{item.ToString(false)}");
            }
        }

        private void Feladat38()
        {
            vezerlokTorlese();
            MegoldasMondatos.Content = lakossag.Where(x => x.Nem == "férfi" || x.Nem == "nő").Count();
        }

        private void Feladat39()
        {
            vezerlokTorlese();
            var tartomanyLegmagasabbJovedelem = lakossag
                .GroupBy(x => x.Tartomany)
                .Select(group => new
                {
                    TartomanyNev = group.Key,
                    LegmagasabbFizu = group.Max(x => x.NettoJovedelem)
                })
                .OrderBy(x => x.LegmagasabbFizu);

            foreach (var item in tartomanyLegmagasabbJovedelem)
            {
                MegoldasLista.Items.Add($"Tartomány: {item.TartomanyNev}, Legmagasabb Fizetés: {item.LegmagasabbFizu}");
            }
        }

        private void Feladat40()
        {
            vezerlokTorlese();
            double nemetHaviJovedelem = lakossag.Where(x => x.Nemzetiseg == "német").Sum(x => x.HaviNetto);
            double nemNemetHaviJovedelem = lakossag.Where(x => x.Nemzetiseg != "német").Sum(x => x.HaviNetto);

            MegoldasMondatos.Content = $"Százalékos arány: {(nemetHaviJovedelem/nemNemetHaviJovedelem)*100:F2}";
        }

        private void Feladat41()
        {
            vezerlokTorlese();

            var rnd = new Random();

            var torokAktivSzavazo = lakossag
                .Where(x => x.Nemzetiseg.ToLower() == "török" && x.AktivSzavazo)
                .OrderBy(x => rnd.Next())
                .Take(10)
                .ToList();

            foreach (var lakos in torokAktivSzavazo)
            {
                MegoldasLista.Items.Add(lakos);
            }
        }

        private void Feladat42()
        {
            vezerlokTorlese();

            var rnd = new Random();

            var atlagSor = lakossag.Average(x => x.SorFogyasztasEvente);

            var atlagFelettSor = lakossag
                .Where(x => x.SorFogyasztasEvente > atlagSor)
                .OrderBy(x => rnd.Next())
                .Take(5)
                .ToList();

            foreach (var item in atlagFelettSor)
            {
                MegoldasLista.Items.Add(item.ToString(true));
            }
        }

        private void Feladat43()
        {
            vezerlokTorlese();

            var atlagosNettoJovedelem = lakossag.Average(x => x.NettoJovedelem);

            var megfeleloTartomanyok = lakossag
                .GroupBy(x => x.Tartomany)
                .Where(group => group.Min(x => x.NettoJovedelem) > atlagosNettoJovedelem)
                .Select(group => new { Tartomany = group.Key, LegkisebbNettoJovedelem = group.Min(x => x.NettoJovedelem) })
                .ToList();

            var random = new Random();
            var veletlenTartomanyok = megfeleloTartomanyok.OrderBy(x => random.Next()).Take(2).ToList();

            MegoldasLista.Items.Add($"Átlagos nettó jövedelem: {atlagosNettoJovedelem:F0} Ft");
            foreach (var tartomany in veletlenTartomanyok)
            {
                MegoldasLista.Items.Add($"Tartomány: {tartomany.Tartomany}, Legkisebb nettó jövedelem: {tartomany.LegkisebbNettoJovedelem} Ft");
            }

        }

        private void Feladat44()
        {
            vezerlokTorlese();

            Random rnd = new Random();

            var nemIsmerunkVegzettseget = lakossag
                .Where(x => x.IskolaiVegzettseg == " ")
                .OrderBy(x=> rnd.Next())
                .Take(3)
                .ToList();
            
            foreach (var item in nemIsmerunkVegzettseget)
            {
                MegoldasLista.Items.Add(item);
            }
        }
        private void Feladat45()
        {
            vezerlokTorlese();

            Random rnd = new Random();

            var egyetemiVeglNok = lakossag
                .Where(x => x.IskolaiVegzettseg == "Universität" && x.Nem == "nő" && x.Nepcsoport != "bajor")
                .Take(5)
                .ToList();


            foreach (var nok in egyetemiVeglNok)
            {
                MegoldasLista.Items.Add(nok.ToString());
            }

            var elsoNoJovedelem = egyetemiVeglNok.FirstOrDefault()?.NettoJovedelem ?? 0;

            var nemetNokTobbeJovedelemmel = lakossag
                .Where(x => x.Nemzetiseg == "német" && x.Nem == "nő" && x.NettoJovedelem > elsoNoJovedelem)
                .OrderBy(x => rnd.Next())
                .Take(3)
                .ToList();

            foreach (var nemetNo in nemetNokTobbeJovedelemmel)
            {
                MegoldasLista.Items.Add(nemetNo.ToString());
            }
        }
    }
}
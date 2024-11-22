using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241108bevolkerung
{
    internal class Allampolgar
    {
        public int Id { get; set; }
        public string Nem { get; set; }
        public int SzuletesiEv  { get; set; }
        public int Suly { get; set; }
        public int Magassag { get; set; }
        public bool Dohanyzik { get; set; }
        public string Nemzetiseg { get; set; }
        public string Nepcsoport { get; set; }
        public string Tartomany { get; set; }
        public int NettoJovedelem { get; set; }
        public string IskolaiVegzettseg { get; set; }
        public string PolitikaiNezet { get; set; }
        public bool AktivSzavazo { get; set; }
        public int SorFogyasztasEvente { get; set; }
        public int KrumpliFogyasztasEvente { get; set; }
        public int HaviNetto { get; set; }
        public int Eletkor { get; set; }
        public int jelen;
        public Allampolgar(string sor)
        {
            var v = sor.Split(";");
            this.Id = int.Parse(v[0]);
            this.Nem = v[1];
            this.SzuletesiEv = int.Parse(v[2]);
            this.Suly = int.Parse(v[3]);
            this.Magassag = int.Parse(v[4]);
            this.Dohanyzik = v[5].ToLower() == "igen" ? true : false;
            this.Nemzetiseg = v[6];
            this.Nepcsoport = this.Nemzetiseg == "német" ? v[7] : "" ;
            this.Tartomany = v[8];
            this.NettoJovedelem = int.Parse(v[9]);
            this.IskolaiVegzettseg = v[10];
            this.PolitikaiNezet = v[11];
            this.AktivSzavazo = v[12].ToLower() == "igen" ? true : false;
            this.SorFogyasztasEvente = v[13] == "NA" ? -1 : int.Parse(v[13]);
            this.KrumpliFogyasztasEvente = v[14] == "NA" ? -1 : int.Parse(v[14]);
            this.HaviNetto = this.NettoJovedelem / 12;
            this.jelen = DateTime.Now.Year;
            this.Eletkor = this.jelen - this.SzuletesiEv;
            
        }
        public override string ToString()
        {
            return $"{this.Id} {this.Nem} {this.SzuletesiEv} {this.Suly} {this.Magassag} {this.Dohanyzik} {this.Nemzetiseg} {this.Nepcsoport} {this.Nem} {this.Tartomany}  {this.NettoJovedelem} {this.IskolaiVegzettseg} {this.PolitikaiNezet} {this.AktivSzavazo} {this.SorFogyasztasEvente} {this.KrumpliFogyasztasEvente} {this.HaviNetto} {this.Eletkor}";
        }
        public  string ToString(bool kiiratas)
        {
            if (kiiratas)
            {
                return $"{this.Nem}\t{this.SzuletesiEv}\t{this.Suly}\t{this.Magassag}\t{(this.Dohanyzik ? "igen":"nem")}";
            }
            else
            {
                return $"{this.Id}\t{this.Nemzetiseg}\t{(this.Nepcsoport == "" ? "nincs" : this.Nepcsoport)}\t{this.Tartomany}\t{this.NettoJovedelem}";
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Linq;
using videotheque.Model;

namespace videotheque.ViewModel
{
    class ModificationMediaViewModel
    {
        public Media Media { get; set; }

        public int idMedia { get; set; }

        public string TitreMedia { get; set; }

        public DateTime DateSortie { get; set; }

        public TimeSpan Duree { get; set; }

        public bool Vu { get; set; }

        public int Note { get; set; }

        public string Commentaire { get; set; }

        public string Synopsis { get; set; }

        public ETypeMedia.TypeMedia TypeMedia { get; set; }

        public int AgeMinimum { get; set; }

        public bool SupportPhysique { get; set; }

        public bool SupportNumerique { get; set; }

        public string Image { get; set; }

        public ELangue.Langue LangueVO { get; set; }

        public ELangue.Langue LangueMedia { get; set; }

        public ELangue.Langue SousTitres { get; set; }

        public ModificationMediaViewModel(){ }

        public ModificationMediaViewModel(int idMedia)
        {
            this.idMedia = idMedia;
            this.ChargerMediaAModifier();

            this.TitreMedia = this.Media.Titre;
            this.Vu = this.Media.Vu;
            this.DateSortie = this.Media.DateSortie;
            this.Duree = this.Media.Duree;
            this.Note = this.Media.Note;
            this.Commentaire = this.Media.Commentaire;
            this.Synopsis = this.Media.Synopsis;
            this.TypeMedia = this.Media.TypeMedia;
            this.AgeMinimum = this.Media.AgeMinimum;
            this.SupportPhysique = this.Media.SupportPhysique;
            this.SupportNumerique = this.Media.SupportNumerique;
            this.Image = this.Media.Image;
            this.LangueVO = this.Media.LangueVO;
            this.LangueMedia = this.Media.LangueMedia;
            this.SousTitres = this.Media.SousTitres;
        }

        public async void ChargerMediaAModifier()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.Media = context.Medias.Where(m => m.Id == idMedia).First();
        }

    }

    
}

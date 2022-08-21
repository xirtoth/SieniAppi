using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace SieniAppi
{
    public class MushRoomViewModel : INotifyPropertyChanged
    {
        readonly IList<Mushroom> source;

        public ObservableCollection<Mushroom> Mushrooms { get; private set; }

        public Mushroom PreviousMushroom { get; private set; }
        public Mushroom CurrentMushroom { get; private set; }
        public Mushroom CurrentItem { get; private set; }
        
        public int PreviousPosition { get; private set; }
        public int CurrentPosition { get; private set; }
        public int Position { get; private set; }

        public ICommand FilterCommand => new Command<string>(FilterItems);

        public MushRoomViewModel()
        {

            source = new List<Mushroom>();
            CreateMushroomCatalog();

            OnPropertyChanged("CurrentItem");
            


        }

        private void CreateMushroomCatalog()
        {
            source.Add(new Mushroom
            {
                Name = "Kanttarelli",
                Description = "Keltavahvero eli kantarelli (Cantharellus cibarius) on kantarellien heimoon kuuluva herkullinen ruokasieni ja kauppasieni. Keltavahveroa on kutsuttu myös vanhoissa sieniopaskirjoissa ruskosieneksi tai keltasieneksi. ",
                isPoisonous = false,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/9a/Chanterelle_Cantharellus_cibarius.jpg"

            });

            source.Add(new Mushroom
            {
                Name = "Mustatorvisieni (Craterellus cornucopioides)",
                Description = "Mustatorvisieni (Craterellus cornucopioides) on kantarelleihin kuuluva erinomainen ruokasieni, joka ei tarvitse esikäsittelyä. ",
                isPoisonous = false,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a7/Craterellus_cornucopioides_03.jpg"
            });

            source.Add(new Mushroom
            {
                Name = "HerkkuTatti (Boletus edulis)",
                Description = "Herkkutatti (Boletus edulis, aiemmin myös kivitatti ja hepotatti) on arvostettu ruokasieni, jota tavataan lähes kaikkialla pohjoisen pallonpuoliskon havu- ja sekametsissä.",
                isPoisonous=false,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2a/Boletus_edulis_2008_edit.jpg/800px-Boletus_edulis_2008_edit.jpg"

            });

            source.Add(new Mushroom
            {
                Name = "Haaparousku (Lactarius trivialis)",
                Description = "Haaparousku (Lactarius trivialis) on Suomessa perinteinen ruokasieni.",
                isPoisonous = false,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ea/Lactarius_trivialis_-_Lindsey_1a.jpg"

            });
            source.Add(new Mushroom
            {
                Name = "Musta surma",
                Description = "Musta surma tappaa",
                isPoisonous = true,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ea/Lactarius_trivialis_-_Lindsey_1a.jpg"

            });

            Mushrooms = new ObservableCollection<Mushroom>(source);

        }

        void ItemChanged(Mushroom item)
        {
            PreviousMushroom = CurrentMushroom;
            CurrentMushroom = item;
            OnPropertyChanged("PreviousMushroom");
            OnPropertyChanged("CurrentMushroom");
        }

        void PositionChanged(int position)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = position;
            OnPropertyChanged("PreviousPosition");
            OnPropertyChanged("CurrentPosition");
        }

        void RemoveMushroom(Mushroom mushroom)
        {
            if (Mushrooms.Contains(mushroom))
            {
                Mushrooms.Remove(mushroom);
            }
        }

   /*     void FavoriteMonkey(Mushroom monkey)
        {
            monkey.IsFavorite = !monkey.IsFavorite;
        }*/
        
        void FilterItems(string filter)
        {
            if (filter == null)
            {
                Mushrooms = new ObservableCollection<Mushroom>(source);
            }
            var filteredItems = source.Where(mushroom => mushroom.Name.ToLower().Contains(filter.ToLower())).ToList();
            foreach(var mushroom in source)
            {
                if(!filteredItems.Contains(mushroom))
                {
                    Mushrooms.Remove(mushroom);
                }
                else
                {
                    if(!Mushrooms.Contains(mushroom))
                    {
                        Mushrooms.Add(mushroom);
                    }
                }
            }
        }
        #region InotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

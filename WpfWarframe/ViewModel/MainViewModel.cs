using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfWarframe.Model.API.Entities;
using WpfWarframe.ViewModel.BaseClass;
using WpfWarframe.Model.API;
using System.Windows.Input;
using System.Threading;

namespace WpfWarframe.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            //ShowFissures();
            ShowSortie();
            FissureForeverUpdate();

            //Fissures = new ObservableCollection<Fissure>(fissureList.ToList().Distinct());
        }

        private ObservableCollection<object> _displayedItems = new ObservableCollection<object>();
        public ObservableCollection<object> DisplayedItems
        {
            get { return _displayedItems; }
            set { _displayedItems = value; OnPropertyChanged(nameof(DisplayedItems)); }
        }

        private ObservableCollection<Fissure> _fissures = new ObservableCollection<Fissure>();
        public ObservableCollection<Fissure> Fissures
        {
            get { return _fissures; }
            set { _fissures = value; OnPropertyChanged(nameof(Fissures)); }
        }

        public void FissureForeverUpdate()
        {
            Task task = new Task(() =>
            {
                while (true)
                {
                    ShowFissures();

                    foreach (object f in DisplayedItems)
                        OnPropertyChanged(nameof(f));

                    Thread.Sleep(1000);
                }
            });
            task.Start();
        }

        public void ShowFissures()
        {
            List<Fissure> fissureList = WarframeAPI.GetInstance().Fissures;
            DisplayedItems = new ObservableCollection<object>();
            foreach (Fissure f in fissureList)
                DisplayedItems.Add(f);
        }

        public void ShowSortie()
        {
            Sortie sortie = WarframeAPI.GetInstance().Sortie;
            DisplayedItems = new ObservableCollection<object>();
            DisplayedItems.Add(sortie);
        }

        private ICommand _sortieCommand;
        public ICommand SortieCommand
        {
            get
            {
                if (_sortieCommand == null)
                {
                    _sortieCommand = new RelayCommand(
                        param => ShowSortie(),
                        param => true
                    );
                }
                return _sortieCommand;
            }
        }

        private ICommand _fissuresCommand;
        public ICommand FissuresCommand
        {
            get
            {
                if (_fissuresCommand == null)
                {
                    _fissuresCommand = new RelayCommand(
                        param => ShowFissures(),
                        param => true
                    );
                }
                return _fissuresCommand;
            }
        }
    }
}

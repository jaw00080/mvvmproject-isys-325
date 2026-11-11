using Exercise.Data;
using Exercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace Exercise.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private PersonDbContext _context = new PersonDbContext();

        public ObservableCollection<Person> People { get; } = new();

        public ObservableCollection<Person> FilteredPeople { get; } = new();

        public MainViewModel()
        {
            _context.Database.EnsureCreated();
            foreach (var person in _context.People)
            {
                People.Add(person);
            }
            UpdateSearch(SearchText);
            AddPersonCommand = new RelayCommand(AddPerson);
            DeletePersonCommand = new RelayCommand(DeletePerson, CanDeletePerson);
            SaveCommand = new RelayCommand(SaveChanges);
            SortByLastNameCommand = new RelayCommand(_ => SortByLastName(null));
            SortByAgeCommand = new RelayCommand(_ => SortByAge(null));
            SortByLastName(null);
        }

        private void UpdateSearch(string searchText)
        {
            FilteredPeople.Clear();

            var SearchText = searchText.Trim();

            var filtered = People.Where(p =>
                p.FirstName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.LastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            foreach (var person in filtered)
            {
                FilteredPeople.Add(person);
            }
        }

        public String _searchText = "";

        public String SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    UpdateSearch(_searchText);
                }
            }
        }

        private Person? _selectedPerson;

        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged();

                    (DeletePersonCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand AddPersonCommand { get; }

        private void AddPerson(object? parameter)
        {
            Person person = new Person()
            {
                FirstName = "New",
                LastName = "Person",
                Age = 18
            };
            _context.People.Add(person);
            People.Add(person);
            UpdateSearch(SearchText);
            SelectedPerson = person;
        }

        public ICommand DeletePersonCommand { get; }

        private bool CanDeletePerson(object? parameter)
        {
            return SelectedPerson != null;
        }

        private void DeletePerson(object? parameter)
        {
            if (SelectedPerson != null)
            {
                _context.People.Remove(SelectedPerson);
                People.Remove(SelectedPerson);
                UpdateSearch(SearchText);
                SelectedPerson = null;
            }
        }

        public ICommand SaveCommand { get; }

        private void SaveChanges(object? parameter)
        {
            _context.SaveChanges();
            MessageBox.Show("Changes saved successfully.", "Save Changes");
        }

        public ICommand SortByLastNameCommand { get; }

        public void SortByLastName(object? parameter)
        {
            FilteredPeople.Clear();
            var sorted = People.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
            
            foreach (var person in sorted)
            {
                FilteredPeople.Add(person);
            }
            UpdateSearch(SearchText);
        }

        public ICommand SortByAgeCommand { get; }

        public void SortByAge(object? parameter)
        {
            FilteredPeople.Clear();
            var sorted = People.OrderBy(p => p.Age).ToList();

            foreach (var person in sorted)
            {
                FilteredPeople.Add(person);
            }
        }
    }
}

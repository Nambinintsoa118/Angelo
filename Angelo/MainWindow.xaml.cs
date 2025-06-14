using Angelo.DataAccess;
using Angelo.Models;
using System.Windows;
using System.Collections.Generic;

namespace Angelo
{
    public partial class MainWindow : Window
    {
        private Personne selectedPersonne;

        public MainWindow()
        {
            InitializeComponent();
            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            DataGridPersonne.ItemsSource = PersonneRepository.GetAll();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            var p = new Personne
            {
                Nom = TxtNom.Text,
                Prenom = TxtPrenom.Text,
                Age = int.Parse(TxtAge.Text)
            };
            PersonneRepository.Ajouter(p);
            ChargerDonnees();
            ViderChamps();
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPersonne != null)
            {
                selectedPersonne.Nom = TxtNom.Text;
                selectedPersonne.Prenom = TxtPrenom.Text;
                selectedPersonne.Age = int.Parse(TxtAge.Text);
                PersonneRepository.Modifier(selectedPersonne);
                ChargerDonnees();
                ViderChamps();
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPersonne != null)
            {
                PersonneRepository.Supprimer(selectedPersonne.Id);
                ChargerDonnees();
                ViderChamps();
            }
        }

        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {
            var liste = PersonneRepository.Rechercher(TxtRecherche.Text);
            DataGridPersonne.ItemsSource = liste;
        }

        private void DataGridPersonne_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedPersonne = (Personne)DataGridPersonne.SelectedItem;
            if (selectedPersonne != null)
            {
                TxtNom.Text = selectedPersonne.Nom;
                TxtPrenom.Text = selectedPersonne.Prenom;
                TxtAge.Text = selectedPersonne.Age.ToString();
            }
        }

        private void ViderChamps()
        {
            TxtNom.Text = "";
            TxtPrenom.Text = "";
            TxtAge.Text = "";
            TxtRecherche.Text = "";
        }
    }
}

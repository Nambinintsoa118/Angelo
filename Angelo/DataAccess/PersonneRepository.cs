using Angelo.Models;
using Npgsql;
using System.Collections.Generic;

namespace Angelo.DataAccess
{
    public static class PersonneRepository
    {
        public static void Ajouter(Personne p)
        {
            using var conn = Database.GetConnection();
            conn.Open();
            var cmd = new NpgsqlCommand("INSERT INTO personne(nom, prenom, age) VALUES (@nom, @prenom, @age)", conn);
            cmd.Parameters.AddWithValue("nom", p.Nom);
            cmd.Parameters.AddWithValue("prenom", p.Prenom);
            cmd.Parameters.AddWithValue("age", p.Age);
            cmd.ExecuteNonQuery();
        }

        public static void Modifier(Personne p)
        {
            using var conn = Database.GetConnection();
            conn.Open();
            var cmd = new NpgsqlCommand("UPDATE personne SET nom = @nom, prenom = @prenom, age = @age WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("nom", p.Nom);
            cmd.Parameters.AddWithValue("prenom", p.Prenom);
            cmd.Parameters.AddWithValue("age", p.Age);
            cmd.Parameters.AddWithValue("id", p.Id);
            cmd.ExecuteNonQuery();
        }

        public static void Supprimer(int id)
        {
            using var conn = Database.GetConnection();
            conn.Open();
            var cmd = new NpgsqlCommand("DELETE FROM personne WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public static List<Personne> Rechercher(string nom)
        {
            var personnes = new List<Personne>();
            using var conn = Database.GetConnection();
            conn.Open();
            var cmd = new NpgsqlCommand("SELECT * FROM personne WHERE nom ILIKE @nom", conn);
            cmd.Parameters.AddWithValue("nom", $"%{nom}%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                personnes.Add(new Personne
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    Age = reader.GetInt32(3)
                });
            }
            return personnes;
        }

        public static List<Personne> GetAll()
        {
            var personnes = new List<Personne>();
            using var conn = Database.GetConnection();
            conn.Open();
            var cmd = new NpgsqlCommand("SELECT * FROM personne ORDER BY id", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                personnes.Add(new Personne
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    Age = reader.GetInt32(3)
                });
            }
            return personnes;
        }
    }
}

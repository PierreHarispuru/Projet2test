using System.Collections.Generic;
using System;
using Projet2.Models;

public interface IDal : IDisposable
{
    void DeleteCreateDatabase();
    List<Profil> ObtientTousLesProfils();
    int CreerProfil(string Nom, int age);
    void ModifierProfil(int id, string Nom, int age);
}
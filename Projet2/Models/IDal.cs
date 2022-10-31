using System.Collections.Generic;
using System;
using Projet2.Models;

public interface IDal : IDisposable
{
    void DeleteCreateDatabase();
    List<Profil> ObtientTousLesProfils();
    int CreerProfil(string nom, string prenom, String typeP, string mail, int telephone, string addresse, int codepostal, Int64 siret, String nomentreprise);
    void ModifierProfil(int id, string nom, string prenom, string mail, int telephone, string addresse, int codepostal);
}
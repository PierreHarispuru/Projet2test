using System.Collections.Generic;
using System;
using Projet2.Models;

public interface IDal : IDisposable
{
    void DeleteCreateDatabase();
    List<Profil> ObtientTousLesProfils();
    int CreerProfil(Profil profil);

    int AjouterUtilisateur(string nom, string password);
    Profil Authentifier(string nom, string password);
    Profil ObtenirUtilisateur(int id);
    Profil ObtenirUtilisateur(string idStr);
    //void ModifierProfil(int id, string nom, string prenom, string mail, int telephone, string addresse, int codepostal);
}
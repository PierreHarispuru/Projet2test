using System;
using System.Collections.Generic;
using Xunit;
using Projet2.Models;

namespace Test1
{
    public class UnitTest1
    {
        [Fact]
        public void Creation_Profil_Verification()
        {
            // Nous supprimons la base si elle existe puis nous la créons
            using (Dal dal = new Dal())
            {
                // Nous supprimons et créons la db avant le test
                dal.DeleteCreateDatabase();
                // Nous créons un profil
                dal.CreerProfil("Jean", 18);

                // Nous vérifions que le profil a bien été créé
                List<Profil> profil = dal.ObtientTousLesProfils();
                Assert.NotNull(profil);
                Assert.Single(profil);
                Assert.Equal("Jean", profil[0].Nom);
                Assert.Equal(18, profil[0].Age);
            }
        }
    }
}

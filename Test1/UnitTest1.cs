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
            using (BddContext ctx = new BddContext())
            {
                // Nous supprimons et créons la db avant le test
                ctx.InitializeDb();
                // Nous créons un profil

                // Nous vérifions que le profil a bien été créé
                using (Dal dal = new Dal())
                {
                    List<Profil> profil = dal.ObtientTousLesProfils();
                    Assert.NotNull(profil);
                    Assert.Single(profil);
                    Assert.Equal("Jean", profil[0].Prenom);
                }
            }
        }
    }
}
